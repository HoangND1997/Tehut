using Microsoft.Data.Sqlite;
using System.Data;

namespace Tehut.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly DatabaseConfig config;

        public DatabaseFactory(DatabaseConfig config) 
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            this.config = config;
        }

        public string GetConnectionString()
        {
            return config.UseInMemory ? $"Data Source={config.DatabasePath};Mode=Memory;Cache=Shared" : $"Data Source={config.DatabasePath}";
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(GetConnectionString());
        }
    }
}
