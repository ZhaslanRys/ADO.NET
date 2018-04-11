using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public abstract class BaseService
    {
        public BaseService(ConnectionStringSettings connectionString)
        {
            ConnectionString = connectionString.ConnectionString;
            ProviderName = connectionString.ProviderName;
        }
        public string ProviderName { get; private set; }
        public string ConnectionString { get; private set; }
        protected DbConnection CreateConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }
    }
}
