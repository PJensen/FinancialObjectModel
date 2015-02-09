using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FinancialObjectModel
{
	/// <summary>
	///   IUnderlying
	/// </summary>
	public class Underlying<T> where T : Security
	{
		/// <summary>
		/// Gets or sets the security.
		/// </summary>
		/// <value>
		/// The security.
		/// </value>
		public T Security { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Security.ToString();
		}
	}

}

