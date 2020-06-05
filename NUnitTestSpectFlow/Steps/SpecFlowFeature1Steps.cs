using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace NUnitTestSpectFlow.Steps
{
    [TestFixture]
    [Binding]
    public class SpecFlowFeature1Steps
    {

        IWebDriver driver = new ChromeDriver();
        [Test]
        [Given(@"Open the Chrome and launch the application")]
        public void GivenOpenTheChromeAndLaunchTheApplication()
        {
            //ScenarioContext.Current.Pending();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
        }
        [Test]
        [When(@"Enter the Email and Password")]
        public void WhenEnterTheEmailAndPassword()
        {
            //ScenarioContext.Current.Pending();
            driver.FindElement(By.Id("Input_Email")).SendKeys("braulio123@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Passw0rd$");
        }
        [Test]
        [Then(@"the result should be the user logged")]
        public void ThenTheResultShouldBeTheUserLogged()
        {
            //ScenarioContext.Current.Pending();
            driver.FindElement(By.Id("login_button")).Click();
        }
    }
}
