using System;
using System.Collections.Generic;

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
            this._marketDataService = marketDataService;
            this._securityMaster = new SecurityMaster(referenceDataService);
        }

        /// <summary>
        /// Gets the security price.
        /// </summary>
        /// <returns>The security price.</returns>
        public Security.SecurityPrice GetSecurityPrice(DateTime asOfDate, string exchange, string ticker)
        {
            // get the security from the security master
            var security = _securityMaster[ticker];

            // get the security exchange price
            return _marketDataService.GetSecurityPrice(asOfDate, new Security.SecurityExchange(security, exchange));
        }

        /// <summary>
        /// The security master.
        /// </summary>
        private readonly SecurityMaster _securityMaster;

        /// <summary>
        /// The market data service.
        /// </summary>
        private readonly IMarketDataService _marketDataService;

        /// <summary>
        /// Gets the security master.
        /// </summary>
        /// <value>The security master.</value>
        public SecurityMaster SecurityMaster { get { return _securityMaster; } }
    }

    /// <summary>
    /// Security master.
    /// </summary>
    public class SecurityMaster
    {
        /// <summary>
        /// The reference data service.
        /// </summary>
        readonly IReferenceDataService referenceDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialObjectModel.SecurityMaster"/> class.
        /// </summary>
        /// <param name="referenceDataService">Reference data service.</param>
        public SecurityMaster(IReferenceDataService referenceDataService)
        {
            this.referenceDataService = referenceDataService;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get { return cachePool.Count; } }


        /// <summary>
        /// The cache pool.
        /// </summary>
        readonly Dictionary<string, Security> cachePool = new Dictionary<string, Security>();

        /// <summary>
        /// Gets or sets the <see cref="FinancialObjectModel.SecurityMaster"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public Security this[string index]
        {
            get
            {
                if (cachePool.ContainsKey(index))
                {
                    return cachePool[index];
                }
                else
                {
                    return this[index] = referenceDataService.GetSecurity(index);
                }
            }
            set
            {
                if (cachePool.ContainsKey(index))
                {
                    cachePool[index] = value;
                }
                else
                {
                    cachePool.Add(index, referenceDataService.GetSecurity(index));
                }
            }
        }
    }
}