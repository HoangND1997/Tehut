using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Tehut.Core.Repositories;
using Tehut.Database.Migrator;
using Tehut.Database.Repositories;

namespace Tehut.Database
{
    public static class DatabaseServiceExtensions
    {
        public static void AddTehutDatabase(this IServiceCollection serviceCollection, DatabaseConfig databaseConfig)
        {
            serviceCollection.AddSingleton(databaseConfig); 

            serviceCollection.AddTransient<IQuizRepository, QuizRepository>();
            serviceCollection.AddTransient<IQuizQuestionRepository, QuizQuestionRepository>();
            serviceCollection.AddTransient<IDatabaseFactory, DatabaseFactory>();    

            serviceCollection.AddSingleton<IDatabaseMigrator, DatabaseMigrator>();
            serviceCollection.AddFluentMigratorCore()
                             .ConfigureRunner(rb => rb
                                .AddSQLite()
                                .WithGlobalConnectionString(s => s.GetRequiredService<IDatabaseFactory>().GetConnectionString())
                                .ScanIn(typeof(DatabaseServiceExtensions).Assembly).For.Migrations())
                             .AddLogging(lb => lb.AddFluentMigratorConsole());
        }
    }
}
