using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Globalization;
using Dapper;

using WebScraper.DatabaseAccess.Models;
using System.Linq;

namespace WebScraper.DatabaseAccess
{
    class DatabaseWriter
    {
        private readonly List<string> scrapedData;

        public DatabaseWriter(List<string> scrapedData)
        {
            this.scrapedData = scrapedData;
        }

        //public void GetFileData()
        //{
        //    string fullPath = @"e:\Vrishali\stockdata.txt";
        //    scrapedData = new List<string>(File.ReadAllLines(fullPath));
        //}


        public void InsertCompany()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                var companies = new List<Company>();
                
                foreach (var item in scrapedData)
                {
                    var data = item.Split('\t');
                    companies.Add(new Company { SymbolName = data[0].Trim(), CompanyName = data[1].Trim() });
                }
                connection.Execute("dbo.uspCompanies_InsertCompany @SymbolName, @CompanyName", companies);
            }
        }

        public void InsertScrapeInfo()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                var today = DateTime.Today;
                var timeZone = "";
                try
                {
                    var data = scrapedData[0].Split('\t');
                    timeZone = data[5].Split(' ')[1].Trim();
                    var timeString = data[5].Split(' ')[0].Trim();

                    TimeSpan tspan = DateTime.ParseExact(timeString, "h:mmtt", CultureInfo.InvariantCulture).TimeOfDay;
                    
                    today = today.Add(tspan);
                    Console.WriteLine(timeZone);
                    Console.WriteLine(today);
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("Time data not valid"); 
                }
                catch(FormatException)
                {
                    Console.WriteLine("Invalid Time data");
                }

                connection.Execute("dbo.uspScrapesInfo_InsertScrapeInfo @ScrapeDate, @TimeZone", new ScrapeInfo { ScrapeDate = today, TimeZone = timeZone });
            }
        }

        public void InsertStockData()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                var stocksData = new List<StockData>();
                int scrapeId = GetScrapeId(connection);
                Console.WriteLine("scrapeId: " + scrapeId);

                foreach (var item in scrapedData)
                {
                    var data = item.Split('\t');
                 
                    int symbolId = GetSymbolId(connection, data[0].Trim());
                    Console.WriteLine("symbolId: " + symbolId);

                    decimal? lastPrice = ParseDecimalString(data[2].Trim());
                    decimal? change = ParseDecimalString(data[3].Trim());
                    decimal? percentChange = ParseDecimalString(data[4].Trim().TrimEnd('%'));
                    decimal? prevClose = ParseDecimalString(data[6].Trim());
                    decimal? openPrice = ParseDecimalString(data[7].Trim());
                    int? shares = ParseIntString(data[8].Trim());
                    decimal? costBasics = ParseDecimalString(data[9].Trim());
                    DateTime tradeDate1;
                    DateTime? tradeDate = DateTime.TryParse(data[10].Trim(), out tradeDate1) ? tradeDate1
                        : (DateTime?)null;

                    decimal? percentAnnualGain = ParseDecimalString(data[11].Trim().TrimEnd('%'));
                    decimal? fiftyTwoWeekHigh = ParseDecimalString(data[12].Trim());
                    decimal? fiftyTwoWeekLow = ParseDecimalString(data[13].Trim());
                    decimal? bid = ParseDecimalString(data[14].Trim());
                    int? bidSize = ParseIntString(data[15].Trim());
                    decimal? ask = ParseDecimalString(data[16].Trim());
                    int? askSize = ParseIntString(data[17].Trim());
                    decimal? marketCap = ParseMarketCap(data[18].Trim());

                    stocksData.Add(new StockData
                    {
                        ScrapeId = scrapeId,
                        SymbolId = symbolId,
                        LastPrice = lastPrice,
                        Change = change,
                        PercentChange = percentChange,
                        PrevClose = prevClose,
                        OpenPrice = openPrice,
                        Shares = shares,
                        CostBasics = costBasics,
                        TradeDate = tradeDate,
                        PercentAnnualGain = percentAnnualGain,
                        FiftyTwoWeekHigh = fiftyTwoWeekHigh,
                        FiftyTwoWeekLow = fiftyTwoWeekLow,
                        Bid = bid,
                        BidSize = bidSize,
                        Ask = ask,
                        AskSize = askSize,
                        MarketCap = marketCap
                    });
                }
                connection.Execute("dbo.uspStocksData_InsertStockData @ScrapeId, @SymbolId, @LastPrice, @Change, @PercentChange, @PrevClose, @OpenPrice, @Shares, @CostBasics, @TradeDate, @PercentAnnualGain, @FiftyTwoWeekHigh, @FiftyTwoWeekLow, @Bid, @BidSize, @Ask, @AskSize, @MarketCap", stocksData);

            }
        }

        private int GetSymbolId(IDbConnection connection, string symbolName)
        {
            List<Company> companies = connection.Query<Company>("dbo.uspCompanies_GetSymbol @SymbolName", new { SymbolName = symbolName }).ToList();

            return companies[0].Id;
        }

        private int GetScrapeId(IDbConnection connection)
        {
             List<ScrapeInfo> scrapes = connection.Query<ScrapeInfo>("dbo.uspScrapesInfo_GetLatest").ToList();
            return scrapes[0].ScrapeId;
        }

        private decimal? ParseDecimalString(string strDecimal)
        {
            return decimal.TryParse(strDecimal, out decimal result) ? result : (decimal?)null;
        }

        private int? ParseIntString(string strInt)
        {
            return int.TryParse(strInt, out int result) ? result : (int?)null;
        }

        private decimal? ParseMarketCap(string marketCap)
        {
            char capUnit = marketCap[marketCap.Length - 1];
            decimal? marketCapValue = 0;
            switch (capUnit)
            {
                case 'B':
                    marketCapValue = ParseDecimalString(marketCap.TrimEnd('B')) * (decimal?)Math.Pow(10.00, 3.00);
                    break;

                case 'T':
                    marketCapValue = ParseDecimalString(marketCap.TrimEnd('T')) * (decimal?)Math.Pow(10.00, 6.00);
                    break;

                case 'M':
                    marketCapValue = ParseDecimalString(marketCap.TrimEnd('M'));
                    break;

                default:
                    marketCapValue = ParseDecimalString(marketCap);
                    break;
            }
            return marketCapValue;
        }
    }
}

