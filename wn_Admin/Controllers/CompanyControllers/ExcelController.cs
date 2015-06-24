using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using wn_Admin.Models;


namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant")]
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult Index()
        {
            var products = new System.Data.DataTable("teste");
            products.Columns.Add("col1", typeof(int));
            products.Columns.Add("col2", typeof(string));

            products.Rows.Add(1, "product 1");
            products.Rows.Add(2, "product 2");
            products.Rows.Add(3, "product 3");
            products.Rows.Add(4, "product 4");
            products.Rows.Add(5, "product 5");
            products.Rows.Add(6, "product 6");
            products.Rows.Add(7, "product 7");


            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }

        public ActionResult Generate()
        {
            wn_admin_db db = new wn_admin_db();
            var timesheets = db.Workings.ToList();

            var table = new System.Data.DataTable("teste");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("PPyr", typeof(int));
            table.Columns.Add("PP", typeof(int));
            table.Columns.Add("Client", typeof(string));
            table.Columns.Add("Project", typeof(string));
            table.Columns.Add("ProjectID", typeof(string));
            table.Columns.Add("Task", typeof(string));
            table.Columns.Add("Identifier", typeof(string));
            table.Columns.Add("Veh", typeof(string));
            table.Columns.Add("Crew", typeof(string));
            table.Columns.Add("StartKm", typeof(double));
            table.Columns.Add("EndKm", typeof(double));
            table.Columns.Add("GPS", typeof(bool));
            table.Columns.Add("Field", typeof(string));
            table.Columns.Add("PD", typeof(bool));
            table.Columns.Add("JobDescription", typeof(string));
            table.Columns.Add("Off", typeof(string));
            table.Columns.Add("Hours", typeof(double));
            table.Columns.Add("Bank", typeof(int));
            table.Columns.Add("OT", typeof(int));

            if (timesheets != null)
            {
                foreach (var t in timesheets)
                {
                    table.Rows.Add
                        (
                        t.Employee.FullName,
                        t.Date,
                        t.PPYr,
                        t.PP,
                        t.Client.ClientName,
                        t.Project.ProjectName,
                        t.Project.ProjectID,
                        t.FK_Task.TaskName,
                        t.Identifier,
                        t.FK_Vehicle.VehicleName,
                        t.Crew,
                        t.StartKm,
                        t.EndKm,
                        t.GPS,
                        t.FK_FieldAccess.FieldAccessName,
                        t.PD,
                        t.JobDescription,
                        t.FK_OffReason.OffReasonName,
                        t.Hours,
                        t.Bank,
                        t.OT
                        );

                }
            }
            



            var grid = new GridView();
            grid.DataSource = table;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=timesheets.xls");
            Response.ContentType = "application/excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            return Content(sw.ToString(), "application/ms-excel");
        }
    }
}