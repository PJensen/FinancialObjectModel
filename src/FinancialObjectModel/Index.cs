using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialObjectModel
{
    /// <summary>
    /// Index.
    /// </summary>
    [Serializable]
    public class Index : Security, IEnumerable<Index.SecurityWeight>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Security"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ticker">The ticker.</param>
        public Index(string name, string ticker)
            : base(name, ticker)
        {
        }

        //		/// <summary>
        //		/// Gets the shares.
        //		/// </summary>
        //		/// <param name="security">The security.</param>
        //		/// <returns></returns>
        //		public decimal GetWeight(Security security)
        //		{
        //			return this[security];
        //		}
        //
        //		/// <summary>
        //		/// Gets the shares.
        //		/// </summary>
        //		/// <returns>The shares.</returns>
        //		/// <param name="ticker">Ticker.</param>
        //		public decimal GetWeight(string ticker)
        //		{
        //			return weightsMap.SingleOrDefault(s => s.Key.Ticker == ticker).Value;
        //		}

        /// <summary>
        /// Gets the weights.
        /// </summary>
        /// <value>
        /// The weights.
        /// </value>
        private Dictionary<Security, decimal> Weights { get { return Securities.ToDictionary(security => security, security => this[security]); } }

        /// <summary>
        /// Adds the specified PCT shares.
        /// </summary>
        /// <param name="weight">The PCT shares.</param>
        /// <param name="security">The security.</param>
        public void Add(Security security, decimal weight)
        {
            if (weight <= 0)
            {
                throw new InvalidOperationException("invalid security shares");
            }

            if (_weightsMap.ContainsKey(security))
            {
                _weightsMap[security] = weight;
            }
            else
            {
                _weightsMap.Add(security, weight);
            }
        }

        /// <summary>
        /// Adds the specified underlying.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="underlying">The underlying.</param>
        /// <param name="weight">The shares.</param>
        public void Add<T>(Underlying<T> underlying, decimal weight) where T : Security
        {
            Add(underlying.Security, weight);
        }

        /// <summary>
        /// Adds the specified security shares.
        /// </summary>
        /// <param name="securityWeight">The security shares.</param>
        public void Add(SecurityWeight securityWeight)
        {
            Add(securityWeight.Security, securityWeight.Shares);
        }

        /// <summary>
        /// Gets the <see cref="System.Decimal"/> with the specified security.
        /// </summary>
        /// <value>
        /// The <see cref="System.Decimal"/>.
        /// </value>
        /// <param name="security">The security.</param>
        /// <returns></returns>
        /// <exception cref="System.IndexOutOfRangeException">security is not part of index</exception>
        public decimal this[Security security]
        {
            get
            {
                if (!_weightsMap.ContainsKey(security))
                {
                    throw new IndexOutOfRangeException("security is not part of index");
                }

                var securityWeight = _weightsMap[security];

                var weightedSecurityWeight = securityWeight / TotalWeight;

                return weightedSecurityWeight;
            }
        }

        /// <summary>
        /// Gets the <see cref="FinancialObjectModel.Index"/> with the specified ticker.
        /// </summary>
        /// <param name="ticker">Ticker.</param>
         public decimal this[string ticker] { get { return this[_weightsMap.Single(s => s.Key.Ticker == ticker).Key]; } }

        /// <summary>
        /// Gets the total of raw weights in the index.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        private decimal TotalWeight { get { return _weightsMap.Values.Sum(); } }

        /// <summary>
        /// Removes the specified security.
        /// </summary>
        /// <param name="security">The security.</param>
        public void Remove(Security security)
        {
            if (_weightsMap.ContainsKey(security))
            {
                _weightsMap.Remove(security);
            }
        }

        /// <summary>
        /// The weights map
        /// </summary>
        private readonly Dictionary<Security, decimal> _weightsMap =
            new Dictionary<Security, decimal>();

        /// <summary>
        /// Gets the securities.
        /// </summary>
        /// <value>
        /// The securities.
        /// </value>
        public IEnumerable<Security> Securities { get { return _weightsMap.Keys; } }

        /// <summary>
        /// SecurityWeight
        /// </summary>
        public struct SecurityWeight
        {
            #region backing store

            readonly Security _security;
            readonly decimal _shares;

            #endregion

            /// <summary>
            /// Gets the security.
            /// </summary>
            /// <value>The security.</value>
            public Security Security { get { return _security; } }

            /// <summary>
            /// Gets the shares.
            /// </summary>
            /// <value>The shares.</value>
            public decimal Shares { get { return _shares; } }

            /// <summary>
            /// Initializes a new instance of <see cref="SecurityWeight"/>
            /// </summary>
            /// <param name="security">the security</param>
            /// <param name="shares">the weights</param>
            public SecurityWeight(Security security, decimal shares)
            {
                _security = security;
                _shares = shares;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<SecurityWeight> GetEnumerator()
        {
            return Weights.Select(s => new SecurityWeight(s.Key, s.Value)).GetEnumerator();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder("SECURITY\tWEIGHT" + Environment.NewLine);

            foreach (var weight in _weightsMap)
            {
                sb.AppendFormat("{0}\t{1}" + Environment.NewLine,
                    weight.Key, this[weight.Key]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

