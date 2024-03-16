using FluentMigrator.Runner;

namespace Tehut.Database.Migrator
{
    public  class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly IDatabaseFactory databaseFactory;
        private readonly IMigrationRunner migrationRunner;

        public DatabaseMigrator(DatabaseConfig databaseConfig, IDatabaseFactory databaseFactory, IMigrationRunner migrationRunner)
        {
            this.databaseConfig = databaseConfig;
            this.databaseFactory = databaseFactory;
            this.migrationRunner = migrationRunner;
        }

        public void MigrateUp()
        {
            if (databaseConfig.UseInMemory)
            {
                // Creates a persitant connection if an in memory database is used
                var connection = databaseFactory.CreateConnection();
                connection.Open(); 
            }

            migrationRunner.MigrateUp();             
        }
    }
}
