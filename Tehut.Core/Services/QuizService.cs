using System.Data;
using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository repository;

        public QuizService(IQuizRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Quiz> CreateQuiz(string title)
        {
            if (await repository.DoesQuizNameExists(title))
            {
                throw new DuplicateNameException($"There is already a quiz with the title \"{title}\"! Can not create new quiz.");
            }

            return await repository.CreateQuiz(title);    
        }

        public Task DeleteQuiz(Quiz quiz)
        {
            return repository.DeleteQuiz(quiz);
        }

        public Task EditQuiz(Quiz quiz, string newTitle)
        {
            return repository.EditQuiz(quiz, newTitle); 
        }

        public Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            return repository.GetAllQuizzes();
        }

        public Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz)
        {
            return repository.GetQuestions(quiz); 
        }

        public Task<Quiz?> GetQuizByName(string name)
        {
            return repository.GetQuizByName(name); 
        }
    }
}
