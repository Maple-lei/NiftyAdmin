using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiftyDapper
{
    public class DapperFactory
    {
        /// <summary>
        /// 胡磊 2017-6-22 默认访问Sqlserver 的连接字符串为 "Default"的数据库
        /// 想法：默认的访问方式为database.config的第一个连接字符串，而不是默认为Sqlserver数据库,这样更灵活,
        /// 但是每次都去config里面去读又影响效率，有什么办法只判断一次
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static IDataAccess GetDataAccess(DatabaseType databaseType = DatabaseType.SqlServer, string connectionName = "DefaultConnection")
        {
            switch (databaseType)
            {
                case DatabaseType.Oracle:
                    return new OracleDataAccess(connectionName);
                default:
                    return new SQLServerDataAccess(connectionName);
            }
        }
    }
}
