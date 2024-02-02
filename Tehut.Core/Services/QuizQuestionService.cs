using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Core.Services
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly IQuizQuestionRepository repository;

        public QuizQuestionService(IQuizQuestionRepository repository)
        {
            this.repository = repository;
        }

        public Task<QuizQuestion> CreateQuestion(Quiz quiz)
        {
            return repository.CreateQuestion(quiz);
        }

        public Task SaveQuestion(QuizQuestion question)
        {
            return repository.SaveQuestion(question);
        }

        public Task DeleteQuestion(QuizQuestion question)
        {
            return repository.DeleteQuestion(question);
        }
    }
}
