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

        public Task<QuizQuestion> CreateQuestion(Quiz quiz, string question)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQuestion(QuizQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task EditQuestion(QuizQuestion question, string newQuestion)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuizAnswer>> GetAnswers(QuizQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task SetCorrectAnswer(QuizAnswer answer)
        {
            throw new NotImplementedException();
        }
    }
}
