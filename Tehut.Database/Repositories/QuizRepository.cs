using Dapper;
using Tehut.Core.Models;
using Tehut.Core.Repositories;
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

        public async Task EditQuiz(Quiz quiz, string newName)
        {
            if (quiz is null)
            {
                return; 
            }

            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Update {QuizTable.TableName} Set {QuizTable.Name} = @name;", new { name = newName })); 
        }

        public async Task DeleteQuiz(Quiz quiz)
        {
            if (quiz is null)
            {
                return; 
            }

            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Delete from {QuizTable.TableName} where id = @id;", new { id = quiz.Id }));
        }

        public async Task<bool> DoesQuizNameExists(string name)
        {
            if (name is null)
            {
                return false; 
            }

            using var connection = databaseFactory.CreateConnection();

            var exists = await connection.ExecuteScalarAsync<int>(new CommandDefinition($"Select Exists (Select 1 from {QuizTable.TableName} where {QuizTable.Name} = @name) as name_exists;", new { name }));

            return exists == 1;
        }


        public async Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            using var connection = databaseFactory.CreateConnection();

                return await connection.QueryAsync<Quiz>(new CommandDefinition($"Select * from {QuizTable.TableName};"));
        }

        public async Task<Quiz?> GetQuizByName(string name)
        {
            using var connection = databaseFactory.CreateConnection();
            
            return await connection.QueryFirstOrDefaultAsync<Quiz>(new CommandDefinition($"Select * from {QuizTable.TableName} where {QuizTable.Name} = @name;", new { name })); 
        }


        public async Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz)
        {
            if (quiz is null)
            { 
                return Enumerable.Empty<QuizQuestion>();
            }

            using var connection = databaseFactory.CreateConnection();
            
            return await connection.QueryAsync<QuizQuestion>(new CommandDefinition($"Select * from {QuizQuestionTable.TableName} where {QuizQuestionTable.Quiz} = @quizId;", new { quizId = quiz.Id }));
        }
    }
}
