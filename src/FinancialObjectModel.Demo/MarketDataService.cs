using System;
using FinancialObjectModel.Interfaces;

namespace FinancialObjectModel.Demo
{
    class MarketDataService : IMarketDataService
    {
        public SecurityPrice GetSecurityPrice(DateTime asOfDate, SecurityExchange security)
        {
            return default(SecurityPrice);
        }
    }
}