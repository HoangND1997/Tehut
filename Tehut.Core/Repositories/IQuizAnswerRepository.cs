using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public interface IQuizAnswerRepository
    {
        Task<QuizAnswer> CreateAnswer(string answer);
    }
}
