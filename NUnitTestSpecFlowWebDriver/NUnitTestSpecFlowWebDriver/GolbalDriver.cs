using NUnitTestSpecFlowWebDriver.Steps;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestSpecFlowWebDriver
{
    public static class GolbalDriver
    {
        public static ChromeDriver driver = new ChromeDriver();
        public static ChromeDriver driver2() { return driver; }

    }
}
