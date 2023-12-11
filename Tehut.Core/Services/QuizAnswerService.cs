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
            return repository.CreateAnswer(question, answer);
        }

        public Task DeleteAnswer(QuizAnswer answer)
        {
            return repository.DeleteAnswer(answer); 
        }

        public Task EditAnswer(QuizAnswer answer, string newAnswer)
        {
            return repository.EditAnswer(answer, newAnswer);
        }
    }
}
