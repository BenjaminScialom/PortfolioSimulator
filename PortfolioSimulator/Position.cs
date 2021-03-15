using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioSimulator
{
    /// <summary>
    /// This class holds information relevant to a single trade througout his life.
    /// It should check that the trading activity is not erronously recorded.
    /// </summary>
    public class Position
    {
        private string symbol;
        private DateTime entryDate;
        private decimal entryPrice;
        private int shares;
        private decimal latestPrice;
        private DateTime latestDate;
        private DateTime? exitDate;
        private decimal? exitPrice;
        Dictionary<DateTime, decimal> historical_close;

        public Position()
        {

        }

        public Position(DataCenter data, string symbol, DateTime entryDate, decimal entryPrice, int shares)
        {
            this.symbol = symbol;
            this.entryDate = entryDate;
            this.entryPrice = entryPrice;
            this.shares = shares;

            latestPrice = data.GetQuotes(symbol).price;
            latestDate = data.GetQuotes(symbol).day;
            historical_close = data.GetDailyClose(symbol);

        }

        public void ExitPosition(DateTime exitDate, decimal exitPrice)
        {
            try
            {
                if (this.entryDate == exitDate)
                    throw new Exception("Cannot enter and exit a position the same day.");
                else
                {
                    this.exitDate = exitDate;
                    this.exitPrice = exitPrice;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}. The position was entered: {this.entryDate}");
            }
        }

        public bool IsActive()
        {
            return exitDate == null;
        }

        public bool IsClosed()
        {
            return !IsActive();
        }

        public decimal GetPositionEntryValue()
        {
            return entryPrice * shares;
        }

        public decimal GetPositionCurrentValue()
        {
            return latestPrice * shares;
        }

        public decimal? GetPositionExitValue()
        {
            if (IsClosed())
                return exitPrice * shares;

            else
                return null;
        }

        public Dictionary<DateTime, decimal> GetPositionHistoricPrices()
        {
            var subset = historical_close.Where(close => close.Key > entryDate);
            return (Dictionary<DateTime, decimal>)subset;
        }

        public Dictionary<DateTime, decimal> GetPositionHistoricValues()
        {
            var subset = historical_close.Where(close => close.Key > entryDate)
                                    .Select(close => new KeyValuePair<DateTime, decimal?>(close.Key, close.Value * shares));
            return (Dictionary<DateTime, decimal>)subset;
        }

        public Dictionary<DateTime, decimal> GetPositionHistoricReturns()
        {
            decimal previousPrice = 0;
            decimal currentPrice = 0;
            decimal change = 0;

            Dictionary<DateTime, decimal> returns = new Dictionary<DateTime, decimal>();

            for (int i = 1; i < historical_close.Values.Count - 1; i++)
            {
                previousPrice = historical_close.Values.ElementAt(i - 1);
                currentPrice = historical_close.Values.ElementAt(i);
                change = (currentPrice - previousPrice) / previousPrice;

                returns.Add(historical_close.Keys.ElementAt(i), Math.Round(change, 4));

            }

            return returns;
        }

        public string Symbol => symbol;
        public DateTime EntryDate => entryDate;
        public decimal EntryPrice => entryPrice;
        public int Shares => shares;
        public DateTime? ExitDate => exitDate;
        public decimal? ExitPrice => exitPrice;
        public decimal LatestValue => this.GetPositionHistoricValues().Values.Last();

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   symbol == position.symbol &&
                   entryDate == position.entryDate &&
                   entryPrice == position.entryPrice &&
                   shares == position.shares;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(symbol, entryDate, entryPrice, shares);
        }
    }
}




