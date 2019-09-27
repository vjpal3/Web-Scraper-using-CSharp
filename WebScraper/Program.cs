using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;


namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://login.yahoo.com/");

                IWebElement username = driver.FindElement(By.Id("login-username"));
                username.SendKeys("vrishalipal");
                username.Submit();
               
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login-passwd")));

                string ypwd = ConfigurationManager.AppSettings["ypwd"];
                password.SendKeys(ypwd);
                password.SendKeys(Keys.Return);

                WebDriverWait waitForFinanceLink = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement financeLink = waitForFinanceLink.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Finance")));
                financeLink.Click();
                //driver.FindElement(By.LinkText("Finance")).Click();
                Thread.Sleep(10000);
            }
        }
    }
}
