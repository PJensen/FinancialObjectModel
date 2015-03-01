using NUnit.Framework;
using System;

namespace FinancialObjectModel.Test
{
	[TestFixture]
	public class IndexTest
	{
		[Test]
		public void IndexEqualWeightTest()
		{
			var index = new Index("S&P 500", "SPY");
			
			index.Add(new Equity("Apple Computer", "AAPL"), 10);
			index.Add(new Equity("Internation Business Machines", "IBM"), 10);

			var aaplWeight = index["AAPL"];
			var ibmWeight = index["IBM"];

			Assert.AreEqual(aaplWeight, ibmWeight);
		}

		[Test]
		public void IndexSixtyFortyWeightTest()
		{
			const int sharesAAPL = 60;
			const int sharesIBM = 40;

			var index = new Index("S&P 500", "SPY");
			
			index.Add(new Equity("Apple Computer", "AAPL"), sharesAAPL);
			index.Add(new Equity("Internation Business Machines", "IBM"), sharesIBM);

			var aaplWeight = index["AAPL"];
			var ibmWeight = index["IBM"];

			Assert.AreNotEqual(aaplWeight, ibmWeight);
			Assert.AreEqual(sharesAAPL / 100.0d, aaplWeight);
			Assert.AreEqual(sharesIBM / 100.0d, ibmWeight);
		}

	}
}

