using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

namespace Tehut.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        private DatabaseConfig databaseConfig = null!; 

        public App()
        {
            var commandLineArgs = Environment.GetCommandLineArgs().Skip(1).ToList();

            var useInMemory = commandLineArgs.FirstOrDefault()?.ToLower() is "inmemory";

            var applicationFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var databasePath = useInMemory ? Guid.NewGuid().ToString() : Path.Combine(Path.Combine(applicationFolder, "Tehut"), "appdata.db");

            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(serviceCollection =>
                {

                    databaseConfig = new DatabaseConfig { DatabasePath = databasePath, UseInMemory = useInMemory};

                    serviceCollection.AddTehutApplication();
                    serviceCollection.AddTehutDatabase(databaseConfig);

                    RegisterViewModels(serviceCollection);
                    RegisterViews(serviceCollection);
                    RegisterOtherServices(serviceCollection);

                }).Build();
        }

        private static void RegisterViewModels(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<HeaderViewModel>();

            serviceCollection.AddSingleton<QuizOverviewViewModel>();
            serviceCollection.AddSingleton<QuizEditViewModel>();
            serviceCollection.AddSingleton<QuizQuestionEditViewModel>();
            serviceCollection.AddSingleton<QuizRunViewModel>();
            serviceCollection.AddSingleton<QuizRunSummaryViewModel>();
        }

        private static void RegisterViews(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(s => new MainWindow { DataContext = s.GetRequiredService<MainViewModel>() });
            serviceCollection.AddSingleton(s => new Header { DataContext = s.GetRequiredService<HeaderViewModel>() }); 

            serviceCollection.AddSingleton(s => new QuizOverviewView { DataContext = s.GetRequiredService<QuizOverviewViewModel>() });
            serviceCollection.AddSingleton(s => new QuizEditView { DataContext = s.GetRequiredService<QuizEditViewModel>() });
            serviceCollection.AddSingleton(s => new QuizQuestionEditView { DataContext = s.GetRequiredService<QuizQuestionEditViewModel>() });
            serviceCollection.AddSingleton(s => new QuizRunView { DataContext = s.GetRequiredService<QuizRunViewModel>() });
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
            AppHost!.Start(); 

            EnsureDatabase(); 

            var navigationService = AppHost!.Services.GetRequiredService<ViewModels.Services.Navigation.INavigationService>();

            navigationService?.NavigateTo<QuizOverviewViewModel>(); 

            var mainWindow = AppHost!.Services.GetRequiredService<MainWindow>();

            mainWindow?.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppHost!.Dispose();

            base.OnExit(e);
        }

        private void EnsureDatabase()
        { 
            var databaseDirectoryPath = Path.GetDirectoryName(databaseConfig.DatabasePath);

            if (!databaseConfig.UseInMemory && !Directory.Exists(databaseDirectoryPath))
            { 
                Directory.CreateDirectory(databaseDirectoryPath!);
            }

            AppHost!.Services.GetRequiredService<IDatabaseMigrator>().MigrateUp();
        }
    }
}
