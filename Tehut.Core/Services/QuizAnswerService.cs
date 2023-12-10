using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Core.Services
{
    public class QuizAnswerService : IQuizAnswerService
    {
        private readonly IQuizAnswerRepository repository;

        public QuizAnswerService(IQuizAnswerRepository repository)
        {
            this.repository = repository;
        }

        public Task<QuizAnswer> CreateAnswer(string answer)
        {
            return repository.CreateAnswer(answer);
        }
    }
}
