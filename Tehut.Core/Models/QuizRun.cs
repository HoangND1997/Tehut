namespace Tehut.Core.Models
{
    internal class QuizRun
    {
        public Quiz? Quiz { get; set; }

        public Dictionary<QuizQuestion, int> UserAnswerForEachQuestion { get; } = new(); 
    }
}
