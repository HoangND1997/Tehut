using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using Tehut.Core;
using Tehut.Database;
using Tehut.Database.Migrator;
using Tehut.UI.ViewModels;
using Tehut.UI.ViewModels.Services;
using Tehut.UI.ViewModels.Services.Navigation;
using Tehut.UI.Views;
using Tehut.UI.Views.Components;
using Tehut.UI.Views.Dialogs;

namespace Tehut.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }

        private DatabaseConfig databaseConfig; 

        public App()
        {
            var applicationFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var databasePath = Path.Combine(Path.Combine(applicationFolder, "Tehut"), "appdata.db");

            databaseConfig = new DatabaseConfig { DatabasePath = databasePath };

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTehutApplication(); 
            serviceCollection.AddTehutDatabase(databaseConfig);

            RegisterViewModels(serviceCollection);
            RegisterViews(serviceCollection);
            RegisterOtherServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void RegisterViewModels(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<HeaderViewModel>();

            serviceCollection.AddSingleton<QuizOverviewViewModel>();
            serviceCollection.AddSingleton<QuizEditViewModel>();
            serviceCollection.AddSingleton<QuizQuestionEditViewModel>();
            serviceCollection.AddSingleton<QuizQuestionViewModel>();
            serviceCollection.AddSingleton<QuizRunSummaryViewModel>();
        }

        private static void RegisterViews(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(s => new MainWindow { DataContext = s.GetRequiredService<MainViewModel>() });
            serviceCollection.AddSingleton(s => new Header { DataContext = s.GetRequiredService<HeaderViewModel>() }); 

            serviceCollection.AddSingleton(s => new QuizOverviewView { DataContext = s.GetRequiredService<QuizOverviewViewModel>() });
            serviceCollection.AddSingleton(s => new QuizEditView { DataContext = s.GetRequiredService<QuizEditViewModel>() });
            serviceCollection.AddSingleton(s => new QuizQuestionEditView { DataContext = s.GetRequiredService<QuizQuestionEditViewModel>() });
            serviceCollection.AddSingleton(s => new QuizQuestionView { DataContext = s.GetRequiredService<QuizQuestionViewModel>() });
            serviceCollection.AddSingleton(s => new QuizRunSummaryView { DataContext = s.GetRequiredService<QuizRunSummaryViewModel>() });
        }

        private static void RegisterOtherServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHeaderService, HeaderService>(); 
            serviceCollection.AddSingleton<ViewModels.Services.IDialogService, DialogService>(); 
            serviceCollection.AddSingleton<ViewModels.Services.Navigation.INavigationService, NavigationService>();
            serviceCollection.AddSingleton<Func<Type, ViewModelBase>>(s => (viewModelType) => (ViewModelBase)s.GetRequiredService(viewModelType));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            EnsureDatabase(); 

            var navigationService = ServiceProvider?.GetRequiredService<ViewModels.Services.Navigation.INavigationService>();
            navigationService?.NavigateTo<QuizOverviewViewModel>(); 

            var mainWindow = ServiceProvider?.GetRequiredService<MainWindow>();

            mainWindow?.Show();
        }

        private void EnsureDatabase()
        { 
            var databaseDirectoryPath = Path.GetDirectoryName(databaseConfig.DatabasePath);

            if (!Directory.Exists(databaseDirectoryPath))
            { 
                Directory.CreateDirectory(databaseDirectoryPath!);
            }

            var migrator = ServiceProvider?.GetRequiredService<IDatabaseMigrator>();
            migrator?.MigrateUp(); 
        }
    }

}
