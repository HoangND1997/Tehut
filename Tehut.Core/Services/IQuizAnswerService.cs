using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizAnswerService
    {
        Task<QuizAnswer> CreateAnswer(string answer); 
    }
}
