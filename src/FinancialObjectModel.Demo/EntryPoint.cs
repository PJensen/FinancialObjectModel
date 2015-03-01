using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialObjectModel.Demo
{
    /// <summary>
    /// FinancialObjectModel Entry Point
    /// </summary>
    class EntryPoint
    {
        static readonly MarketDataService MarketDataService = new MarketDataService();
        static readonly ReferenceDataService ReferenceDataService = new ReferenceDataService();
        static readonly FOM FinancialObjectModel = new FOM(MarketDataService, ReferenceDataService);

        /// <summary>
        /// Runs the FinancialObjectModel demo.
        /// </summary>
        /// <param name="args">none required</param>
        static void Main(string[] args)
        {
            var securityPrice = FinancialObjectModel.GetSecurityPrice(DateTime.Today, "NYSE", "AAPL");
        }
    }
}
