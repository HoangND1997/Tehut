using Microsoft.Extensions.DependencyInjection;
using Tehut.Core.Repositories;
using Tehut.Database.Repositories;

namespace Tehut.Database
{
    public static class DatabaseExtensions
    {
        public static void AddTehutDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IQuizRepository, QuizRepository>();
            serviceCollection.AddTransient<IQuizQuestionRepository, QuizQuestionRepository>();
            serviceCollection.AddTransient<IQuizAnswerRepository, QuizAnswerRepository>();
        }
    }
}
