using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Configuration;
using OpenQA.Selenium.Interactions;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var navigation = new Navigation();
            var scraper = new Scraper(navigation);
            scraper.StartScraper(); 

            
        }
    }
}
