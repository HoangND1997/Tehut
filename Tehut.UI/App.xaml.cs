using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using Tehut.Core;
using Tehut.Database;
using Tehut.UI.Views;

namespace Tehut.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider; 

        public App()
        {
            var databasePath = Path.Combine(Path.Combine(Path.GetTempPath(), "Tehut"), "quiz.db"); 

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTehutApplication(); 
            serviceCollection.AddTehutDatabase(new DatabaseConfig { DatabasePath = databasePath });

            serviceCollection.AddSingleton(s => new MainWindow()); 

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show(); 
        }
    }

}
