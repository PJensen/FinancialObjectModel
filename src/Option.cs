using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System;

namespace FinancialObjectModel
{
	/// <summary>
	///   An option
	/// </summary>
	/// <typeparam name="T"> the type of the underlying </typeparam>
	public abstract class Option<T> : Security where T : Security
	{
		private readonly DateTime _expiration;
		private readonly OptionRights _rights;
		private readonly decimal _strikePrice;
		private readonly OptionType _type;
		private readonly Underlying<T> _underlying;

		/// <summary>
		///   Initializes a new instance of the <see cref="Option{T}" /> class.
		/// </summary>
		/// <param name="type"> the type of option </param>
		/// <param name="rights"> The rights. </param>
		/// <param name="underlying"> The underlying. </param>
		/// <param name="strikePrice"> The strike price. </param>
		/// <param name="expiration"> The expiration. </param>
		protected Option(OptionType type, OptionRights rights, Underlying<T> underlying, decimal strikePrice, DateTime expiration)
			: base(OptionName(rights, underlying, strikePrice), OptionTicker(underlying, expiration))
		{
			_underlying = underlying;
			_strikePrice = strikePrice;
			_expiration = expiration;
			_type = type;
			_rights = rights;
		}

		/// <summary>
		///   Gets the underlying.
		/// </summary>
		/// <value> The underlying. </value>
		public Underlying<T> Underlying { get { return _underlying; } }

		/// <summary>
		///   Gets or sets the strike price.
		/// </summary>
		/// <value> The strike price. </value>
		public decimal StrikePrice { get { return _strikePrice; } }

		/// <summary>
		///   Gets the expiration.
		/// </summary>
		/// <value> The expiration. </value>
		public DateTime Expiration { get { return _expiration; } }

		/// <summary>
		///   Gets or sets the type.
		/// </summary>
		/// <value> The type. </value>
		public OptionType Type { get { return _type; } }

		/// <summary>
		///   Gets or sets the rights.
		/// </summary>
		/// <value> The rights. </value>
		public OptionRights Rights { get { return _rights; } }

		/// <summary>
		/// Options the name.
		/// </summary>
		/// <param name="rights">The rights.</param>
		/// <param name="underlying">The underlying.</param>
		/// <param name="strike"> </param>
		/// <returns></returns>
		private static string OptionName(OptionRights rights, Underlying<T> underlying, decimal strike)
		{
			return rights + " " + underlying.Security.Name + " @ " + strike;
		}

		/// <summary>
		/// Options the ticker.
		/// </summary>
		/// <param name="underlying">The underlying.</param>
		/// <param name="expiration">The expiration.</param>
		/// <returns></returns>
		private static string OptionTicker(Underlying<T> underlying, DateTime expiration)
		{
			return underlying.ToString() +
			expiration.ToDateCode() +
			expiration.SafeSubstring(3, 2);
		}

#region Nested type: OptionValue

		/// <summary>
		///   OptionValue
		/// </summary>
		public struct OptionValue
		{
			/// <summary>
			///   The intrinsic value
			/// </summary>
			public decimal IntrinsicValue;

			/// <summary>
			///   The time value
			/// </summary>
			public decimal TimeValue;

			/// <summary>
			///   The underlying price
			/// </summary>
			public decimal UnderlyingPrice;
		}

#endregion
	}
}

