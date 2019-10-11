using WebScraper.DatabaseAccess;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //var scraper = new Scraper(new Navigation(), new StockDataCollection());
            //scraper.StartScraper();

            //var dbWriter = new DatabaseWriter(scraper.GetScraperData());
            //dbWriter.GetFileData();
            //dbWriter.InsertCompany();
            //dbWriter.InsertScrapeInfo();
            //dbWriter.InsertStockData();

            var dbReader = new DatabaseReader();
            var scrapeId = 1;
            dbReader.GetStocksDataByScrapeID(scrapeId);
        }
    }
}
