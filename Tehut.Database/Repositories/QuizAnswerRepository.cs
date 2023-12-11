using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Database.Repositories
{
    public class QuizAnswerRepository : IQuizAnswerRepository
    {
        private readonly IDatabaseFactory databaseFactory;

        public QuizAnswerRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Task<QuizAnswer> CreateAnswer(QuizQuestion quizQuestion, string answer)
        {
            throw new NotImplementedException();
        }
    }
}
