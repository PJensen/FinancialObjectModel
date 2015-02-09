using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FinancialObjectModel
{
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

		/// <summary>
		/// Gets the weight.
		/// </summary>
		/// <param name="security">The security.</param>
		/// <returns></returns>
		public SecurityWeight GetWeight(Security security)
		{
			return new SecurityWeight(security, this[security]);
		}

		/// <summary>
		/// Gets the weights.
		/// </summary>
		/// <value>
		/// The weights.
		/// </value>
		private Dictionary<Security, decimal> Weights { get { return Securities.ToDictionary(security => security, security => this[security]); } }

		/// <summary>
		/// Adds the specified PCT weight.
		/// </summary>
		/// <param name="weight">The PCT weight.</param>
		/// <param name="security">The security.</param>
		public void Add(Security security, decimal weight)
		{
			if (weight <= 0)
				throw new InvalidOperationException("invalid security weight");

			if (weightsMap.ContainsKey(security))
			{
				weightsMap[security] = weight;
			}
			else
			{
				weightsMap.Add(security, weight);
			}
		}

		/// <summary>
		/// Adds the specified underlying.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="underlying">The underlying.</param>
		/// <param name="weight">The weight.</param>
		public void Add<T>(Underlying<T> underlying, decimal weight) where T : Security
		{
			Add(underlying.Security, weight);
		}

		//		/// <summary>
		//		/// Adds the specified security weight.
		//		/// </summary>
		//		/// <param name="securityWeight">The security weight.</param>
		//		/// <param name="weight">The weight.</param>
		//		private void Add (Security securityWeight, decimal weight)
		//		{
		//		}

		/// <summary>
		/// Adds the specified security weight.
		/// </summary>
		/// <param name="securityWeight">The security weight.</param>
		public void Add(SecurityWeight securityWeight)
		{
			Add(securityWeight.Security, securityWeight.Weight);
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
				if (!weightsMap.ContainsKey(security))
				{
					throw new IndexOutOfRangeException("security is not part of index");
				}

				var securityWeight = weightsMap[security];

				var weightedSecurityWeight = securityWeight / TotalWeight;

				return weightedSecurityWeight;
			}
		}

		/// <summary>
		/// Gets the total of raw weights in the index.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		private decimal TotalWeight { get { return weightsMap.Values.Sum(); } }

		/// <summary>
		/// Removes the specified security.
		/// </summary>
		/// <param name="security">The security.</param>
		public void Remove(Security security)
		{
			if (weightsMap.ContainsKey(security))
			{
				weightsMap.Remove(security);
			}
		}

		/// <summary>
		/// The weights map
		/// </summary>
		private readonly Dictionary<Security, decimal> weightsMap =
			new Dictionary<Security, decimal>();

		/// <summary>
		/// Gets the securities.
		/// </summary>
		/// <value>
		/// The securities.
		/// </value>
		public IEnumerable<Security> Securities { get { return weightsMap.Keys; } }

		/// <summary>
		/// SecurityWeight
		/// </summary>
		public struct SecurityWeight
		{
			private readonly Security _security;
			private readonly decimal _weight;

			public Security Security { get { return _security; } }

			public decimal Weight { get { return _weight; } }

			public SecurityWeight(Security security, decimal weight)
			{
				_security = security;
				_weight = weight;
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

			foreach (var weight in weightsMap)
			{
				sb.AppendFormat("{0}\t{1}" + Environment.NewLine,
					weight.Key, this[weight.Key]);
			}

			return sb.ToString();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

