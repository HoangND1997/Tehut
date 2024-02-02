using Microsoft.Extensions.DependencyInjection;
using Tehut.Core;
using Tehut.Core.Services;
using Tehut.Database;
using Tehut.Database.Migrator;

namespace Tehut.Integration.Tests.Services
{
    internal class QuizQuestionServiceTests
    {
        private IQuizQuestionService sut; 
        private IQuizService quizService;

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

            quizService = serviceProvider.GetRequiredService<IQuizService>();
            sut = serviceProvider.GetRequiredService<IQuizQuestionService>();
        }

        [Test]
        public async Task QuizQuestionService_WhenAddingQuestions_ShouldBeListedInQuiz()
        {
            var quiz = await quizService.CreateQuiz("Math Quiz");

            var question1 = await sut.CreateQuestion(quiz); 
            var question2 = await sut.CreateQuestion(quiz); 
            var question3 = await sut.CreateQuestion(quiz);

            await quizService.LoadQuestionsFor(quiz); 

            Assert.That(quiz.Questions.Count, Is.EqualTo(3));

            Assert.That(quiz.Questions.All(q => q.Quiz == quiz));
            Assert.That(quiz.Questions.Any(q => q.Id == question1.Id));
            Assert.That(quiz.Questions.Any(q => q.Id == question2.Id));
            Assert.That(quiz.Questions.Any(q => q.Id == question3.Id));
        }

        [Test]
        public async Task QuizQuestionService_WhenAddingQuestionWithData_ShouldBeListedInQuiz()
        {
            var quiz = await quizService.CreateQuiz("Math Quiz");

            var question = await sut.CreateQuestion(quiz);

            question.Question = "What is 1+1?";
            question.Answer1 = "1";
            question.Answer2 = "2";
            question.Answer3 = "3";
            question.Answer4 = "4";
            question.SetCorrectAnswer(1);

            await sut.SaveQuestion(question);

            await quizService.LoadQuestionsFor(quiz);

            Assert.That(quiz.Questions.Count, Is.EqualTo(1));

            Assert.That(quiz.Questions[0].Question, Is.EqualTo("What is 1+1?"));
            Assert.That(quiz.Questions[0].Answer1, Is.EqualTo("1"));
            Assert.That(quiz.Questions[0].Answer2, Is.EqualTo("2"));
            Assert.That(quiz.Questions[0].Answer3, Is.EqualTo("3"));
            Assert.That(quiz.Questions[0].Answer4, Is.EqualTo("4"));
            Assert.That(quiz.Questions[0].CorrectAnswer, Is.EqualTo(1));
        }

        [Test]
        public async Task QuizQuestionService_WhenAddingAndDeleting_ShouldNotListTheQuestionAnymore()
        {
            var quiz = await quizService.CreateQuiz("Math Quiz");

            var question1 = await sut.CreateQuestion(quiz);
            var question2 = await sut.CreateQuestion(quiz);
            var question3 = await sut.CreateQuestion(quiz);

            await quizService.LoadQuestionsFor(quiz);

            Assert.That(quiz.Questions.Count, Is.EqualTo(3));

            await sut.DeleteQuestion(question1);

            await quizService.LoadQuestionsFor(quiz);

            Assert.That(quiz.Questions.Count, Is.EqualTo(2));
            Assert.That(!quiz.Questions.Any(q => q.Id == question1.Id));
        }
    }
}
