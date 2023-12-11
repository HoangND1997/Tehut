using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public interface IQuizQuestionRepository
    {
        #region Question Handling 

        /// <summary>
        /// Creates a new question in the database for the given quiz. 
        /// </summary>
        /// <param name="quiz">The quiz to which the questions is added.</param>
        /// <param name="question">The text of the question to be created.</param>
        /// <returns>The newly created question.</returns>
        Task<QuizQuestion> CreateQuestion(Quiz quiz, string question);

        /// <summary>
        /// Edits the text of the given question in the database to the new given text.
        /// </summary>
        /// <param name="question">The question to be edited.</param>
        /// <param name="newQuestion">The new question text.</param>
        Task EditQuestion(QuizQuestion question, string newQuestion);

        /// <summary>
        /// Deletes the quiz question from the database and removed the reference to the belonging quiz.
        /// </summary>
        /// <param name="question">The question to be removed.</param>
        Task DeleteQuestion(QuizQuestion question); 

        #endregion


        #region Answer Handling 

        /// <summary>
        /// Gets all the answers from the database for the given question.
        /// </summary>
        /// <param name="question">The question from which the answers are retrieved.</param>
        /// <returns>All answers belonging to the question.</returns>
        Task<IEnumerable<QuizAnswer>> GetAnswers(QuizQuestion question);

        /// <summary>
        /// Sets the given answer as the correct answer for the attached question.
        /// </summary>
        /// <param name="answer">The answer to be set as correct.</param>
        Task SetCorrectAnswer(QuizAnswer answer);

        #endregion 
    }
}
