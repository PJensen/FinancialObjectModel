using System.Collections.Generic;
using FinancialObjectModel.Interfaces;

namespace FinancialObjectModel
{
    /// <summary>
    /// Security master.
    /// </summary>
    public class SecurityMaster
    {
        /// <summary>
        /// The reference data service.
        /// </summary>
        readonly IReferenceDataService _referenceDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialObjectModel.SecurityMaster"/> class.
        /// </summary>
        /// <param name="referenceDataService">Reference data service.</param>
        public SecurityMaster(IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get { return _cachePool.Count; } }


        /// <summary>
        /// The cache pool.
        /// </summary>
        readonly Dictionary<string, Security> _cachePool = new Dictionary<string, Security>();

        /// <summary>
        /// Gets or sets the <see cref="FinancialObjectModel.SecurityMaster"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public Security this[string index]
        {
            get {
                return _cachePool.ContainsKey(index)
                           ? _cachePool[index]
                           : (this[index] = _referenceDataService.GetSecurity(index));
            }
            set
            {
                if (_cachePool.ContainsKey(index))
                {
                    _cachePool[index] = value;
                }
                else
                {
                    _cachePool.Add(index, _referenceDataService.GetSecurity(index));
                }
            }
        }
    }
}