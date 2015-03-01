using System;
using System.Collections.Generic;

namespace FinancialObjectModel
{
	/// <summary>
	/// security master interface
	/// </summary>
	public interface IReferenceDataService
	{
		/// <summary>
		/// Gets the security.
		/// </summary>
		/// <returns>The security.</returns>
		/// <param name="ticker">Ticker.</param>
		Security GetSecurity(string ticker);
	}
	
}