using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.DatabaseAccess.Models
{
    class Company
    {
        public int Id { get; set; }
        public string SymbolName { get; set; }
        public string CompanyName { get; set; }

        public StockData stockData { get; set; }
    }
}
