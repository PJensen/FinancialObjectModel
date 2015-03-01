using System.Collections.Generic;

namespace FinancialObjectModel.Interfaces
{
	/// <summary>
	/// security master interface
	/// </summary>
	public interface IReferenceDataService
	{
        List<Security> Securities { get;}

	    /// <summary>
		/// Gets the security.
		/// </summary>
		/// <returns>The security.</returns>
		/// <param name="ticker">Ticker.</param>
		Security GetSecurity(string ticker);

	    /// <summary>
        /// Initialized
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Initialize
        /// </summary>
	    bool Initialize();
	}
}