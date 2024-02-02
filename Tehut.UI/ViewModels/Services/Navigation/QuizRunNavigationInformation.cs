using Tehut.Core.Models;

namespace Tehut.UI.ViewModels.Services.Navigation
{
    internal class QuizRunNavigationInformation : NavigationInformation
    {
        public QuizQuestion? QuestionToShow { get; set; }
    }
}
