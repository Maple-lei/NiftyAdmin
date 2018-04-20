using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;

namespace NiftyDapper
{
    public class OracleDataAccess : BaseDataAccess
    {
        public OracleDataAccess(string connectionName)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            dbConnection = new OracleConnection(connectionString);

            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new OracleDialect());
            var sqlGenerator = new SqlGeneratorImpl(config);
            dbExtention = new Database(dbConnection, sqlGenerator);
        }
    }
}
