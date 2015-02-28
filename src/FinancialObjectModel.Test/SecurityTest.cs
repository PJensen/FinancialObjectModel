using System;
using NUnit.Framework;

namespace FinancialObjectModel.Test
{
	[TestFixture]
	public class SecurityTest
	{
		[Test]
		public void EquitySecurityCtorTest()
		{
			var expectedName = Guid.NewGuid().ToString();
			var expectedTicker = Guid.NewGuid().ToString();

			var tmp = new Equity(expectedName, expectedTicker);

			Assert.AreEqual(expectedName, tmp.Name);
			Assert.AreEqual(expectedTicker, tmp.Ticker);
		}

		[Test]
		public void EquityOptionSecurityCtorTest()
		{
			const string expectedName = "International Business Machines";
			const string expectedTicker = "IBM";

			// create the underlying equity
			var ibm = new Underlying<Equity>(expectedName, expectedTicker);

			var expectedExpiration = new DateTime(2015, 01, 01);
			const decimal expectedStrike = 100;
			const OptionRights expectedRights = OptionRights.Call;
			const OptionType expectedType = OptionType.American;

			var ibmEquityOption = new EquityOption(expectedType, expectedRights, ibm, expectedStrike, expectedExpiration);

			Assert.AreEqual(expectedName, ibmEquityOption.Underlying.Security.Name);
			Assert.AreEqual(expectedTicker, ibmEquityOption.Underlying.Security.Ticker);
			Assert.AreEqual(expectedRights, ibmEquityOption.Rights);
			Assert.AreEqual(expectedStrike, ibmEquityOption.StrikePrice);
			Assert.AreEqual(expectedType, ibmEquityOption.Type);
		}
	}
}

