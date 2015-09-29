using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize()]
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index(string fileName)
        {
            if (fileName == null || fileName.Equals("")) return View();

            var fullName = fileName + ".pdf";
            var path = Server.MapPath("~/App_Data/" + fullName);

            var existed = System.IO.File.Exists(path);

            if (existed) return File(path, "application/pdf", fullName);
            else return View();
        }
    }
}