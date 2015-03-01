using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FinancialObjectModel
{
	/// <summary>
	///   Naming conventions are used to help identify properties common to many different types of options.
	/// </summary>
	public enum OptionType
	{
		/// <summary>
		///   an option that may only be exercised on expiration.
		/// </summary>
		European,

		/// <summary>
		///   an option that may be exercised on any trading day on or before expiry.
		/// </summary>
		American,

		/// <summary>
		///   an option that may be exercised only on specified dates on or before expiration.
		/// </summary>
		Bermudan,

		/// <summary>
		///   option whose payoff is determined by the average underlying price over some preset time period.
		/// </summary>
		Asian,

		/// <summary>
		///   any option with the general characteristic that the underlying security's price must pass a certain level or "barrier" before it can be exercised.
		/// </summary>
		Barrier,

		/// <summary>
		///   An all-or-nothing option that pays the full amount if the underlying security meets the defined condition on expiration otherwise it expires worthless.
		/// </summary>
		Binary
	}
}
