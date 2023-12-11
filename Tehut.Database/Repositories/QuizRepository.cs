using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Database.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly IDatabaseFactory databaseFactory;

        public QuizRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Task AddQuestion(QuizQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> CreateQuiz(string name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Quiz>> GetAllQuiz(string pattern)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> GetQuizById(int quizId)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> GetQuizByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task RemoveQuestion(QuizQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> RenameQuiz(Quiz quiz, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
