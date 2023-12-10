using FluentMigrator.Runner;

namespace Tehut.Database.Migrator
{
    public  class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly IMigrationRunner migrationRunner;

        public DatabaseMigrator(IMigrationRunner migrationRunner)
        {
            this.migrationRunner = migrationRunner;
        }

        public void MigrateUp()
        {
            migrationRunner.MigrateUp();             
        }
    }
}
