using System;

namespace FinancialObjectModel
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
		Security.SecurityPrice GetSecurityPrice(DateTime asOfDate, Security.SecurityExchange security);
	}
}

