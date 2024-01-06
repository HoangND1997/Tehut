﻿using Tehut.Core.Models;
using Tehut.Core.Repositories;

namespace Tehut.Database.Repositories
{
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly IDatabaseFactory databaseFactory;

        public QuizQuestionRepository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Task<QuizQuestion> CreateQuestion(Quiz quiz, string question)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQuestion(QuizQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task EditQuestion(QuizQuestion question, string newQuestion)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuizAnswer>> GetAnswers(QuizQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task SetCorrectAnswer(QuizAnswer answer)
        {
            throw new NotImplementedException();
        }
    }
}
