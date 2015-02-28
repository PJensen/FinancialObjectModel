using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace FinancialObjectModel.Test
{
	[TestFixture]
	public class IntegrationTest
	{
		/// <summary>
		/// The object under test.
		/// </summary>
		private FOM objectUnderTest = null;

		[SetUp]
		public void SetUp()
		{
			objectUnderTest = new FOM(new MarketDataMock(), new RefDataMock());
		}

		[TearDown]
		public void TearDown()
		{
			objectUnderTest = null;
		}

		[Test]
		public void FOMIntegrationTest()
		{
			Assert.IsNotNull(objectUnderTest.SecurityMaster);

			for (int i = 0; i < 10; i++)
			{
				var securityPrice = objectUnderTest.GetSecurityPrice(DateTime.Today, "NYSE", "AAPL");

				Assert.AreEqual(1, objectUnderTest.SecurityMaster.Count);

				Assert.IsTrue(securityPrice.Price > 0);

				Console.WriteLine(securityPrice);
			}
		}

		public class RefDataMock : IReferenceDataService
		{
			public Security GetSecurity(string ticker)
			{
				var tmp = new Equity(ticker, ticker);

				return tmp;
			}
		}


		public class MarketDataMock : IMarketDataService
		{

			readonly Random random = new Random();

			readonly Dictionary<string, double> priceMap = new Dictionary<string, double>();

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

				if (!priceMap.ContainsKey(ticker))
				{
					priceMap.Add(ticker, random.Next(1, maxValue: maxPrice));
				}
				else
				{
					// up a random amount; down a random amount.
					priceMap[ticker] += random.NextDouble();
					priceMap[ticker] -= random.NextDouble();
				}

				return new Security.SecurityPrice(securityExchange.Security, Convert.ToDecimal(priceMap[ticker]), exchange);
			}
		}

	}
}

