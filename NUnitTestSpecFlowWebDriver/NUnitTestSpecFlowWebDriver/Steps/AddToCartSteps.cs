using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace NUnitTestSpecFlowWebDriver.Steps
{
    [TestFixture]
    [Order(2)]
    [Binding]
    public class AddToCartSteps
    {

       
        [Test, Order(1)]
        [Given(@"the user click the button Detalles")]
        public void GivenTheUserClickTheButtonDetalles()
        {
            //ScenarioContext.Current.Pending();
            //driver.Manage().Window.Maximize();

            GolbalDriver.driver2().Navigate().GoToUrl("https://localhost:5001/");
            GolbalDriver.driver2().FindElement(By.Id("details")).Click();
            
        }
        [Test, Order(2)]
        [Given(@"the user click the button Añadir al Carrito")]
        public void GivenTheUserClickTheButtonAnadirAlCarrito()
        {
            //ScenarioContext.Current.Pending();
            GolbalDriver.driver2().FindElement(By.Id("add_Cart")).Click();
        }
        [Test, Order(3)]
        [When(@"the user goes click the button cart")]
        public void WhenTheUserGoesClickTheButtonCart()
        {
            //ScenarioContext.Current.Pending();
            GolbalDriver.driver2().FindElement(By.Id("iconCart")).Click();
            
        }
        [Test, Order(4)]
        [Then(@"the game is in the cart details")]
        public void ThenTheGameIsInTheCartDetails()
        {
            //ScenarioContext.Current.Pending();
            GolbalDriver.driver2().FindElement(By.Id("pagar_button")).Click();
            
        }
    }
}
