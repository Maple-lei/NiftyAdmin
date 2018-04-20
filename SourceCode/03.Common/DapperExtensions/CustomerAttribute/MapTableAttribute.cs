using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtensions.CustomerAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MapTableAttribute : Attribute
    {
        public string MapTableNameValue { get; set; }
        public MapTableAttribute()
        {

        }

        public MapTableAttribute(string mapTableName)
        {
            this.MapTableNameValue = mapTableName;
        }
    }
}
