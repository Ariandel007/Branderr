using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace NUnitTestSpecFlowWebDriver.Steps
{
    [TestFixture]
    [Order(1)]
    [Binding]
    public class SpecFlowFeature1Steps
    {
       
        [Test,Order(1)]
        [Given(@"Open the Chrome and launch the application")]
        public void GivenOpenTheChromeAndLaunchTheApplication()
        {
            //ScenarioContext.Current.Pending();
            GolbalDriver.driver2().Manage().Window.Maximize();
            GolbalDriver.driver2().Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
        }
        [Test,Order(2)]
        [When(@"Enter the Email and Password")]
        public void WhenEnterTheEmailAndPassword()
        {
            //ScenarioContext.Current.Pending();
            GolbalDriver.driver2().FindElement(By.Id("Input_Email")).SendKeys("braulio123@gmail.com");
            GolbalDriver.driver2().FindElement(By.Id("Input_Password")).SendKeys("Passw0rd$");
        }
        [Test,Order(3)]
        [Then(@"the result should be the user logged")]
        public void ThenTheResultShouldBeTheUserLogged()
        {
            //ScenarioContext.Current.Pending();
            GolbalDriver.driver2().FindElement(By.Id("login_button")).Click();
        }
    }
}
