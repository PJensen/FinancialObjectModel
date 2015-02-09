using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System;

namespace FinancialObjectModel
{

	/// <summary>
	///   EquityOption
	/// </summary>
	public class EquityOption : Option<Equity>
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="Option{T}" /> class.
		/// </summary>
		/// <param name="type"> the type of option </param>
		/// <param name="rights"> The rights. </param>
		/// <param name="underlying"> The underlying. </param>
		/// <param name="strikePrice"> The strike price. </param>
		/// <param name="expiration"> The expiration. </param>
		public EquityOption (OptionType type, OptionRights rights, Underlying<Equity> underlying, decimal strikePrice, DateTime expiration)
			: base (type, rights, underlying, strikePrice, expiration)
		{
		}

		/// <summary>
		///   CLX14 is a Crude Oil (CL), November (X) 2014 (14) contract Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns> A <see cref="System.String" /> that represents this instance. </returns>
		public override string ToString ()
		{
			const string space = " ";

			return Rights + space + Underlying.Security.Ticker + space + Expiration.ToDateCode () + space + Expiration.Year.SafeSubstring (2, 2);
		}
	}
}