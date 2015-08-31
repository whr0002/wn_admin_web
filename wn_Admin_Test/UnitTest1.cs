using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wn_Admin;
using wn_Admin.Controllers.CompanyControllers;
using System.Web.Mvc;
using wn_Admin.Models;

namespace wn_Admin_Test
{
    [TestClass]
    public class EmployeesControllerTest
    {
        private wn_admin_db db = new wn_admin_db();

        [TestMethod]
        public void Index()
        {
            EmployeesController ec = new EmployeesController();

            ViewResult vr = ec.Index() as ViewResult;

            Assert.AreEqual("", vr.ViewName);
        }
    }
}
