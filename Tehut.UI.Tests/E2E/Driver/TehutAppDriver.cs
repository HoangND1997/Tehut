using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Tehut.UI.Tests.E2E.Driver
{
    internal class TehutAppDriver
    {
        private readonly IConfigurationRoot configuration; 

        public TehutAppDriver()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("testSettings.json").Build();
        }

        public WindowsDriver<WindowsElement> OpenAppConnection() 
        {
            var winAppDriverUri = configuration.GetSection("WinAppDriver").Value!;

            var appWorkingDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Tehut.UI\bin\Debug\net7.0-windows\win-x64"));
            var appLocation = Path.Combine(appWorkingDir, "Tehut.exe");

            var options = new AppiumOptions();

            options.AddAdditionalCapability("app", appLocation);
            options.AddAdditionalCapability("appWorkingDir", appWorkingDir);
            options.AddAdditionalCapability("appArguments", "inmemory");

            var driver = new WindowsDriver<WindowsElement>(new Uri(winAppDriverUri), options);

            driver.SwitchTo().Window(driver.CurrentWindowHandle);

            return driver; 
        }
    }
}
