using Microsoft.Extensions.Configuration;

namespace SWECVI.ApplicationCore.Common
{ 
    public class DataSourceProvider : IDataSourceProvider
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;

        public DataSourceProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public void SetConnectionString(string connection)
        {
            _connectionString = connection;
        }
    }
}
