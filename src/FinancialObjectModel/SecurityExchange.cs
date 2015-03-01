namespace FinancialObjectModel
{
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
}