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
        private Dictionary<DateTime, decimal> cashHistory = new Dictionary<DateTime, decimal>();
        private Dictionary<DateTime, decimal> returns = new Dictionary<DateTime, decimal>();
        private Dictionary<DateTime, decimal> valueByDate = new Dictionary<DateTime, decimal>();
        private decimal initialCash;
        private DateTime creationDate;

        public Portfolio(decimal initialCash, DateTime creationDate)
        {
            this.initialCash = initialCash;
            this.creationDate = creationDate;

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
            if (!cashHistory.ContainsKey(date))
            {
                cashHistory.Add(date, cash);
            }
        }

        public Dictionary<DateTime, decimal> GetCashHistory()
        {
            return cashHistory;
        }

        public Dictionary<DateTime, Dictionary<string, decimal>> GetPortfolioWeightsHistory()
        {

            Dictionary<DateTime, Dictionary<string, decimal>> weightsByDate = new Dictionary<DateTime, Dictionary<string, decimal>>();
            Dictionary<string, decimal> weightsValue = new Dictionary<string, decimal>();

            decimal PortfolioValue = GetCuurentTotalValue();

            if (positions != null)
            {
                foreach (var position in positions)
                {
                    foreach (var item in position.GetPositionHistoricValues())
                    {
                        weightsValue.Add(position.Symbol, item.Value / PortfolioValue);
                        weightsByDate.Add(item.Key, weightsValue);
                    }
                }
            }

            return weightsByDate;
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

        public decimal GetCuurentTotalValue()
        {
            decimal totalValue = GetPortfolioValueHistory().Values.Last();
            return totalValue;
        }

        // Get current cash  = intitial cash +/- cash movement
        // get returns per/positions and date (like value)
        // get equity history  = cash + stock value

        // get portfolio return and volatility
        // Get number of positions


    }
}
