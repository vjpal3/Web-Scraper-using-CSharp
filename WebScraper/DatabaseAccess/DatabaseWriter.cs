using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    }
}

