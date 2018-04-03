using NiftyDapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nifty.Service
{
    public class CommonService
    {
        IDataAccess dataAccess;

        public CommonService(DatabaseType databaseType = DatabaseType.SqlServer, string connectionName = "DefaultConnection")
        {
            this.dataAccess = DapperFactory.GetDataAccess(databaseType, connectionName);
        }

        public List<T> QueryAsSql<T, TParam>(string sql, TParam param) where T : class
        {
            return dataAccess.QueryAsSql<T, TParam>(sql, param);
        }
    }
}
