using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Globalization;
using Dapper;

using WebScraper.DatabaseAccess.Models;

namespace WebScraper.DatabaseAccess
{
    class DatabaseWriter
    {
        private List<string> lines;

        public void GetFileData()
        {
            string fullPath = @"e:\Vrishali\stockdata.txt";
            lines = new List<string>(File.ReadAllLines(fullPath));
        }
        public void InsertCompany()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                var companies = new List<Company>();
                
                foreach (var line in lines)
                {
                    var data = line.Split('\t');
                    companies.Add(new Company { SymbolName = data[0], CompanyName = data[1] });
                }
                connection.Execute("dbo.uspCompanies_InsertCompany @SymbolName, @CompanyName", companies);
            }

        }

        public void InsertScrapeInfo()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                var scrapesInfo = new List<ScrapeInfo>();
                var today = DateTime.Today;
                var timeZone = "";
                try
                {
                    var data = lines[0].Split('\t');
                    timeZone = data[5].Split(' ')[1];
                    var timeString = data[5].Split(' ')[0];

                    TimeSpan tspan = DateTime.ParseExact(timeString, "h:mmtt", CultureInfo.InvariantCulture).TimeOfDay;
                    
                    today = today.Add(tspan);
                    Console.WriteLine(timeZone);
                    Console.WriteLine(today);
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("Invalid Time data"); 
                }

                scrapesInfo.Add(new ScrapeInfo { ScrapeDate = today, TimeZone = timeZone });

                connection.Execute("dbo.uspScrapesInfo_InsertScrapeInfo @ScrapeDate, @TimeZone", scrapesInfo);
            }
        }
    }
}

