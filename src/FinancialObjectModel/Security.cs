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

        /// <summary>
        ///   Gets or sets the ticker.
        /// </summary>
        /// <value> The ticker. </value>
        [XmlAttribute]
        public string Ticker { get; protected set; }

        #region ISecurity Members


        /// <summary>
        ///   Gets or sets the name of the security.
        /// </summary>
        /// <value> The name. </value>
        [XmlAttribute]
        public string Name { get; protected set; }


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
        public interface IHasValue<in T>
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
            readonly string _exchange;
            readonly Security _security;

            /// <summary>
            /// Initializes a new instance of the <see cref="SecurityExchange"/> struct.
            /// </summary>
            /// <param name="security">Security.</param>
            /// <param name="exchange">Exchange.</param>
            public SecurityExchange(Security security, string exchange)
            {
                _exchange = exchange;
                _security = security;
            }

            /// <summary>
            /// Gets the exchange.
            /// </summary>
            /// <value>The exchange.</value>
            public string Exchange { get { return _exchange; } }

            /// <summary>
            /// Gets the security.
            /// </summary>
            /// <value>The security.</value>
            public Security Security { get { return _security; } }

            /// <summary>
            /// Equals
            /// </summary>
            /// <param name="other">the other</param>
            /// <returns>true if equals</returns>
            public bool Equals(SecurityExchange other)
            {
                return string.Equals(_exchange, other._exchange) && Equals(_security, other._security);
            }

            /// <summary>
            /// Equals
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;

                return obj is SecurityExchange && Equals((SecurityExchange)obj);
            }

            /// <summary>
            /// GetHashCode
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    return ((_exchange != null ? _exchange.GetHashCode() : 0) * 397) ^ (_security != null ? _security.GetHashCode() : 0);
                }
            }
        }

        /// <summary>
        /// Security price.
        /// </summary>
        public struct SecurityPrice
        {
            #region backing store
            private readonly DateTime _asOfDate;
            private readonly Security _security;
            private readonly decimal _price;
            private readonly string _exchange;
            #endregion

            /// <summary>
            /// Initializes a new instance of the <see cref="SecurityPrice"/> struct.
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
            /// Initializes a new instance of the <see cref="SecurityPrice"/> struct.
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
            public Security Security
            {
                get { return _security; }
            }

            /// <summary>
            /// Gets the price.
            /// </summary>
            /// <value>The price.</value>
            public decimal Price
            {
                get { return _price; }
            }

            /// <summary>
            /// Gets as of date.
            /// </summary>
            /// <value>As of date.</value>
            public DateTime AsOfDate
            {
                get { return _asOfDate; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(SecurityPrice other)
            {
                return _asOfDate.Equals(other._asOfDate) && Equals(_security, other._security) && _price == other._price &&
                       string.Equals(_exchange, other._exchange);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is SecurityPrice && Equals((SecurityPrice)obj);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = _asOfDate.GetHashCode();
                    hashCode = (hashCode * 397) ^ (_security != null ? _security.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ _price.GetHashCode();
                    hashCode = (hashCode * 397) ^ (_exchange != null ? _exchange.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }
    }
}

