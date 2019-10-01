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
            IWebElement username = driver.FindElement(By.Id("login-username"));
            string yUserName = ConfigurationManager.AppSettings["yUserName"];
            username.SendKeys(yUserName);
            username.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login-passwd")));
            string yUserPwd = ConfigurationManager.AppSettings["yUserPwd"];
            password.SendKeys(yUserPwd);
            password.SendKeys(Keys.Return);
        }

        public void GoToFinancePage(IWebDriver driver)
        {
            WebDriverWait waitForFinanceLink = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement financeLink = waitForFinanceLink.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='mega-bottombar']/ul/li[3]/a[@href='https://finance.yahoo.com/']")));

            try
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
                financeLink.Click();
            }
            catch (WebDriverTimeoutException) { }
            finally
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            }
            Console.WriteLine("Title: " + driver.Title);
        }

        public void GetListOfPortfolios(IWebDriver driver)
        {
            WebDriverWait waitForFolioList = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement folioList = waitForFolioList.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/portfolios']")));
            folioList.Click();
        }

        public void OpenAPortfolio(IWebDriver driver)
        {
            WebDriverWait waitForFolio = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement folio = waitForFolio.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("ul[data-test='secnav-list'] li:nth-child(2)>a[title='Solid Folio']")));
            folio.Click();
        }

        public void CloseBrowser(IWebDriver driver)
        {
            Thread.Sleep(10000);
            driver.Quit();
        }

    }
}
