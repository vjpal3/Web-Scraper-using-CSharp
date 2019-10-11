using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WebScraper.DatabaseAccess.Models;

namespace WebScraper.DatabaseAccess
{
    class DatabaseReader
    {
        private List<StockDataCompany> stocksData = new List<StockDataCompany>();

        public void GetStocksDataByScrapeID(int scrapeId)
        {
            ScrapeInfo scrapeInfo;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                scrapeInfo = GetScrapeId(connection, scrapeId);

                stocksData = connection.Query<Company, StockData, StockDataCompany>
                    ("dbo.uspStocksData_Companies_GetByScrapeId @ScrapeId",
                    MapResults,
                    new { ScrapeId = scrapeId }, splitOn: "LastPrice").ToList();
            }
            DisplayStocksData(scrapeInfo);
        }

        private StockDataCompany MapResults(Company company, StockData stockData)
        {
            var stockDataCompany = new StockDataCompany
            {
                ScrapedStockData = stockData,
                ScrapedCompanyData = company
            };
            return stockDataCompany;
        }

        private void DisplayStocksData(ScrapeInfo scrapeInfo)
        {
            Console.WriteLine("Data scraped on: " + scrapeInfo.ScrapeDate.ToString("MMM, dd yyyy h:mm tt") + " " + scrapeInfo.TimeZone);
            Console.WriteLine();
            foreach (StockDataCompany data in stocksData)
            {
                Console.Write(data.ScrapedCompanyData.SymbolName + "  " + data.ScrapedCompanyData.CompanyName + "\t");
                Console.Write("{0:F2} \t", data.ScrapedStockData.LastPrice);
                
                var change = data.ScrapedStockData.Change;
                Console.Write((change > 0) ? "+{0:F2}\t" : "{0:F2}\t", change);

                var percentChange = data.ScrapedStockData.PercentChange;
                Console.Write((percentChange > 0) ? "+{0:F2}\t" : "{0:F2}\t", percentChange);

                Console.WriteLine(data.ScrapedStockData.Shares + "\t"
                    + data.ScrapedStockData.TradeDate?.ToString("MMM, dd yyyy")
                );
            }
            Console.WriteLine();
        }

        private ScrapeInfo GetScrapeId(IDbConnection connection, int scrapeId)
        {
            List<ScrapeInfo> scrapes = connection.Query<ScrapeInfo>("dbo.uspScrapesInfo_GetByScrapeId @ScrapeId", new { ScrapeId = scrapeId }).ToList();
            return scrapes[0];
        }
    }
}
