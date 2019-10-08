using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;
using System.IO;

namespace WebScraper
{
    class StockDataCollection
    {
        
        private readonly StringBuilder stockData = new StringBuilder();
        public void AccessTableData(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table")));

            ScrapeDataHeader(driver);
            ScrapeStockData(driver);
            SaveDataToFile(); 
        }

        public void SaveDataToFile()
        {
            string fullPath = @"e:\Vrishali\stockdata.txt";
            File.WriteAllText(fullPath, stockData.ToString());
        }


        private void ScrapeDataHeader(IWebDriver driver)
        {
            int headerColCount = driver.FindElements(By.XPath("//table/thead/tr/th")).Count;
            for (int i = 1; i <= headerColCount - 1; i++)
            {
                string headerData = driver.FindElement(By.XPath("//body//th[" + i + "]")).Text + "\t";
                //Console.Write(headerData);
                stockData.Append(headerData);
            }
            //Console.WriteLine("\n");
            stockData.Append("\n");
        }

        private void ScrapeStockData(IWebDriver driver)
        {
            int rowCount = driver.FindElements(By.XPath("//table/tbody/tr")).Count;
            int columnCount = driver.FindElements(By.XPath("//table/tbody/tr[1]//td")).Count;
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount - 2; j++)
                {
                    string cellData = driver.FindElement(By.XPath("//table[@class='W(100%)']/tbody/tr[" + i + "]/td[" + j + "]")).Text + "\t";
                    //Console.Write(cellData);
                    stockData.Append(cellData);
                }
                //Console.WriteLine();
                stockData.Append("\n");
            }
        }
    }
}
