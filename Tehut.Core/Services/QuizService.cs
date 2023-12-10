using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task AddQuestion(QuizQuestion question)
        {
            return repository.AddQuestion(question);
        }

        public Task<Quiz> CreateQuiz(string name)
        {
            return repository.CreateQuiz(name);
        }

        public Task DeleteQuiz(Quiz quiz)
        {
            return repository.DeleteQuiz(quiz);
        }

        public Task<IEnumerable<Quiz>> GetAllQuiz()
        {
            return repository.GetAllQuiz();
        }

        public Task<IEnumerable<Quiz>> GetAllQuiz(string pattern)
        {
            return repository.GetAllQuiz(pattern);
        }

        public Task<Quiz> GetQuizByName(string name)
        {
            return repository.GetQuizByName(name);
        }

        public Task RemoveQuestion(QuizQuestion question)
        {
            return repository.RemoveQuestion(question); 
        }

        public Task<Quiz> RenameQuiz(Quiz quiz, string newName)
        {
            return repository.RenameQuiz(quiz, newName);
        }
    }
}
