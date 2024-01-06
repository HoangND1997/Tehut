using Tehut.Core.Models;
using Tehut.Core.Repositories;
using Dapper;
using Tehut.Database.Schemas;

namespace Tehut.Database.Repositories
{
    public class QuizAnswerRepository : IQuizAnswerRepository
    {
        private readonly IDatabaseFactory databaseFactory;

        public QuizAnswerRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public async Task<QuizAnswer?> CreateAnswer(QuizQuestion quizQuestion, string answer)
        {
            if (quizQuestion?.Id is not > 0)
            {
                return null; 
            }

            return new QuizAnswer
            {
                Id = 0, 
                Answer = answer,
                Question = quizQuestion,
                QuestionId = quizQuestion.Id
            };
        }

        public async Task DeleteAnswer(QuizAnswer answer)
        {
            if (answer?.Id is not > 0)
            {
                return; 
            }
            
            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Delete from {QuizAnswerTable.TableName} where id = @id", new { id = answer.Id }));
        }

        public async Task EditAnswer(QuizAnswer answer, string newAnswer)
        {
            if (answer?.Id is not > 0)
            {
                return;
            }

            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Update {QuizAnswerTable.TableName} Set {QuizAnswerTable.Answer} = @answer", new { answer = newAnswer })); 
        }
    }
}
