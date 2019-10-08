using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;
using OpenQA.Selenium.Interactions;
using WebScraper.DatabaseAccess;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //var scraper = new Scraper(new Navigation(), new StockDataCollection());
            //scraper.StartScraper();

            new DatabaseWriter().TestDataBaseConection();
        }
    }
}
