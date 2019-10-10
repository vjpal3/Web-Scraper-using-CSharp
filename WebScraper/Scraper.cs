using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;
using System.Collections.Generic;

namespace WebScraper
{
    class Scraper
    {
        public IWebDriver Driver { get; private set; }
        public Navigation WebNavigation { get; set; }
        public StockDataCollection DataCollection { get; set; }
        private List<string> stockData = new List<string>();

        public Scraper(Navigation navigation, StockDataCollection collection)
        {
            WebNavigation = navigation;
            DataCollection = collection;
        }

        public List<string> GetScraperData()
        {
            return stockData;
        }
        public void StartScraper()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            using (Driver = new ChromeDriver(options))
            {
                StartNavigation();
                StartDataCollection();
                StopScraper();
            }   
        }

        private void StopScraper()
        {
            WebNavigation.CloseBrowser(Driver);
        }

        private void StartDataCollection()
        {
            DataCollection.AccessTableData(Driver);
            stockData = DataCollection.GetStockData();
        }

        private void StartNavigation()
        {
            WebNavigation.LaunchBrowser(Driver);
            WebNavigation.Login(Driver);
            WebNavigation.GoToFinancePage(Driver);
            WebNavigation.GetListOfPortfolios(Driver);
            WebNavigation.OpenAPortfolio(Driver);

            
        }
    }
}
