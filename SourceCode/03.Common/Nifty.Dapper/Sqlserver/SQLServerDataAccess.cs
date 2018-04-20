using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace NiftyDapper
{
    public class SQLServerDataAccess : BaseDataAccess
    {
        public SQLServerDataAccess(string connectionName = "DefaultConnection")
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            dbConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
            var sqlGenerator = new SqlGeneratorImpl(config);
            dbExtention = new Database(dbConnection, sqlGenerator);
        }

        public List<T> QueryByPaging<T>(PageModel pageModel)
        {
            var param = new DynamicParameters();
            param.Add("@TableName", pageModel.TableName, DbType.String);
            param.Add("@Fields", pageModel.Fields, DbType.String);
            param.Add("@Where", pageModel.Where, DbType.String);
            param.Add("@OrderBy", pageModel.OrderBy, DbType.String);
            param.Add("@PageIndex", pageModel.PageIndex, DbType.Int32);
            param.Add("@PageSize", pageModel.PageSize, DbType.Int32);
            param.Add("@TotalCount", 0, DbType.Int32, ParameterDirection.Output);

            List<T> result = dbConnection.Query<T>("Proc_QueryByPaging", param, null, true, null, CommandType.StoredProcedure).ToList();
            pageModel.TotalCount = param.Get<int>("@TotalCount");

            return result;
        }
    }
}
