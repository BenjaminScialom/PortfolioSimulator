using System;
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

        }




        public string Symbol { get => symbol; }
        public DateTime EntryDate { get => entryDate; }
        public decimal EntryPrice { get => entryPrice; }
        public int Shares { get => shares; }

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




