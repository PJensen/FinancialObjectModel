using System;

namespace FinancialObjectModel
{
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

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("AsOfDate: {0}, Security: {1}, Price: {2}, Exchange: {3}", _asOfDate, _security, _price, _exchange);
        }
    }
}