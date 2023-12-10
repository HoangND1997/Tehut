using Microsoft.Extensions.DependencyInjection;
using Tehut.Core.Services;

namespace Tehut.Core
{
    public static class ApplicationExtensions
    {
        public static void AddTehutApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IQuizService, QuizService>(); 
            serviceCollection.AddTransient<IQuizQuestionService, QuizQuestionService>(); 
            serviceCollection.AddTransient<IQuizAnswerService, QuizAnswerService>(); 
        }
    }   
}