using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialObjectModel.Test
{
	[TestClass]
	public class PricingTest
	{
		[TestMethod]
		public void TestEquityOptionPricing()
		{
			const string expectedName = "International Business Machines";
			const string expectedTicker = "IBM";

		    var ibm = new Underlying<Equity>(new Equity(expectedName, expectedTicker));

			var expectedExpiration = new DateTime(2015, 01, 01);
			const decimal expectedStrike = 100;
			const OptionRights expectedRights = OptionRights.Call;
			const OptionType expectedType = OptionType.American;

			var ibmEquityOption = new EquityOption(expectedType, expectedRights, ibm, expectedStrike, expectedExpiration);

            Assert.IsNotNull(ibmEquityOption);
		}
	}
}

