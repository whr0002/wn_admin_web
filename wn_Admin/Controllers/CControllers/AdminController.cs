using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CompanyModels;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize(Roles="SUPERADMIN")]
    public class AdminController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: Admin
        public void ReadEmployeeExcel()
        {
            var path = Server.MapPath("~/App_Data/EmployeeList.xls");
            var excel = new ExcelQueryFactory(path);

            var emps = from e in excel.Worksheet<Sheet1>()
                       select e;

            Response.Write("Size: " + emps.Count());

            foreach (var e in emps)
            {
                string[] split = e.Name.Split(' ');
                

                if (split[0].Equals("Troy") || 
                    split[0].Equals("Hao") ||
                    split[0].Equals("Soman") ||
                    split[0].Equals("Bruce") ||
                    split[0].Equals("Tanner") ||
                    split[0].Equals("Mackenzie"))
                {
                    Response.Write(split[0] + " | " + split[1] + "<br />");
                    continue;
                }

                Employee employee = new Employee();
                employee.FirstMidName = split[0];
                employee.LastName = split[1];


                db.Employees.Add(employee);
                
                
            }

            db.SaveChanges();
            
            
        }

        // GET: Admin
        public void readPj()
        {
            var path = Server.MapPath("~/App_Data/pjs.xls");
            var excel = new ExcelQueryFactory(path);

            var pj = from e in excel.Worksheet<CUMPJ>()
                       select e;

            Response.Write("Size: " + pj.Count());

            foreach (var e in pj)
            {
                //Project project = new Project();
                //project.ProjectID = e.ID;
                //project.ProjectName = e.Project;
                //Client client = new Client();
                //client.ClientName = e.Client;
                //project.Client

            }

            //db.SaveChanges();


        }

        class CUMPJ
        {
            public string ID { get; set; }
            public string Project { get; set; }
            public string Client { get; set; }
        }
    }


    
}