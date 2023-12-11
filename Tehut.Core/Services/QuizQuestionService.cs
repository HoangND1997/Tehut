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
            return repository.CreateQuestion(quiz, question);
        }

        public Task DeleteQuestion(QuizQuestion question)
        {
            return repository.DeleteQuestion(question);
        }

        public Task EditQuestion(QuizQuestion question, string newQuestion)
        {
            return repository.EditQuestion(question, newQuestion);  
        }

        public Task<IEnumerable<QuizAnswer>> GetAnswers(QuizQuestion question)
        {
            return repository.GetAnswers(question); 
        }

        public Task SetCorrectAnswer(QuizAnswer answer)
        {
            return repository.SetCorrectAnswer(answer);
        }
    }
}
