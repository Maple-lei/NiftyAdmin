using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NiftyDapper
{
    public abstract class BaseDataAccess : IDataAccess
    {
        public IDbConnection dbConnection = null;
        public IDatabase dbExtention = null;

        public virtual List<T> QueryAsSql<T, TParam>(string sql, TParam param) where T : class
        {
            return dbConnection.Query<T>(sql, param).ToList();
        }

        public virtual List<T> QueryAsProc<T, TParam>(string procName, TParam param) where T : class
        {
            return dbConnection.Query<T>(procName, param, null, true, null, CommandType.StoredProcedure).ToList();
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <typeparam name="TParam">动态对象</typeparam>
        /// <param name="sql">需要执行的Sql语句</param>
        /// <param name="param">Sql语句需要替换的参数</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteSql<TParam>(string sql,TParam param)
        {
            return dbConnection.Execute(sql, param);
        }

        public virtual List<T> GetList<T>(object predicate = null, IList<ISort> sort = null) where T : class
        {
            return dbExtention.GetList<T>(predicate, sort).ToList();
        }

        public virtual List<T> GetPage<T>(int pageNo, int pageSize, object predicate = null, IList<ISort> sort = null) where T : class
        {
            return dbExtention.GetPage<T>(predicate, sort, pageNo, pageSize).ToList();
        }

        /// <summary>
        /// 批量插入数据，内部自带事务
        /// </summary>
        /// <typeparam name="T">对应表的实体类</typeparam>
        /// <param name="entityList">枚举对象</param>
        /// <returns>是否插入成功</returns>
        public virtual bool InsertBatch<T>(IEnumerable<T> entityList) where T : class
        {
            bool isOk = true;

            IDbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                if (dbTransaction == null)
                {
                    return false;
                }

                dbExtention.Insert<T>(entityList, dbTransaction);

                dbTransaction.Commit();
                dbTransaction.Dispose();

                return isOk;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                dbTransaction.Dispose();

                return false;
            }
        }

        /// <summary>
        /// 插入单个实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>插入成功：如果有主键，则返回第一个主键的值。插入失败则返回null</returns>
        public virtual dynamic Insert<T>(T entity) where T : class
        {
            return dbExtention.Insert(entity);
        }

        public virtual bool Delete<T>(object predicate = null) where T : class
        {
            return dbExtention.Delete<T>(predicate);
        }

        public virtual bool Update<T>(T entity) where T : class
        {
            return dbExtention.Update<T>(entity);
        }
    }
}
