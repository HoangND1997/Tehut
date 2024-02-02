using Dapper;
using Tehut.Core.Models;
using Tehut.Core.Repositories;
using Tehut.Database.Schemas;

namespace Tehut.Database.Repositories
{
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly IDatabaseFactory databaseFactory;

        public QuizQuestionRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public async Task<QuizQuestion> CreateQuestion(Quiz quiz)
        {
            if (quiz == null)
            {
                return null!; 
            }

            using var connection = databaseFactory.CreateConnection();

            var questionId = await connection.QuerySingleAsync<int>(new CommandDefinition($"Insert into {QuizQuestionTable.TableName} ({QuizQuestionTable.Quiz}) Values (@quizId) Returning {QuizQuestionTable.Id};", new { quizId = quiz.Id }));

            return new QuizQuestion
            {
                Id = questionId,
                Quiz = quiz,
            };
        }

        public async Task SaveQuestion(QuizQuestion question)
        {
            if (question == null)
            {
                return;
            }

            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Update {QuizQuestionTable.TableName} Set " + 
                                                                $"{QuizQuestionTable.Question} = @question," +
                                                                $"{QuizQuestionTable.Answer1} = @answer1," +
                                                                $"{QuizQuestionTable.Answer2} = @answer2," +
                                                                $"{QuizQuestionTable.Answer3} = @answer3," +
                                                                $"{QuizQuestionTable.Answer4} = @answer4," +
                                                                $"{QuizQuestionTable.CorrectAnswer} = @correctAnswer " + 
                                                                $"Where {QuizQuestionTable.Id} = @id;", new
                                                                {
                                                                    question = question.Question,
                                                                    answer1 = question.Answer1,
                                                                    answer2 = question.Answer2,
                                                                    answer3 = question.Answer3,
                                                                    answer4 = question.Answer4,
                                                                    correctAnswer = question.CorrectAnswer,
                                                                    id = question.Id
                                                                }));

        }

        public async Task DeleteQuestion(QuizQuestion question)
        {
            if (question == null)
            {
                return; 
            }

            using var connection = databaseFactory.CreateConnection();

            await connection.ExecuteAsync(new CommandDefinition($"Delete from {QuizQuestionTable.TableName} Where {QuizQuestionTable.Id} = @id", new { id = question.Id }));
        }
    }
}
