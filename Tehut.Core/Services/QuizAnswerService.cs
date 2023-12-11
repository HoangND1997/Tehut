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

        public Task<QuizAnswer> CreateAnswer(QuizQuestion question, string answer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnswer(QuizAnswer answer)
        {
            throw new NotImplementedException();
        }

        public Task EditAnswer(QuizAnswer answer, string newAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
