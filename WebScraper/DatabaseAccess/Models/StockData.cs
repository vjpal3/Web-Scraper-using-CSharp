using System;

namespace WebScraper.DatabaseAccess.Models
{
    class StockData
    {
        public int Id { get; set; }
        public int ScrapeId { get; set; }
        public int SymbolId { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? Change { get; set; }
        public decimal? PercentChange { get; set; }
        public decimal? PrevClose { get; set; }
        public decimal? OpenPrice { get; set; }
        public int? Shares { get; set; }
        public decimal? CostBasics { get; set; }
        public DateTime? TradeDate { get; set; }
        public decimal? PercentAnnualGain { get; set; }
        public decimal? FiftyTwoWeekHigh { get; set; }
        public decimal? FiftyTwoWeekLow { get; set; }
        public decimal? Bid { get; set; }
        public int? BidSize { get; set; }
        public decimal? Ask { get; set; }

        public int? AskSize { get; set; }
        public decimal? MarketCap { get; set; }

        //public void Display()
        //{
        //    Console.WriteLine(LastPrice + " ## " + Change + " ## " + PercentChange + " ## " + PercentAnnualGain + " " + TradeDate + " " + BidSize);
        //}

    }

    
}
