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
        public IWebDriver Driver { get; private set; }
        public void StartScraper()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            using (Driver = new ChromeDriver(options))
            {
                LaunchBrowser();
                Login();
                GoToFinancePage();
                GetListOfPortfolios();
                OpenAPortfolio();
                CloseBrowser();
            }
        }

        private void OpenAPortfolio()
        {
            WebDriverWait waitForFolio = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement folio = waitForFolio.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("ul[data-test='secnav-list'] li:nth-child(2)>a[title='Solid Folio']")));
            folio.Click();
        }

        private void GetListOfPortfolios()
        {
            WebDriverWait waitForFolioList = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement folioList = waitForFolioList.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/portfolios']")));
            folioList.Click();
        }

        private void LaunchBrowser()
        {
            Driver.Navigate().GoToUrl("https://login.yahoo.com/");
        }

        private void Login()
        {
            IWebElement username = Driver.FindElement(By.Id("login-username"));
            string yUserName = ConfigurationManager.AppSettings["yUserName"];
            username.SendKeys(yUserName);
            username.Submit();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login-passwd")));
            string yUserPwd = ConfigurationManager.AppSettings["yUserPwd"];
            password.SendKeys(yUserPwd);
            password.SendKeys(Keys.Return);
        }

        private void GoToFinancePage()
        {
            WebDriverWait waitForFinanceLink = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement financeLink = waitForFinanceLink.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='mega-bottombar']/ul/li[3]/a[@href='https://finance.yahoo.com/']")));

            try
            {
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
                financeLink.Click();
            }
            catch (WebDriverTimeoutException) { }
            finally
            {
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            }
            Console.WriteLine("Title: " + Driver.Title);
        }

        private void CloseBrowser()
        {
            Thread.Sleep(10000);
            Driver.Quit();
        }

    }
}
