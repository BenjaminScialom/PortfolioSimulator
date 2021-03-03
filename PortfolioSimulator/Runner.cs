using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PortfolioSimulator
{
    class Runner
    {
        static void Main(string[] args)
        {

            string apiKey = getApiKey();
            DataCenter conn = new DataCenter(apiKey);
            List<SecurityData> daily_data = conn.GetDailyInfo("AAPL");
            Quote quote_data = conn.GetQuotes("AAPL");
            Dictionary<DateTime, decimal> daily_close = conn.GetDailyClose("AAPL");
            decimal today_price = daily_data.LastOrDefault().Close;
            decimal today_quote = quote_data.price;
            Console.WriteLine(today_price);
            Console.WriteLine(today_quote);
            Console.WriteLine(daily_close.LastOrDefault());
        }

        public static string getApiKey()
        {
            //Create an object of FileInfo for specified path            
            FileInfo fi = new FileInfo(@"/Users/benjamin.s/Desktop/Simulator/PortfolioSimulator/Protected.txt");
            //Open a file for Read\Write
            FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            //Create an object of StreamReader by passing FileStream object on which it needs to operates on
            StreamReader sr = new StreamReader(fs);

            //Use the ReadToEnd method to read all the content from file
            string key = sr.ReadToEnd();
            return key;
        }

    }
}
