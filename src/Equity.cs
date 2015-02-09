using System;

namespace FinancialObjectModel
{
	/// <summary>
	///        Equity
	/// </summary>
	public class Equity : Security
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Security"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="ticker">The ticker.</param>
		public Equity(string name, string ticker)
			: base(name, ticker)
		{
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Ticker;
		}
	}
}

