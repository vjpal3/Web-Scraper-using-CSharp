using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WebScraper.DatabaseAccess
{
    class DatabaseWriter
    {
        public void TestDataBaseConection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionStrVal("ScraperData")))
            {
                
            }

        }
    }
}
