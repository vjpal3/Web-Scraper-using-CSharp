using WebScraper.DatabaseAccess;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //var scraper = new Scraper(new Navigation(), new StockDataCollection());
            //scraper.StartScraper();

            var dbWriter = new DatabaseWriter();
            dbWriter.GetFileData();
            //dbWriter.InsertCompany();
            dbWriter.InsertScrapeInfo();
        }
    }
}
