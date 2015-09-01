using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin_Test.Unit_Tests
{
    [TestClass]
    public class TimesheetDateValidatorTest
    {

        [TestMethod]
        public void ValidateTimesheetDateRangeTest()
        {
            DateTime d1 = new DateTime(2015, 9, 2);
            DateTime d2 = new DateTime(2015, 9, 1);
            var result = TimesheetDateValidator.ValidateTimesheetDateRange(d1, d2);

            Assert.AreEqual("Date should not be in the future.", result);

            d1 = new DateTime(2015, 8, 20);
            d2 = new DateTime(2015, 8, 18);

            result = TimesheetDateValidator.ValidateTimesheetDateRange(d1, d2);

            Assert.AreEqual("Date must be within 10 days.", result);


            d1 = new DateTime(2015, 8, 27);
            d2 = new DateTime(2015, 8, 26);

            result = TimesheetDateValidator.ValidateTimesheetDateRange(d1, d2);

            Assert.AreEqual("End Date must be later than Start Date", result);



        }
    }
}
