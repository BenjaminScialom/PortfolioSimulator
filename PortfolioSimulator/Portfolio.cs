using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioSimulator
{
    /// <summary>
    /// This class holds a list of Positions objects, record historical balances of cash
    /// and report on performance and behavior of the portfolio.      
    /// </summary>
    public class Portfolio
    {
        private List<Position> positions;
        private Dictionary<DateTime, decimal> cashByDate = new Dictionary<DateTime, decimal>();
        private Dictionary<DateTime, decimal> valueByDate = new Dictionary<DateTime, decimal>();
        private Dictionary<DateTime, decimal> returnsByDate = new Dictionary<DateTime, decimal>();
        private Dictionary<DateTime, decimal> equityByDate = new Dictionary<DateTime, decimal>();

        private decimal initialCash;
        private DateTime creationDate;

        public Portfolio(decimal initialCash, DateTime creationDate)
        {
            this.initialCash = initialCash;
            this.creationDate = creationDate;
            cashByDate.Add(creationDate, initialCash);

        }

        public void AddPosition(Position position)
        {
            if (!positions.Contains(position))
            {
                positions.Add(position);
            }
            else
            {
                throw new Exception("This position has already been recorded in the portfolio");
            }
        }

        public void RecordCashMovement(DateTime date, decimal cash)
        {
            if (!cashByDate.ContainsKey(date))
            {
                cashByDate.Add(date, cash);
            }
        }


        public Dictionary<DateTime, decimal> GetPortfolioValueHistory()
        {
            // Add up value of assets
            foreach (var position in positions)
            {
                foreach (var item in position.GetPositionHistoricValues())
                {
                    if (valueByDate.ContainsKey(item.Key))
                    {
                        valueByDate[item.Key] += item.Value;
                    }

                    else
                    {
                        valueByDate[item.Key] = item.Value;
                    }
                }
            }

            valueByDate = valueByDate.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            return valueByDate;
        }


        public Dictionary<DateTime, decimal> GetReturnsHistory()
        {
            Dictionary<DateTime, Dictionary<string, decimal>> weights = GetPortfolioWeightsHistory();

            foreach (var position in positions)
            {
                foreach (var item in position.GetPositionHistoricReturns())
                {
                    decimal wght = weights[item.Key][position.Symbol];

                    if (returnsByDate.ContainsKey(item.Key))
                    {
                        returnsByDate[item.Key] += wght * item.Value;
                    }
                    else
                    {
                        returnsByDate[item.Key] = wght * item.Value;
                    }
                }
            }

            return returnsByDate;

        }

        public Dictionary<DateTime, decimal> GetEquityHistory()
        {
            foreach (var cashItem in cashByDate)
            {
                if (valueByDate.ContainsKey(cashItem.Key))
                {
                    equityByDate[cashItem.Key] = cashItem.Value + valueByDate[cashItem.Key];
                }
            }

            return equityByDate;
        }


        // get portfolio volatility
        // Get number of positions


    }
}
