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
            _referenceDataService = referenceDataService;
            _securityMaster = new SecurityMaster(referenceDataService);
        }

        /// <summary>
        /// initialized
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <returns>true if initialization was okay</returns>
        public bool Initialize()
        {
            return !Initialized && (Initialized = _referenceDataService.Initialize());
        }

        /// <summary>
        /// Gets the security price.
        /// </summary>
        /// <returns>The security price.</returns>
        public SecurityPrice GetSecurityPrice(DateTime asOfDate, string exchange, string ticker)
        {
            CheckInitState();

            // get the security from the security master
            var security = _securityMaster.GetSecurity(s => s.Ticker == ticker);

            // get the security exchange price
            return _marketDataService.GetSecurityPrice(asOfDate, new SecurityExchange(security, exchange));
        }

        /// <summary>
        /// GetSecurity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Security GetSecurity(Func<Security, bool> predicate)
        {
            return _securityMaster.GetSecurity(predicate);
        }

        /// <summary>
        /// Checks the initialization state. If not initialized; does the init and sets the state.
        /// <remarks>
        /// They should expect that initialize will be called on all registered services</remarks>
        /// </summary>
        private void CheckInitState()
        {
            if (!Initialized)
            {
                Initialize();
            }
        }

        /// <summary>
        /// The market data service.
        /// </summary>
        private readonly IMarketDataService _marketDataService;

        /// <summary>
        /// _referenceDataService
        /// </summary>
        private readonly IReferenceDataService _referenceDataService;

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