using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Tehut.Core;
using Tehut.Core.Services;
using Tehut.Database;
using Tehut.Database.Migrator;

namespace Tehut.Integration.Tests.Services
{
    [TestFixture]
    internal class QuizServiceTests
    {
        private IQuizService sut;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTehutDatabase(new DatabaseConfig { DatabasePath = Guid.NewGuid().ToString(), UseInMemory = true });
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

            createdQuiz.Name = "Greek Gods";

            await sut.SaveQuiz(createdQuiz);

            var allQuizzes = await sut.GetAllQuizzes();

            Assert.That(allQuizzes.Count, Is.EqualTo(1));
            Assert.That(allQuizzes.FirstOrDefault()?.Name, Is.EqualTo("Greek Gods"));
        }

        [Test]
        public async Task QuizService_WhenCreatingQuizzes_ShouldCreateQuizzesWithUniqueId()
        {
            var createdQuiz1 = await sut.CreateQuiz("Egyptian Gods"); 
            var createdQuiz2 = await sut.CreateQuiz("Greek Gods"); 
            var createdQuiz3 = await sut.CreateQuiz("Roman Gods");

            Assert.That(createdQuiz1.Id, Is.EqualTo(1));
            Assert.That(createdQuiz2.Id, Is.EqualTo(2));
            Assert.That(createdQuiz3.Id, Is.EqualTo(3));
        }
    }
}
