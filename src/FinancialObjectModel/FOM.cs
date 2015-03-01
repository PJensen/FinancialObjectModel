using System;
using FinancialObjectModel.Interfaces;

namespace FinancialObjectModel
{
    /// <summary>
    /// Financial Object Model
    /// </summary>
    public class FOM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialObjectModel.FOM"/> class.
        /// </summary>
        /// <param name="marketDataService">Market data service.</param>
        /// <param name="referenceDataService">Reference data service.</param>
        public FOM(IMarketDataService marketDataService, IReferenceDataService referenceDataService)
        {
            _marketDataService = marketDataService;
            _securityMaster = new SecurityMaster(referenceDataService);
        }

        /// <summary>
        /// Gets the security price.
        /// </summary>
        /// <returns>The security price.</returns>
        public SecurityPrice GetSecurityPrice(DateTime asOfDate, string exchange, string ticker)
        {
            // get the security from the security master
            var security = _securityMaster[ticker];

            // get the security exchange price
            return _marketDataService.GetSecurityPrice(asOfDate, new SecurityExchange(security, exchange));
        }

        /// <summary>
        /// The market data service.
        /// </summary>
        private readonly IMarketDataService _marketDataService;

        /// <summary>
        /// the security master
        /// </summary>
        private readonly SecurityMaster _securityMaster;

        /// <summary>
        /// SecurityMaster
        /// </summary>
        public SecurityMaster SecurityMaster { get { return _securityMaster; } }
    }
}