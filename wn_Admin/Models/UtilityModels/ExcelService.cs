using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class ExcelService
    {
        public string generateExcel(IQueryable<Working> ws)
        {
            var timesheets = ws.ToList();

            var table = new System.Data.DataTable("teste");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("End Date", typeof(DateTime));
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
            table.Columns.Add("KmDiff", typeof(double));
            table.Columns.Add("Equipments", typeof(string));
            table.Columns.Add("Field", typeof(string));
            table.Columns.Add("PD", typeof(bool));
            table.Columns.Add("JobDescription", typeof(string));
            table.Columns.Add("Off", typeof(string));
            table.Columns.Add("Hours", typeof(double));


            if (timesheets != null)
            {
                foreach (var t in timesheets)
                {
                    table.Rows.Add
                        (
                        t.Employee.FullName,
                        t.Date,
                        t.EndDate,
                        t.PPYr,
                        t.PP,
                        t.Project.FK_Client.ClientName,
                        t.Project.ProjectName,
                        t.Project.ProjectID,
                        t.Task,
                        t.Identifier,
                        t.Veh,
                        t.Crew,
                        t.StartKm,
                        t.EndKm,
                        t.KmDiff,
                        t.Equipment,
                        t.Field,
                        t.PD,
                        t.JobDescription,
                        t.OffReason,
                        t.Hours
                        );

                }
            }


            return execute(table);
        }

        public string groupResult(IQueryable<Working> ws)
        {
            var summary = from w in ws
                          group w by new
                          {
                              w.EmployeeID,
                              w.Employee.FirstMidName
                          }
                              into grouped
                              select new EmployeeSummary
                              {
                                  EmployeeID = grouped.Key.EmployeeID,
                                  EmployeeName = grouped.Key.FirstMidName,
                                  TotalHour = grouped.Sum(s => s.Hours),
                                  TotalBank = grouped.Sum(s => s.Bank),
                                  TotalOT = grouped.Sum(s => s.OT)
                              };


            var timesheets = summary.ToList();
            var table = new System.Data.DataTable("EmployeeSummary");
            table.Columns.Add("Employee ID", typeof(int));
            table.Columns.Add("Employee Name", typeof(string));
            table.Columns.Add("Total Hour", typeof(double));
            table.Columns.Add("Total Bank", typeof(double));
            table.Columns.Add("Total OT", typeof(double));

            foreach (var t in timesheets)
            {
                table.Rows.Add
                    (
                    t.EmployeeID,
                    t.EmployeeName,
                    t.TotalHour,
                    t.TotalBank,
                    t.TotalOT
                    );
            }


            return execute(table);
        }


        private string execute(System.Data.DataTable table)
        {
            var grid = new GridView();
            grid.DataSource = table;
            grid.DataBind();

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            //return Content(sw.ToString(), "application/ms-excel");
            return sw.ToString();
        }
    }
}