using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;

namespace WebScraper
{
    class Navigation
    {
        public void LaunchBrowser(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://login.yahoo.com/");
        }

        public void Login(IWebDriver driver)
        {
            string username = "login-username";
            EnterUserCredentials(driver, username);

            string userPasswd = "login-passwd";
            EnterUserCredentials(driver, userPasswd);
        }

        private static void EnterUserCredentials(IWebDriver driver, string userValue)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(userValue)));
            string userCredential = ConfigurationManager.AppSettings[userValue];
            element.SendKeys(userCredential);
            element.SendKeys(Keys.Return);
        }

        public void GoToFinancePage(IWebDriver driver)
        {
            WebDriverWait waitForFinanceLink = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            
            try
            {
                driver.Navigate().GoToUrl("https://finance.yahoo.com");
            }
            catch (WebDriverException)
            {
                Console.WriteLine("Yahoo finance Page did not load within 20 seconds!");
            }
            Console.WriteLine("Title: " + driver.Title);
        }

        public void GetListOfPortfolios(IWebDriver driver)
        {
            WebDriverWait waitForFolioList = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement folioList = waitForFolioList.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/portfolios']")));
            folioList.Click();
        }

        public void OpenAPortfolio(IWebDriver driver)
        {
            WebDriverWait waitForFolio = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement folio = waitForFolio.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("ul[data-test='secnav-list'] li:nth-child(2)>a[title='Solid Folio']")));
            folio.Click();

            WebDriverWait waitForCustomView = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_3/view/view_4");
        }

        public void CloseBrowser(IWebDriver driver)
        {
            Thread.Sleep(10000);
            driver.Quit();
        }
    }
}