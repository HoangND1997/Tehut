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
            return await repository.CreateQuiz(title);    
        }

        public Task DeleteQuiz(Quiz quiz)
        {
            return repository.DeleteQuiz(quiz);
        }

        public Task SaveQuiz(Quiz quiz)
        {
            return repository.SaveQuiz(quiz); 
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            var quizzes = await repository.GetAllQuizzes();

            foreach(var quiz in quizzes) 
            {
                await LoadQuestionsFor(quiz);
            }

            return quizzes; 
        }

        public async Task LoadQuestionsFor(Quiz quiz)
        {
            if (quiz == null)
            {
                return; 
            }

            quiz.Questions = (await repository.GetQuestions(quiz)).ToList(); 
        }
    }
}
