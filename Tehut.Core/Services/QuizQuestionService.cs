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

        public Task<QuizQuestion> AddAnswer(QuizAnswer answer)
        {
            return repository.AddAnswer(answer);
        }

        public Task<QuizQuestion> CreateQuestion(string question)
        {
            return repository.CreateQuestion(question);
        }

        public Task DeleteAnswer(QuizAnswer answer)
        {
            return repository.DeleteAnswer(answer); 
        }

        public Task EditQuestion(QuizQuestion question, string newQuestion)
        {
            return repository.EditQuestion(question, newQuestion);
        }

        public Task SetCorrectAnswer(QuizAnswer answer)
        {
            return repository.SetCorrectAnswer(answer);
        }
    }
}
