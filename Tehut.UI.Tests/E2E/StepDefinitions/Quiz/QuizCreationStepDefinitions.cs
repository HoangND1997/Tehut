using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using Tehut.UI.Tests.E2E.Driver;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.Views.Automation;

namespace Tehut.UI.Tests.E2E.StepDefinitions.Quiz
{
    [Binding]
    [Scope(Tag = "QuizCreation")]
    internal class QuizCreationStepDefinitions
    {
        private WindowsDriver<WindowsElement> driver = null!;
        private WebDriverWait wait = null!;

        [BeforeScenario]
        public void Setup()
        {
            driver = new TehutAppDriver().OpenAppConnection();

            wait = new(driver, TimeSpan.FromSeconds(2));
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
        }

        [When(@"clicking on the add-quiz card element")]
        public void WhenClickingOnTheAdd_QuizCardElement()
        {
            driver.FindElementByAccessibilityId(QuizOverviewAutoNames.AdderCard).Click();
        }

        [When(@"clicking the plus icon in the action ribbon")]
        public void WhenClickingThePlusIconInTheActionRibbon()
        {
            driver.FindElementByAccessibilityId(ActionBarAutoNames.ActionBarName(ActionBarType.Add)).Click(); 
        }

        [Then(@"a new quiz is added to the quiz list")]
        public void ThenANewQuizIsAddedToTheQuizList()
        {
            var quizCards = wait.Until(_ => 
            {
                var quizzes = driver.FindElementsByClassName(QuizCardAutoNames.QuizCard);

                if (!quizzes.Any())
                {
                    throw new WebDriverException();
                }

                return quizzes;
            });
        
            Assert.That(quizCards.Count(), Is.EqualTo(1));  
        }

        [AfterScenario]
        public void TearDown() 
        {
            driver.Close(); 
        }
    }
}
