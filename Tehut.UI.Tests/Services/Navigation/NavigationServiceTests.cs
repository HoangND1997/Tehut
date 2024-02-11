using DevExpress.Mvvm; 
using NSubstitute;
using Tehut.UI.ViewModels.Services.Navigation;

namespace Tehut.UI.Tests.Services.Navigation
{
    public abstract class Page1 : ViewModelBase, INavigationPage
    {
        public abstract Task OnEnterPage(NavigationInformation navigationInformation);
        public abstract Task OnExitPage<T>(T nextView) where T : ViewModelBase;
    }

    public abstract class Page2 : ViewModelBase, INavigationPage
    {
        public abstract Task OnEnterPage(NavigationInformation navigationInformation);
        public abstract Task OnExitPage<T>(T nextView) where T : ViewModelBase;
    }

    public abstract class Page3 : ViewModelBase, INavigationPage
    {
        public abstract Task OnEnterPage(NavigationInformation navigationInformation);
        public abstract Task OnExitPage<T>(T nextView) where T : ViewModelBase;
    }

    internal class NavigationServiceTests
    {
        private ViewModels.Services.Navigation.INavigationService sut;

        private Page1 page1; 
        private Page2 page2;
        private Page3 page3;

        [SetUp]
        public void Setup()
        {
            page1 = Substitute.For<Page1>();  
            page2 = Substitute.For<Page2>();  
            page3 = Substitute.For<Page3>();  

            sut = new NavigationService(GetPage); 
        }

        private ViewModelBase GetPage(Type pageType)
        {
            if (pageType == typeof(Page1))
            {
                return page1;
            }
            else if (pageType == typeof(Page2))
            {
                return page2;
            }
            else if (pageType == typeof(Page3))
            {
                return page3;
            }

            return null!; 
        }

        [Test]
        public async Task NavigateTo_GivenTwoPages_WhenNavigationBackwardsAndForwards_ShouldReturnToFirstPageAndThenSecondPage()
        { 
            await sut.NavigateTo<Page1>();
            await sut.NavigateTo<Page2>();

            await sut.NavigateToPreviousPage();

            Assert.That(sut.CurrentView, Is.EqualTo(page1));

            await sut.NavigateToNextPage();

            Assert.That(sut.CurrentView, Is.EqualTo(page2));
        }

        [Test]
        public async Task NavigateTo_GivenTwoPages_WhenNavigationFromOneToTheOther_ShouldCallEnterAndExitMethods()
        {
            var customNavigationInformation = new NavigationInformation();

            await sut.NavigateTo<Page1>(customNavigationInformation);
            await sut.NavigateTo<Page2>();

            await page1.Received().OnEnterPage(customNavigationInformation);
            await page1.Received().OnExitPage(); 

            await page2.Received().OnEnterPage(NavigationInformation.Empty);
        }

        [Test]
        public async Task NavigateTo_GivenThreePages_WhenNavigatingBackwardsAndThenToNewPage_ShouldOverwriteHistory()
        {
            await sut.NavigateTo<Page1>(); 
            await sut.NavigateTo<Page2>();

            await sut.NavigateToPreviousPage(); 
            await sut.NavigateTo<Page3>();

            await sut.NavigateToPreviousPage();
            await sut.NavigateToNextPage();

            Assert.That(sut.CurrentView, Is.EqualTo(page3)); 
        }

        [Test]
        public async Task NavigateTo_GivenTwoPages_WhenNavigatingForwards_ShouldCallEnterWithPreviousNavigationInformation()
        {
            var customNavigationInformation1 = new NavigationInformation();
            var customNavigationInformation2 = new NavigationInformation();

            await sut.NavigateTo<Page1>(customNavigationInformation1);
            await sut.NavigateTo<Page2>(customNavigationInformation2);

            await sut.NavigateToPreviousPage();
            await sut.NavigateToNextPage();

            await page1.Received().OnEnterPage(customNavigationInformation1);
            await page2.Received().OnEnterPage(customNavigationInformation2);
        }

        [Test]
        public async Task CanNavigateToPrevious_GivenTwoPages_ShouldReturnTrue()
        {
            await sut.NavigateTo<Page1>(); 
            await sut.NavigateTo<Page2>();

            Assert.That(sut.CanNavigateToPreviousPage(), Is.True);
            Assert.That(sut.CanNavigateToNextPage(), Is.False); 
        }

        [Test]
        public async Task CanNavigateToPrevious_GivenTwoPagesAndThenNavigatingBackwards_ShouldReturnFalse()
        {
            await sut.NavigateTo<Page1>();
            await sut.NavigateTo<Page2>();

            await sut.NavigateToPreviousPage(); 

            Assert.That(sut.CanNavigateToPreviousPage(), Is.False);
            Assert.That(sut.CanNavigateToNextPage(), Is.True);
        }
    }
}
