using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Core.Services
{
    internal class QuizService : IQuizService
    {
        private readonly IQuizRepository repository;

        public QuizService(IQuizRepository repository)
        {
            this.repository = repository;
        }

        public Task<Quiz> CreateQuiz(string title)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> EditQuiz(Quiz quiz, string newTitle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> GetQuizByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
