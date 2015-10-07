using LinqToExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CompanyModels;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize(Roles = "SUPERADMIN")]
    public class AdminController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: Admin
        public void ReadEmployeeExcel()
        {
            var path = Server.MapPath("~/App_Data/OldData151002.xls");
            var excel = new ExcelQueryFactory(path);

            var emps = from e in excel.Worksheet<Sheet1>()
                       select e;

            Response.Write("Size: " + emps.Count());

            int i = 0;
            foreach (var e in emps)
            {
                Working w = new Working();
                w.Date = e.Date;
                w.EndDate = e.Date;
                w.EmployeeID = e.EmployeeID;
                w.ProjectID = e.ProjectID;
                w.Task = e.Task;
                w.Identifier = e.Identifier;
                w.Veh = e.Veh;
                w.Crew = e.Crew;
                w.StartKm = e.StartKm;
                w.EndKm = e.EndKm;
                w.Field = e.Field;
                w.PD = e.PD;
                w.JobDescription = e.JobDescription;
                w.OffReason = e.OffReason;
                w.Hours = e.Hours;
                w.Bank = e.Bank;
                w.OT = e.OT;
                w.PPYr = e.PPyr;
                w.PP = e.PayPeriod;
                w.Equipment = e.Equipment;
                w.isReviewed = e.isReviewed;
                w.Reviewer = db.Employees.Where(x => x.EmployeeID == e.SupervisorID).Select(s => s.FullName).FirstOrDefault();
                w.isReviewed = true;

                w.Employee = db.Employees.Find(e.EmployeeID);
                w.Project = db.Projects.Find(e.ProjectID);

                db.Workings.Add(w);
                db.SaveChanges();

                WorkingSupervisor ws = new WorkingSupervisor();
                ws.EmployeeID = e.SupervisorID;
                ws.WorkingID = w.WorkingID;
                ws.Employee = db.Employees.Find(e.SupervisorID);
                ws.Working = w;
               

                db.WorkingSupervisors.Add(ws);

                db.SaveChanges();

      
            }

        }

        public void addProjectID()
        {
            var path = Server.MapPath("~/App_Data/OldData151002.xls");
            var excel = new ExcelQueryFactory(path);

            var emps = (from e in excel.Worksheet<Sheet1>()
                       select e);

            Response.Write("Size: " + emps.Count() + "<br />");

            var pids = emps.Select(s => s.ProjectID).Distinct();
            var pidsInDb = db.Projects;
            var clientTable = new Hashtable();
            var projectTable = new Hashtable();
            // Add existing project ID to hash table
            foreach (var p in pidsInDb) {
                var clientpart = p.ProjectID.Substring(0, (p.ProjectID.IndexOf('-')));
                clientTable[clientpart] = p.Client;
                projectTable[p.ProjectID] = p.Client;
            }


            foreach (var e in pids)
            {
                var clientpart = e.Substring(0, (e.IndexOf('-')));

                if (clientTable[clientpart] == null)
                {
                    // The client does not exist, create one
                    var newClient = new Client();
                    newClient.ClientName = clientpart;
                    db.Clients.Add(newClient);
                    db.SaveChanges();

                    clientTable[clientpart] = newClient.ClientID;

                    Response.Write("Created client: " + newClient.ClientName + "<br />");
                }

                // The client exists now, check if the project ID exists.
                createProject(projectTable, clientTable, clientpart, e);
                
            }
        }

        private void createProject(Hashtable projectTable, Hashtable clientTable, String clientpart, String e) {
            if (projectTable[e] == null)
            {
                // Project ID  does not exists, create it
                var oldClient = db.Clients.Find(clientTable[clientpart]);
                var newProject = new Project();


                newProject.ProjectID = e;
                newProject.ProjectName = e;
                newProject.Client = (int)clientTable[clientpart];
                newProject.FK_Client = oldClient;

                db.Projects.Add(newProject);
                db.SaveChanges();

                projectTable[e] = newProject.Client;

                Response.Write("Created project: " + newProject.ProjectID + "<br />");
            }
        }


        public void addRelation()
        {
            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            tss.migrate();
            Response.Write("Relations added.");
        }

        public void addSupervisor()
        {
            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            tss.addSupervisors();
            Response.Write("Supervisors added.");
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

        public void sendEmail()
        {
            EmailNotificationService ens = new EmailNotificationService();
            ens.sendReviewNotification();
            Response.Write("Sent Email done");
        }

        class CUMPJ
        {
            public string ID { get; set; }
            public string Project { get; set; }
            public string Client { get; set; }
        }

        class Sheet1
        {
            public int WorkingID { get; set; }

            public DateTime Date { get; set; }

            public int EmployeeID { get; set; }

            public string ProjectID { get; set; }

            public int SupervisorID { get; set; }

            public string Task { get; set; }

            public string Identifier { get; set; }

            public string Veh { get; set; }

            public string Crew { get; set; }

            public int? StartKm { get; set; }

            public int? EndKm { get; set; }

            public string Field { get; set; }

            public bool PD { get; set; }

            public string JobDescription { get; set; }

            public string OffReason { get; set; }

            public double Hours { get; set; }

            public double? Bank { get; set; }

            public double? OT { get; set; }

            public int PPyr { get; set; }

            public int PayPeriod { get; set; }




            public DateTime EndDate { get; set; }

            public string Equipment { get; set; }

            public bool isReviewed { get; set; }

            public string Reviewer { get; set; }
        }
    }



}