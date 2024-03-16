using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using Tehut.UI.Tests.E2E.Driver;
using Tehut.UI.ViewModels.Actions;
using Tehut.UI.Views.Automation;

namespace Tehut.UI.Tests.E2E.StepDefinitions.Quiz
{
    [Binding]
    [Scope(Tag = "QuizEditing")]
    internal class QuizEditNameStepDefinitions
    {
        private WindowsDriver<WindowsElement> driver = null!; 
        private WebDriverWait wait = null!;

        [BeforeScenario]
        public void Setup()
        { 
            driver = new TehutAppDriver().OpenAppConnection();
            wait = new(driver, TimeSpan.FromSeconds(3));
        }

        [Given(@"a new quiz in the list")]
        public void GivenANewQuizInTheList()
        {
            driver.FindElementByAccessibilityId(QuizOverviewAutoNames.AdderCard).Click(); 
        }

        [When(@"clicking on the edit icon in the quiz card")]
        public void WhenClickingOnTheEditIconInTheQuizCard()
        {
            wait.Until(_ => driver.FindElementByAccessibilityId(QuizCardAutoNames.EditButton)).Click(); 
        }

        [When(@"clicking on the edit icon in the action ribbon in the quiz view")]
        public void WhenClickingOnTheEditIconInTheActionRibbonInTheQuizView()
        {
            wait.Until(_ => driver.FindElementByAccessibilityId(ActionBarAutoNames.ActionBarName(ActionBarType.Edit))).Click(); 
        }

        [When(@"writing ""([^""]*)"" in the edit field of the quiz edit window")]
        public void WhenWritingInTheEditFieldOfTheQuizEditWindow(string quizName)
        {
            wait.Until(_ => driver.FindElementByAccessibilityId(TextEditDialogAutoNames.TextField)).SendKeys(quizName);
        }

        [When(@"pressing the confirm button on the quiz edit window")]
        public void WhenPressingTheConfirmButtonOnTheQuizEditWindow()
        {
            driver.FindElementByAccessibilityId(TextEditDialogAutoNames.ConfirmButton).Click();
        }

        [Then(@"quiz name in the header title should show ""([^""]*)""")]
        public void ThenQuizNameInTheNavigationTitleShouldShow(string expectedTitle)
        {
            var headerTitle = wait.Until(_ => driver.FindElementByAccessibilityId(HeaderAutoNames.Title));

            Assert.That(headerTitle.Text, Is.EqualTo(expectedTitle));        
        }

        [AfterScenario]
        public void TearDown()
        {
            driver.Close(); 
        }
    }
}
