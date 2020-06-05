using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowBrander
{
    [Binding]
    public class LoginSteps
    {
        IWebDriver driver = new ChromeDriver();
        
        [Given(@"Open the Chrome and launch the application")]
        public void GivenOpenTheChromeAndLaunchTheApplication()
        {
            //ScenarioContext.Current.Pending();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
        }
        
        [When(@"Enter the Email and Password")]
        public void WhenEnterTheEmailAndPassword()
        {
            //ScenarioContext.Current.Pending();
            driver.FindElement(By.Id("Input_Email")).SendKeys("braulio123@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Passw0rd$");
        }
        
        [Then(@"the result should be the user logged")]
        public void ThenTheResultShouldBeTheUserLogged()
        {
            //ScenarioContext.Current.Pending();
            driver.FindElement(By.Id("login_button")).Click();
        }
    }
}
