﻿using System.Data.SqlTypes;
using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public  interface IQuizRepository
    {
        #region  Write Methods 

        /// <summary>
        /// Creates a new quiz in the database with the given name. 
        /// </summary>
        /// <param name="name">The name of the new quiz.</param>
        /// <returns>The new created quiz</returns>
        Task<Quiz> CreateQuiz(string name);

        /// <summary>
        /// Edits the name of the given quiz to the new given name in the database. 
        /// </summary>
        /// <param name="quiz">The quiz to be edited.</param>
        /// <param name="newName">The new name of the quiz.</param>
        Task EditQuiz(Quiz quiz, string newName);

        /// <summary>
        /// Deletes the quiz from the database. 
        /// </summary>
        /// <param name="quiz">The quiz to be deleted.</param>
        Task DeleteQuiz(Quiz quiz);

        #endregion


        #region Search Methods

        /// <summary>
        /// Gets all the quizzes available in the database. 
        /// </summary>
        /// <returns>All available quizzes.</returns>
        Task<IEnumerable<Quiz>> GetAllQuizzes();

        /// <summary>
        /// Gets a quiz by its name from the database.
        /// </summary>
        /// <param name="name">The name to search for in a quiz.</param>
        /// <returns>The quiz with the given name or null if not existing.</returns>
        Task<Quiz?> GetQuizByName(string name);

        /// <summary>
        /// Checks whether or not the given name already exists as a name for a quiz. 
        /// </summary>
        /// <param name="name">The name to check for existence.</param>
        /// <returns>True, if there is already a quiz with the given name.</returns>
        Task<bool> DoesQuizNameExists(string name); 

        #endregion

        /// <summary>
        /// Gets all the questions from the database that are belonging to the quiz.
        /// </summary>
        /// <param name="quiz">The quiz from which the questions should be retrieved.</param>
        /// <returns>All questions belonging to the quiz.</returns>
        Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz);
    }
}
