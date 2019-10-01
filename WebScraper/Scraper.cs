using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;


namespace WebScraper
{
    class Scraper
    {
        public IWebDriver Driver { get; private set; }
        public Navigation navigation { get; set; }

        public Scraper(Navigation nav)
        {
            navigation = nav;
        }
        public void StartScraper()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            using (Driver = new ChromeDriver(options))
            {
                StartNavigation();
            }   
        }

        private void StartNavigation()
        {
            navigation.LaunchBrowser(Driver);
            navigation.Login(Driver);
            navigation.GoToFinancePage(Driver);
            navigation.GetListOfPortfolios(Driver);
            navigation.OpenAPortfolio(Driver);
            navigation.CloseBrowser(Driver);
        }
    }
}
