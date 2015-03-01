using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FinancialObjectModel
{
	/// <summary>
	///   The right to buy or sell.
	/// </summary>
	public enum OptionRights
	{
		/// <summary>
		///   Put options give you the right but not the obligation, to sell something at a specific price for a specific time period.
		/// </summary>
		Put,

		/// <summary>
		///   Call options give you the right but not the obligation, to buy something at a specific price for a specific time period.
		/// </summary>
		Call,
	}

}
