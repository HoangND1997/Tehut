using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Tehut.Core.Repositories;
using Tehut.Database.Migrator;
using Tehut.Database.Repositories;

namespace Tehut.Database
{
    public static class DatabaseExtensions
    {
        public static void AddTehutDatabase(this IServiceCollection serviceCollection, DatabaseConfig databaseConfig)
        {
            serviceCollection.AddSingleton(databaseConfig); 

            serviceCollection.AddTransient<IQuizRepository, QuizRepository>();
            serviceCollection.AddTransient<IQuizQuestionRepository, QuizQuestionRepository>();
            serviceCollection.AddTransient<IQuizAnswerRepository, QuizAnswerRepository>();

            serviceCollection.AddSingleton<IDatabaseMigrator, DatabaseMigrator>();
            serviceCollection.AddFluentMigratorCore()
                             .ConfigureRunner(rb => rb
                                .AddSQLite()
                                .WithGlobalConnectionString($"Data Source={databaseConfig.DatabasePath}")
                                .ScanIn(typeof(DatabaseExtensions).Assembly).For.Migrations()
                             );
        }
    }
}
