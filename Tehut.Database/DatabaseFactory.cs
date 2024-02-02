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

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection($"Data Source={config.DatabasePath}");
        }
    }
}
