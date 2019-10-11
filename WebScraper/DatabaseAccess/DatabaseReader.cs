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
        private List<Company> stocksData = new List<Company>();

        public void GetStocksDataByScrapeID(int scrapeId)
        {
            ScrapeInfo scrapeInfo;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                scrapeInfo = GetScrapeId(connection, scrapeId);

                stocksData = connection.Query<Company, StockData, Company>
                    ("dbo.uspStocksData_Companies_GetByScrapeId @ScrapeId",
                    MapResults,
                    new { ScrapeId = scrapeId }, splitOn: "LastPrice").ToList();
            }
            DisplayStocksData(scrapeInfo);

        }

        private Company MapResults(Company company, StockData stockData)
        {
            company.stockData = stockData;
            return company;
        }

        private void DisplayStocksData(ScrapeInfo scrapeInfo)
        {
            Console.WriteLine("Data scraped on: " + scrapeInfo.ScrapeDate.ToString("MMM, dd yyyy h:mm tt") + " " + scrapeInfo.TimeZone);
            Console.WriteLine();
            foreach (Company data in stocksData)
            {
                Console.Write(data.SymbolName + "  " + data.CompanyName + "\t");
                Console.Write("{0:F2} \t", data.stockData.LastPrice);
                
                var change = data.stockData.Change;
                Console.Write((change > 0) ? "+{0:F2}\t" : "{0:F2}\t", change);

                var percentChange = data.stockData.PercentChange;
                Console.Write((percentChange > 0) ? "+{0:F2}\t" : "{0:F2}\t", percentChange);

                Console.WriteLine(data.stockData.Shares + "\t"
                    + data.stockData.TradeDate?.ToString("MMM, dd yyyy")
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
