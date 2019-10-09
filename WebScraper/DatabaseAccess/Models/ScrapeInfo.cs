using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.DatabaseAccess.Models
{
    class ScrapeInfo
    {
        public int ScrapeId { get; set; }
        public DateTime ScrapeDate { get; set; }
        public string TimeZone { get; set; }
    }
}
