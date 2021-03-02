using System;
using System.Collections.Generic;
using ServiceStack;

namespace PortfolioSimulator
{
    public class DataCenter
    {

        private readonly string _apiKey;

        /// <summary>
        /// This class is responsible for collecting the data and put them in the adequate shape for the other classes.
        /// Data provider = AlphaVantage.
        /// </summary>
        public DataCenter(string apiKey)
        {
            _apiKey = apiKey;
        }

        public List<SecurityData> GetDailyPrices(string symbol)
        {
            const string FUNCTION = "TIME_SERIES_DAILY_ADJUSTED";
            string connectionString = "https://" + $@"www.alphavantage.co/query?function={FUNCTION}&symbol={symbol}&apikey={this._apiKey}&datatype=csv";
            List<SecurityData> prices = connectionString.GetStringFromUrl().FromCsv<List<SecurityData>>();
            prices.Reverse();
            return prices;
        }

        public Quote GetQuotes(string symbol)
        {
            const string FUNCTION = "GLOBAL_QUOTE";
            string connectionString = "https://" + $@"www.alphavantage.co/query?function={FUNCTION}&symbol={symbol}&apikey={this._apiKey}&datatype=csv";
            Quote quote = connectionString.GetStringFromUrl().FromCsv<Quote>();
            return quote;
        }



    }

    /// <summary>
    /// Quote intformation  on the current day.
    /// </summary>
    public class Quote
    {

        public string symbol { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal price { get; set; }
        public decimal volume { get; set; }
        public DateTime day { get; set; }
        public decimal prevClose { get; set; }
        public decimal change { get; set; }

    }

    /// <summary>
    /// Class containing the based information of a security.
    /// </summary>
    public class SecurityData
    {
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }

        public decimal High { get; set; }
        public decimal Low { get; set; }

        public decimal Close { get; set; }
        public decimal Volume { get; set; }

    }


}
