using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialObjectModel.Test
{
    [TestClass]
    public class IndexTest
    {
        [TestMethod]
        public void IndexEqualWeightTest()
        {
            var index = new Index("S&P 500", "SPY")
			    {
			        {new Equity("Apple Computer", "AAPL"), 10},
			        {new Equity("Internation Business Machines", "IBM"), 10}
			    };

            var aaplWeight = index["AAPL"];
            var ibmWeight = index["IBM"];

            Assert.AreEqual(aaplWeight, ibmWeight);
        }

        [TestMethod]
        public void IndexSixtyFortyWeightTest()
        {
            const double epsilon = 0.0001;

            const int sharesAAPL = 60;
            const int sharesIBM = 40;

            var index = new Index("S&P 500", "SPY")
                {
                    {new Equity("Apple Computer", "AAPL"), sharesAAPL},
                    {new Equity("Internation Business Machines", "IBM"), sharesIBM}
                };

            var aaplWeight = index["AAPL"];
            var ibmWeight = index["IBM"];

            Assert.AreNotEqual(aaplWeight, ibmWeight);
            
            Assert.IsTrue(Math.Abs(sharesAAPL / 100.0d - (double) aaplWeight) < epsilon);
            Assert.IsTrue(Math.Abs(sharesIBM / 100.0d - (double) ibmWeight) < epsilon);
        }
    }
}

