using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System;
using System.Xml.Serialization;

namespace FinancialObjectModel
{
	/// <summary>
	///   Security
	/// </summary>
	[Serializable]
	public abstract class Security
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Security"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="ticker">The ticker.</param>
		protected Security(string name, string ticker)
		{
			Name = name;
			Ticker = ticker;
		}

		const string fieldName = "name";
		const string fieldTicker = "ticker";

		/// <summary>
		///   Gets or sets the ticker.
		/// </summary>
		/// <value> The ticker. </value>
		[XmlAttribute]
		public string Ticker { get { return this[fieldTicker] as string; } set { this[fieldTicker] = value; } }

		/// <summary>
		/// The security fields.
		/// </summary>
		readonly Dictionary<string, object> _securityFields = new Dictionary<string, object>();

		/// <summary>
		/// Gets or sets the <see cref="FinancialObjectModel.Security"/> with the specified fieldName.
		/// </summary>
		/// <param name="fieldName">Field name.</param>
		public object this[string fieldName]
		{
			get
			{
				return _securityFields[fieldName];
			}
			set
			{
				if (_securityFields.ContainsKey(fieldName))
				{
					_securityFields[fieldName] = value;
				}
				else
				{
					_securityFields.Add(fieldName, value);
				}
			}
		}


#region ISecurity Members


		/// <summary>
		///   Gets or sets the name of the security.
		/// </summary>
		/// <value> The name. </value>
		[XmlAttribute]
		public string Name { get { return this[fieldName] as string; } set { this[fieldName] = value; } }


#endregion

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("{0}", Name);
		}

		/// <summary>
		/// Valuation interface.
		/// </summary>
		public interface IHasValue<T>
		{
			/// <summary>
			/// Gets the value of the security.
			/// </summary>
			/// <returns>The value.</returns>
			/// <param name="parameters">Parameters.</param>
			SecurityPrice GetValue(T parameters);
		}

		/// <summary>
		/// Security exchange.
		/// </summary>
		public struct SecurityExchange
		{
			readonly string exchange;
			readonly Security security;

			/// <summary>
			/// Initializes a new instance of the <see cref="FinancialObjectModel.Security+SecurityExchange"/> struct.
			/// </summary>
			/// <param name="security">Security.</param>
			/// <param name="exchange">Exchange.</param>
			public SecurityExchange(Security security, string exchange)
			{
				this.exchange = exchange;
				this.security = security;
			}

			/// <summary>
			/// Gets the exchange.
			/// </summary>
			/// <value>The exchange.</value>
			public string Exchange { get { return exchange; } }

			/// <summary>
			/// Gets the security.
			/// </summary>
			/// <value>The security.</value>
			public Security Security { get { return security; } }

			/// <summary>
			/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="FinancialObjectModel.Security+SecurityExchange"/>.
			/// </summary>
			/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="FinancialObjectModel.Security+SecurityExchange"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
			/// <see cref="FinancialObjectModel.Security+SecurityExchange"/>; otherwise, <c>false</c>.</returns>
			public override bool Equals(object obj)
			{
				if (obj == null)
					return false;
				if (obj.GetType() != typeof(SecurityExchange))
					return false;
				SecurityExchange other = (SecurityExchange)obj;
				return exchange == other.exchange && security == other.security;
			}


			/// <summary>
			/// Serves as a hash function for a <see cref="FinancialObjectModel.Security+SecurityExchange"/> object.
			/// </summary>
			/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
			public override int GetHashCode()
			{
				unchecked
				{
					return (exchange != null ? exchange.GetHashCode() : 0) ^ (security != null ? security.GetHashCode() : 0);
				}
			}
			
		}

		/// <summary>
		/// Security price.
		/// </summary>
		public struct SecurityPrice
		{
			readonly DateTime _asOfDate;
			readonly Security _security;
			readonly decimal _price;
			readonly string _exchange;

			/// <summary>
			/// Initializes a new instance of the <see cref="FinancialObjectModel.Security+SecurityPrice"/> struct.
			/// </summary>
			/// <param name="security">Security.</param>
			/// <param name = "asOfDate"></param>
			/// <param name="price">Price.</param>
			/// <param name = "exchange"></param>
			public SecurityPrice(Security security, DateTime asOfDate, decimal price, string exchange)
			{
				_price = price;
				_asOfDate = asOfDate;
				_security = security;
				_exchange = exchange;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="FinancialObjectModel.Security+SecurityPrice"/> struct.
			/// </summary>
			/// <param name="security">Security.</param>
			/// <param name="price">Price.</param>
			/// <param name = "exchange"></param>
			public SecurityPrice(Security security, decimal price, string exchange)
				: this(security, DateTime.Today, price, exchange)
			{
			}

			/// <summary>
			/// Gets the security.
			/// </summary>
			/// <value>The security.</value>
			public Security Security { get { return _security; } }

			/// <summary>
			/// Gets the price.
			/// </summary>
			/// <value>The price.</value>
			public decimal Price { get { return _price; } }

			/// <summary>
			/// Gets as of date.
			/// </summary>
			/// <value>As of date.</value>
			public DateTime AsOfDate { get { return _asOfDate; } }

			/// <summary>
			/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="FinancialObjectModel.Security+SecurityPrice"/>.
			/// </summary>
			/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="FinancialObjectModel.Security+SecurityPrice"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
			/// <see cref="FinancialObjectModel.Security+SecurityPrice"/>; otherwise, <c>false</c>.</returns>
			public override bool Equals(object obj)
			{
				if (obj == null)
					return false;
				if (obj.GetType() != typeof(SecurityPrice))
					return false;
				SecurityPrice other = (SecurityPrice)obj;
				return _asOfDate == other._asOfDate && _security == other._security && _price == other._price && _exchange == other._exchange;
			}


			/// <summary>
			/// Serves as a hash function for a <see cref="FinancialObjectModel.Security+SecurityPrice"/> object.
			/// </summary>
			/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
			public override int GetHashCode()
			{
				unchecked
				{
					return _asOfDate.GetHashCode() ^ (_security != null ? _security.GetHashCode() : 0) ^ _price.GetHashCode() ^ (_exchange != null ? _exchange.GetHashCode() : 0);
				}
			}

			/// <summary>
			/// Returns a <see cref="System.String"/> that represents the current <see cref="FinancialObjectModel.Security+SecurityPrice"/>.
			/// </summary>
			/// <returns>A <see cref="System.String"/> that represents the current <see cref="FinancialObjectModel.Security+SecurityPrice"/>.</returns>
			public override string ToString()
			{
				return string.Format("[SecurityPrice: _asOfDate={0}, _security={1}, _price={2}, _exchange={3}]", _asOfDate, _security, _price, _exchange);
			}
			
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="FinancialObjectModel.Security"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (fieldName != null ? fieldName.GetHashCode() : 0) ^ (fieldTicker != null ? fieldTicker.GetHashCode() : 0);
			}
		}

		
	}
}

