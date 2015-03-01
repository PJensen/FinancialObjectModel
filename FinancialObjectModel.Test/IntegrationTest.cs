using System;
using System.Collections.Generic;
using FinancialObjectModel.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialObjectModel.Test
{
    [TestClass]
    public class IntegrationTest
    {
        /// <summary>
        /// The object under test.
        /// </summary>
        private FOM objectUnderTest = null;

        [TestInitialize]
        public void TestInitialize()
        {
            objectUnderTest = new FOM(new MarketDataMock(), new RefDataMock());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            objectUnderTest = null;
        }

        [TestMethod]
        public void FOMIntegrationTest()
        {
            Assert.IsNotNull(objectUnderTest.SecurityMaster);

            for (var i = 0; i < 10; i++)
            {
                var securityPrice = objectUnderTest.GetSecurityPrice(DateTime.Today, "NYSE", "AAPL");

                Assert.AreEqual(1, objectUnderTest.SecurityMaster.Count);

                Assert.IsTrue(securityPrice.Price > 0);

                Console.WriteLine(securityPrice);
            }
        }

        /// <summary>
        /// RefDataMock
        /// </summary>
        public class RefDataMock : IReferenceDataService
        {
            public Security GetSecurity(string ticker)
            {
                var tmp = new Equity(ticker, ticker);

                return tmp;
            }
        }

        /// <summary>
        /// MarketDataMock
        /// </summary>
        public class MarketDataMock : IMarketDataService
        {

            readonly Random _random = new Random();
            readonly Dictionary<string, double> _priceMap = new Dictionary<string, double>();

            /// <summary>
            /// Gets the security price.
            /// </summary>
            /// <returns>The security price.</returns>
            /// <param name = "asOfDate">the as of date</param>
            /// <param name="securityExchange">security exchange</param>
            public Security.SecurityPrice GetSecurityPrice(DateTime asOfDate, Security.SecurityExchange securityExchange)
            {
                const int maxPrice = 780;

                var security = securityExchange.Security;
                var exchange = securityExchange.Exchange;
                var ticker = security.Ticker;

                if (!_priceMap.ContainsKey(ticker))
                {
                    _priceMap.Add(ticker, _random.Next(1, maxValue: maxPrice));
                }
                else
                {
                    // up a random amount; down a random amount.
                    _priceMap[ticker] += _random.NextDouble();
                    _priceMap[ticker] -= _random.NextDouble();
                }

                return new Security.SecurityPrice(securityExchange.Security, Convert.ToDecimal(_priceMap[ticker]), exchange);
            }
        }
    }
}

