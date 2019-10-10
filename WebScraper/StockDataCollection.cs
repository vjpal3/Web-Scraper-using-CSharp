using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace WebScraper
{
    class StockDataCollection
    {
        
        //private readonly StringBuilder stockData = new StringBuilder();
        private readonly List<string> stockData = new List<string>();
        public void AccessTableData(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table")));

            //ScrapeDataHeader(driver);
            ScrapeStockData(driver);
            //SaveDataToFile(); 
        }

        public List<string> GetStockData()
        {
            return stockData;
        }


        private void ScrapeDataHeader(IWebDriver driver)
        {
            int headerColCount = driver.FindElements(By.XPath("//table/thead/tr/th")).Count;
            string headerData = "";
            for (int i = 1; i <= headerColCount - 1; i++)
            {
                headerData += driver.FindElement(By.XPath("//body//th[" + i + "]")).Text + "\t";
                //stockData.Append(headerData);
            }
            stockData.Add(headerData);
        }

        private void ScrapeStockData(IWebDriver driver)
        {
            int rowCount = driver.FindElements(By.XPath("//table/tbody/tr")).Count;
            int columnCount = driver.FindElements(By.XPath("//table/tbody/tr[1]//td")).Count;
            for (int i = 1; i <= rowCount; i++)
            {
                string cellData = "";
                for (int j = 1; j <= columnCount - 2; j++)
                {
                    cellData += driver.FindElement(By.XPath("//table[@class='W(100%)']/tbody/tr[" + i + "]/td[" + j + "]")).Text + "\t";
                }
                stockData.Add(cellData);
            }
        }
    }
}
