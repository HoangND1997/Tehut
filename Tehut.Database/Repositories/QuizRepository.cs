using Dapper;
using System.Transactions;
using Tehut.Core.Models;
using Tehut.Core.Repositories;
using Tehut.Database;
using Tehut.Database.Schemas;

namespace Tehut.Database.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly IDatabaseFactory databaseFactory;

        public QuizRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public async Task<Quiz> CreateQuiz(string name)
        {
            using var connection = databaseFactory.CreateConnection();

            var createdId = await connection.QuerySingleAsync<int>(new CommandDefinition($"Insert into {QuizTable.TableName} ({QuizTable.Name}) Values (@name) Returning {QuizTable.Id};", new { name }));

            return new Quiz
            {
                Id = createdId,
                Name = name,
            };
        }

        public async Task SaveQuiz(Quiz quiz)
        {
            if (quiz is null)
            {
                return; 
            }

            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Update {QuizTable.TableName} Set {QuizTable.Name} = @name Where {QuizTable.Id} = @id;", new { name = quiz.Name, id = quiz.Id }));
        }

        public async Task DeleteQuiz(Quiz quiz)
        {
            if (quiz is null)
            {
                return; 
            }

            using var connection = databaseFactory.CreateConnection();

            connection.Open(); 

            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(new CommandDefinition($"Delete from {QuizQuestionTable.TableName} where {QuizQuestionTable.Quiz} = @quizId", new { quizId = quiz.Id })); 
                await connection.ExecuteAsync(new CommandDefinition($"Delete from {QuizTable.TableName} where {QuizTable.Id} = @id;", new { id = quiz.Id }));

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            using var connection = databaseFactory.CreateConnection();

            return await connection.QueryAsync<Quiz>(new CommandDefinition($"Select * from {QuizTable.TableName};"));
        }

        public async Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz)
        {
            if (quiz is null)
            { 
                return Enumerable.Empty<QuizQuestion>();
            }

            using var connection = databaseFactory.CreateConnection();
            
            var questions = await connection.QueryAsync<QuizQuestion>(new CommandDefinition($"Select * from {QuizQuestionTable.TableName} where {QuizQuestionTable.Quiz} = @quizId;", new { quizId = quiz.Id }));

            foreach (var question in questions)
            {
                question.Quiz = quiz;
            }

            return questions; 
        }
    }
}
