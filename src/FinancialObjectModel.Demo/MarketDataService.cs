using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using FinancialObjectModel.Interfaces;

namespace FinancialObjectModel.Demo
{
    class MarketDataService : IMarketDataService
    {
        readonly WebClient _webClient = new WebClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asOfDate"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public SecurityPrice GetSecurityPrice(DateTime asOfDate, SecurityExchange security)
        {
            double tmpPrice;

            if (_priceMap.ContainsKey(security.Security.Ticker))
            {
                tmpPrice = _priceMap[security.Security.Ticker];
            }
            else
            {
                tmpPrice = Random.Next(1, 2000) * Random.NextDouble();
                _priceMap.Add(security.Security.Ticker, tmpPrice);
            }

            tmpPrice -= Random.NextDouble();
            tmpPrice += Random.NextDouble();

            return new SecurityPrice(security.Security, asOfDate, Convert.ToDecimal(tmpPrice), security.Exchange);
        }

        /// <summary>
        /// price map
        /// </summary>
        private readonly Dictionary<string, double> _priceMap = new Dictionary<string, double>();

        /// <summary>
        /// prng
        /// </summary>
        static readonly Random Random = new Random();
    }
}