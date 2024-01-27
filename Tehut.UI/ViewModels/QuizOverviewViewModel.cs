using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Tehut.Core.Models;
using Tehut.UI.ViewModels.Entities;

namespace Tehut.UI.ViewModels
{
    public class QuizOverviewViewModel : ViewModelBase
    {
        public ObservableCollection<QuizCardViewModel> Quizzes { get; } = new();

        public QuizOverviewViewModel()
        {
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 0, Name = "Egyptian Gods" })); 
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 1, Name = "Greek Gods" })); 
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 2, Name = "Roman Gods" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 3, Name = "U.S. Presidents" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 4, Name = "Basic Chemistry" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 5, Name = "German History" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 6, Name = "Swiss Mountains" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 7, Name = "Football" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 8, Name = "Coding" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 9, Name = "World Capitals" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 10, Name = "Animal Kingdom" }));
            Quizzes.Add(new QuizCardViewModel(new Quiz { Id = 11, Name = "Architecture" }));
        }
    }
}
