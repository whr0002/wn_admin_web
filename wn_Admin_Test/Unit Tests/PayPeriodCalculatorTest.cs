using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wn_Admin.Models;
using wn_Admin.Models.CModels;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin_Test.Unit_Tests
{
    [TestClass]
    public class PayPeriodCalculatorTest
    {

        [TestMethod]
        public void getPayPeriodTest()
        {
            PayPeriodCalculator ppc = new PayPeriodCalculator();
            DateTime date = new DateTime(2020, 9, 1);

            var result = ppc.getPayPeriod(date);

            Assert.AreEqual(0, result.PPYear);
            Assert.AreEqual(0, result.PPNumber);
        }

    }
}
