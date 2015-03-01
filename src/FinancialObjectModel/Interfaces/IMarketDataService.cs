using System;

namespace FinancialObjectModel.Interfaces
{
	/// <summary>
	/// I market data source.
	/// </summary>
	public interface IMarketDataService
	{
		/// <summary>
		/// Gets the security price.
		/// </summary>
		/// <returns>The security price.</returns>
		SecurityPrice GetSecurityPrice(DateTime asOfDate, SecurityExchange security);
	}
}

