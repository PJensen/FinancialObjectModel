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
            Console.WriteLine("FinancialObjectModel :: Demo");
            Console.WriteLine();

            if (!FinancialObjectModel.Initialize())
            {
                var tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to initialize!");
                Console.ForegroundColor = tmp;

                return;
            }

            var aapl = FinancialObjectModel.GetSecurity(s => s.Ticker == "AAPL");

            var applOption = new EquityOption(OptionType.American, OptionRights.Call, new Underlying<Equity>(aapl), 100M,
                                              DateTime.Today);

            Console.WriteLine(applOption);

            for (var i = 0; i < 10; i++)
            {
                var securityPrice = FinancialObjectModel.GetSecurityPrice(DateTime.Today, "NYSE", "AAPL");

                Console.WriteLine(securityPrice.Security.Ticker + ", " + securityPrice.Price);    
            }

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
