using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiftyDapper
{
    public interface IDataAccess
    {
        List<T> QueryAsSql<T, TParam>(string sql, TParam param) where T : class;

        List<T> QueryAsProc<T, TParam>(string procName, TParam param) where T : class;

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <typeparam name="TParam">动态对象</typeparam>
        /// <param name="sql">需要执行的Sql语句</param>
        /// <param name="param">Sql语句需要替换的参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql<TParam>(string sql, TParam param);

        /// <summary>
        /// 批量插入数据，内部自带事务
        /// </summary>
        /// <typeparam name="T">对应表的实体类</typeparam>
        /// <param name="entityList">枚举对象</param>
        /// <returns>是否插入成功</returns>
        bool InsertBatch<T>(IEnumerable<T> entityList) where T : class;

        /// <summary>
        /// 插入单个实体类
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体类</param>
        /// <returns>插入成功：如果有主键，则返回第一个主键的值。插入失败则返回null</returns>
        dynamic Insert<T>(T entity) where T : class;

        List<T> GetList<T>(object predicate = null, IList<ISort> sort = null) where T : class;

        List<T> GetPage<T>(int pageNo, int pageSize, object predicate = null, IList<ISort> sort = null) where T : class;

        bool Delete<T>(object predicate = null) where T : class;

        bool Update<T>(T entity) where T : class;
    }
}
