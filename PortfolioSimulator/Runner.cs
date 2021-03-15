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
            Console.WriteLine(daily_close);

            //decimal previousPrice = 0;
            //decimal currentPrice = 0;
            //decimal change = 0;

            //Dictionary<DateTime, decimal> returns = new Dictionary<DateTime, decimal>();

            //for (int i = 1; i < daily_close.Values.Count; i++)
            //{
            //    previousPrice = daily_close.Values.ElementAt(i - 1);
            //    currentPrice = daily_close.Values.ElementAt(i);
            //    change = (currentPrice - previousPrice) / previousPrice;

            //    returns.Add(daily_close.Keys.ElementAt(i), Math.Round(change, 4));

            //}

            //Console.WriteLine(returns.Last());
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
