using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Tehut.Core;
using Tehut.Core.Services;
using Tehut.Database;
using Tehut.Database.Migrator;

namespace Tehut.Integration.Tests.Services
{
    [TestFixture]
    internal class QuizServicesTests
    {
        private IQuizService sut;
        private string databasePath = string.Empty; 

        [SetUp]
        public void Setup()
        {
            databasePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "QuizServiceTests_" + Guid.NewGuid().ToString() + ".db");

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTehutDatabase(new DatabaseConfig { DatabasePath = databasePath });
            serviceCollection.AddTehutApplication(); 

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var migrator = serviceProvider.GetRequiredService<IDatabaseMigrator>();
            migrator.MigrateUp(); 

            sut = serviceProvider.GetRequiredService<IQuizService>();
        }

        [TearDown]
        public void TearDown()
        {
           SqliteConnection.ClearAllPools();
           // File.Delete(databasePath); 
        }

        [Test]
        public async Task QuizService_WhenCreatingAnDeletingQuitz_ShouldAddAndRemoveQuizFromDatabase()
        {
            var createdQuiz = await sut.CreateQuiz("Egyptian Gods");

            var allQuizzes = await sut.GetAllQuizzes(); 

            Assert.That(createdQuiz?.Name, Is.EqualTo("Egyptian Gods"));
            Assert.That(allQuizzes.Count, Is.EqualTo(1));
            Assert.That(allQuizzes.FirstOrDefault()?.Name, Is.EqualTo("Egyptian Gods"));

            await sut.DeleteQuiz(createdQuiz);

            var allQuizzesAfterDelete = await sut.GetAllQuizzes();

            Assert.That(allQuizzesAfterDelete.Count, Is.EqualTo(0)); 
        }

        [Test]
        public async Task QuizService_WhenEditingTheNameOfTheQuiz_ShouldChangeTheNameOfTheQuiz()
        {
            var createdQuiz = await sut.CreateQuiz("Egyptian Gods");

            await sut.EditQuiz(createdQuiz, "Greek Gods");

            var allQuizzes = await sut.GetAllQuizzes();

            Assert.That(allQuizzes.Count, Is.EqualTo(1));
            Assert.That(allQuizzes.FirstOrDefault()?.Name, Is.EqualTo("Greek Gods")); 
        }
    }
}
