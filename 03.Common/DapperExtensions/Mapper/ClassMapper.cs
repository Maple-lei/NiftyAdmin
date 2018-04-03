﻿using DapperExtensions.CustomerAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

namespace DapperExtensions.Mapper
{
    public interface IClassMapper
    {
        string SchemaName { get; }
        string TableName { get; }
        IList<IPropertyMap> Properties { get; }
        Type EntityType { get; }
    }

    public interface IClassMapper<T> : IClassMapper where T : class
    {
    }

    /// <summary>
    /// Maps an entity to a table through a collection of property maps.
    /// </summary>
    public class ClassMapper<T> : IClassMapper<T> where T : class
    {
        /// <summary>
        /// Gets or sets the schema to use when referring to the corresponding table name in the database.
        /// </summary>
        public string SchemaName { get; protected set; }

        /// <summary>
        /// Gets or sets the table to use in the database.
        /// </summary>
        public string TableName { get; protected set; }

        /// <summary>
        /// A collection of properties that will map to columns in the database table.
        /// </summary>
        public IList<IPropertyMap> Properties { get; private set; }

        public Type EntityType
        {
            get { return typeof(T); }
        }

        public ClassMapper()
        {
            PropertyTypeKeyTypeMapping = new Dictionary<Type, KeyType>
                                             {
                                                 { typeof(byte), KeyType.Identity }, { typeof(byte?), KeyType.Identity },
                                                 { typeof(sbyte), KeyType.Identity }, { typeof(sbyte?), KeyType.Identity },
                                                 { typeof(short), KeyType.Identity }, { typeof(short?), KeyType.Identity },
                                                 { typeof(ushort), KeyType.Identity }, { typeof(ushort?), KeyType.Identity },
                                                 { typeof(int), KeyType.Identity }, { typeof(int?), KeyType.Identity },
                                                 { typeof(uint), KeyType.Identity}, { typeof(uint?), KeyType.Identity },
                                                 { typeof(long), KeyType.Identity }, { typeof(long?), KeyType.Identity },
                                                 { typeof(ulong), KeyType.Identity }, { typeof(ulong?), KeyType.Identity },
                                                 { typeof(BigInteger), KeyType.Identity }, { typeof(BigInteger?), KeyType.Identity },
                                                 { typeof(Guid), KeyType.Guid }, { typeof(Guid?), KeyType.Guid },
                                             };

            Properties = new List<IPropertyMap>();
            //Table(typeof(T).Name);

            Table(typeof(T));
        }

        protected Dictionary<Type, KeyType> PropertyTypeKeyTypeMapping { get; private set; }

        public virtual void Schema(string schemaName)
        {
            SchemaName = schemaName;
        }

        public virtual void Table(string tableName)
        {
            TableName = tableName;
        }

        public virtual void Table(Type t)
        {
            Attribute attribute = t.GetCustomAttribute(typeof(MapTableAttribute));

            if(attribute != null)
            {
                TableName = (((MapTableAttribute)attribute)).MapTableNameValue;
            }
            else
            {
                TableName = t.Name;
            }
        }

        protected virtual void AutoMap()
        {
            AutoMap(null);
        }

        protected virtual void AutoMap(Func<Type, PropertyInfo, bool> canMap)
        {
            Type type = typeof(T);
            bool hasDefinedKey = Properties.Any(p => p.KeyType != KeyType.NotAKey);
            PropertyMap keyMap = null;
            foreach (var propertyInfo in type.GetProperties())
            {
                if (Properties.Any(p => p.Name.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    continue;
                }

                if ((canMap != null && !canMap(type, propertyInfo)))
                {
                    continue;
                }

                PropertyMap map = Map(propertyInfo);
                if (!hasDefinedKey)
                {
                    if (IsAttributeExist(map.PropertyInfo,typeof(KeyAttribute)))
                    {
                        keyMap = map;
                    }
                }
            }

            if (keyMap != null)
            {
                keyMap.Key(PropertyTypeKeyTypeMapping.ContainsKey(keyMap.PropertyInfo.PropertyType)
                    ? PropertyTypeKeyTypeMapping[keyMap.PropertyInfo.PropertyType]
                    : KeyType.Assigned);
            }
        }

        /// <summary>
        /// Fluently, maps an entity property to a column
        /// </summary>
        protected PropertyMap Map(Expression<Func<T, object>> expression)
        {
            PropertyInfo propertyInfo = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            return Map(propertyInfo);
        }

        /// <summary>
        /// Fluently, maps an entity property to a column
        /// </summary>
        protected PropertyMap Map(PropertyInfo propertyInfo)
        {
            PropertyMap result = new PropertyMap(propertyInfo);

            if (!IsAttributeExist(propertyInfo,typeof(NotMapAttribute)))
            {
                this.GuardForDuplicatePropertyMap(result);
                Properties.Add(result);
            }            

            return result;
        }

        /// <summary>
        /// Removes a propertymap entry
        /// </summary>
        /// <param name="expression"></param>
        protected void UnMap(Expression<Func<T, object>> expression)
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            var mapping = this.Properties.Where(w => w.Name == propertyInfo.Name).SingleOrDefault();

            if (mapping == null)
            {
                throw new ApplicationException("Unable to UnMap because mapping does not exist.");
            }

            this.Properties.Remove(mapping);
        }

        private void GuardForDuplicatePropertyMap(PropertyMap result)
        {
            if (Properties.Any(p => p.Name.Equals(result.Name)))
            {
                throw new ArgumentException(string.Format("Duplicate mapping for property {0} detected.", result.Name));
            }
        }

        /// <summary>
        /// Is attribute exist in property
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="attributeClass"></param>
        /// <returns></returns>
        private bool IsAttributeExist(PropertyInfo propertyInfo, Type attributeClass)
        {
            object[] objAttrs = propertyInfo.GetCustomAttributes(attributeClass, true);

            return objAttrs.Length > 0 ;
        }
    }
}