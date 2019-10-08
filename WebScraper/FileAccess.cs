using System;
using System.Collections.Generic;
using System.IO;

namespace WebScraper
{
    class FileAccess
    {
        public static void SaveDataToFile(string stockData)
        {
            string fullpath = @"e:\Vrishali\stockdata_test.txt";
            File.WriteAllText(fullpath, stockData);
        }
    }
}
