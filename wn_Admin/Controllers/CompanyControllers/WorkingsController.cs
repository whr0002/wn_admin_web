using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CModels;
using wn_Admin.Models.CompanyModels;
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant, Employee")]
    public class WorkingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo mUserInfo = new UserInfo();

        // GET: Workings
        public ActionResult Index(int[] employeeId, string[] tasks, int? page, DateTime? startDate = null, DateTime? endDate = null, int ppYear = -1, int pp = -1, int clientId = -1, string projectId = null, string isReviewed = null, Boolean? exportToExcel = null, Boolean? isGroupBy = null)
        {

            string userId = User.Identity.GetUserId();
            var employee = mUserInfo.getEmployee(userId);

            DataFilterService dfs = new DataFilterService();
            var dataFilterModel = dfs.filter(db, employeeId, tasks, page, startDate, endDate, ppYear, pp, clientId, projectId, isReviewed);
            var workings = dataFilterModel.Workings;
            ViewBag.CurrentFilter = dataFilterModel.filters;

            EmployeeFormViewModel efvm = new EmployeeFormService().getEmployeeFormViewModel(db, userId, mUserInfo, employee, workings);
            ViewBag.hasReviewControl = efvm.hasReviewControl;
            ViewBag.hasFullControl = efvm.hasFullControl;
            ViewBag.EmployeeID = efvm.EmployeeID;
            ViewBag.clientId = efvm.clientId;
            ViewBag.projectId = efvm.projectId;
            ViewBag.CurrentEID = efvm.CurrentEID;
            ViewBag.EID = efvm.EID;
            ViewBag.isReviewed = efvm.isReviewed;
            ViewBag.tasks = efvm.tasks;


            workings = efvm.workings;

            // Export to Excel?
            if (exportToExcel != null && exportToExcel == true)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=timesheets.xls");
                Response.Charset = "";
                ExcelService excelService = new ExcelService();

                if (isGroupBy != null && isGroupBy == true)
                {
                    return Content(excelService.groupResult(workings), "application/ms-excel");
                }

                return Content(excelService.generateExcel(workings), "application/ms-excel");
            }


            // Generate time sheet summary
            //var tempE = mUserInfo.getEmployee(userId);
            //ViewBag.TMSummary = workings.Where(w => w.EmployeeID == tempE.EmployeeID).GroupBy(g => new { g.EmployeeID, g.Employee.FullName }).Select(
            //    s => new TimesheetSummaryViewModel
            //    {
            //        EmployeeName = s.Key.FullName,
            //        TotalHours = s.Sum(b => b.Hours),
            //        DateRange = dateRange
            //    }).ToList();

            // Order by date
            workings = workings.OrderByDescending(o => o.Date);

            return View(workings.ToList());
        }

        // GET: Workings/Details/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Working working = db.Workings.Find(id);
            if (working == null)
            {
                return HttpNotFound();
            }
            return View(working);
        }

        // GET: Workings/Create
        public ActionResult Create()
        {


            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            ViewBag.Field = new SelectList(db.FieldAccesses.OrderBy(o => o.FieldAccessName), "FieldAccessName", "FieldAccessName");
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonName", "OffReasonName");
            ViewBag.Task = new SelectList(db.Tasks, "TaskName", "TaskName");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleName", "VehicleName");
            ViewBag.ProjectID = new SelectList(db.Projects.Where(w => w.Status == 1).OrderBy(o => o.ProjectName), "ProjectID", "ProjectName");
            ViewBag.Equipment = new MultiSelectList(db.Equipments.OrderBy(o => o.EquipmentName), "EquipmentName", "EquipmentName");

            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            var currentEmployee = mUserInfo.getEmployee(User.Identity.GetUserId());
            ViewBag.Supervisors = tss.getSupervisorList(currentEmployee.EmployeeID);

            setEmployeeDropdowns();

            Working working = new Working();

            working.Date = TimesheetDateValidator.setStartTime(TimesheetDateValidator.getEdmontonTime());
            working.EndDate = TimesheetDateValidator.setEndTime(TimesheetDateValidator.getEdmontonTime());

            PayPeriodCalculator calc = new PayPeriodCalculator();
            PPViewModel ppv = calc.getPayPeriod(working.Date);
            working.PPYr = ppv.PPYear;
            working.PP = ppv.PPNumber;
            //working.OffReason = 1;

            return View(working);
        }

        public ActionResult CreateOff()
        {

            setEmployeeDropdowns();
            var userId = User.Identity.GetUserId();
            if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
            {
                ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonName", "OffReasonName");
            }
            else
            {
                ViewBag.OffReason = new SelectList(db.OffReasons.Where(
                    w => w.OffReasonName.Equals("Regular Day Off") ||
                        w.OffReasonName.Equals("Off w/o Pay") ||
                        w.OffReasonName.Equals("Sick")
                    ), "OffReasonName", "OffReasonName");
            }

            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            var currentEmployee = mUserInfo.getEmployee(userId);
            ViewBag.Supervisors = tss.getSupervisorList(currentEmployee.EmployeeID);

            Working working = new Working();

            working.Date = DateTime.Today;
            working.EndDate = working.Date;
            PayPeriodCalculator calc = new PayPeriodCalculator();
            PPViewModel ppv = calc.getPayPeriod(working.Date);
            working.PPYr = ppv.PPYear;
            working.PP = ppv.PPNumber;

            working.ProjectID = "0-0-2015";
            //working.Task = "None";


            return View(working);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOff(int[] Supervisors, [Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP, ProjectID,Task, JobDescription,OffReason")] Working working)
        {
            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            SupervisorValidator supervisorValidator = new SupervisorValidator();
            var result = supervisorValidator.validate(Supervisors);

            if (ModelState.IsValid && result == null)
            {
                working.ProjectID = "0-0-2015";
                if (working.EndDate >= working.Date)
                {

                    db.Workings.Add(working);
                    db.SaveChanges();
                    tss.create(working.WorkingID, Supervisors);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("EndDate", "'Date To' must be later than 'Date From'");
                }

            }
            else if (result != null)
            {
                ModelState.AddModelError("Supervisors", result);
            }


            var currentEmployee = mUserInfo.getEmployee(User.Identity.GetUserId());
            ViewBag.Supervisors = tss.getSupervisorList(currentEmployee.EmployeeID);
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            setEmployeeDropdowns();
            return View(working);
        }

        // POST: Workings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,OffReason,Hours")] Working working)
        //{


        //    if (ModelState.IsValid)
        //    {

        //        // Check date to see if it is valid.
        //        string validationError = TimesheetDateValidator.ValidateTimesheetDateRange(working.Date, working.EndDate);
        //        string userId = User.Identity.GetUserId();

        //        // For working days, set end date to start date
        //        working.EndDate = working.Date;

        //        if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
        //        {
        //            db.Workings.Add(working);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            if (validationError != null)
        //            {
        //                ModelState.AddModelError("Date", validationError);
        //            }
        //            else
        //            {
        //                db.Workings.Add(working);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //        }

        //    }


        //    ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
        //    setEmployeeDropdowns();
        //    ViewBag.Field = new SelectList(db.FieldAccesses.OrderBy(o => o.FieldAccessName), "FieldAccessID", "FieldAccessName", working.Field);
        //    ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", working.OffReason);
        //    ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName", working.Task);
        //    ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName", working.Veh);
        //    ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
        //    return View(working);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ajaxCreate(string[] Equipment, string[] Field, int[] Supervisors, [Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,PD,JobDescription,OffReason,Hours")] Working working)
        {
            SupervisorValidator supervisorValidator = new SupervisorValidator();
            var result = supervisorValidator.validate(Supervisors);

            if (ModelState.IsValid && result == null)
            {


                // Check date to see if it is valid.
                string validationError = TimesheetDateValidator.ValidateTimesheetDateRange(working.Date, working.EndDate);
                string userId = User.Identity.GetUserId();

                // Combine equipments
                if (Equipment != null)
                {
                    string equipments = string.Join(", ", Equipment);
                    working.Equipment = equipments;
                }

                // Combine Field Access
                if (Field != null)
                {
                    string fields = string.Join(", ", Field);
                    working.Field = fields;
                }



                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    createWorking(working, Supervisors);
                }
                else
                {
                    if (validationError != null)
                    {
                        Response.Write(validationError);
                        //ModelState.AddModelError("Date", validationError);
                    }
                    else
                    {
                        createWorking(working, Supervisors);
                    }
                }
            }
            else
            {
                var errors = string.Join("<br />", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).Select(s => s.ErrorMessage).ToArray());
                if (result != null) errors += "<br />" + result;
                Response.Write(errors);
            }
        }

        private void createWorking(Working working, int[] sids)
        {
            VehicleService vehicleService = new VehicleService();
            TimesheetSupervisorService tss = new TimesheetSupervisorService();

            vehicleService.updateVehicleStatus(working.Veh, working.EndKm);
            db.Workings.Add(working);
            db.SaveChanges();
            tss.create(working.WorkingID, sids);

            Response.Write("valid");
        }

        // GET: Workings/Edit/5
        [Authorize(Roles = "Accountant, SUPERADMIN, Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Working working = db.Workings.Find(id);
            if (working == null)
            {
                return HttpNotFound();
            }

            var employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (mUserInfo.isInRole(User.Identity.GetUserId(), "Employee") && employee.EmployeeID != working.EmployeeID)
            {
                // Only allow users to edit their own time sheets, exception for Accountant, Superadmin
                return HttpNotFound();
            }

            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            setEmployeeDropdowns();

            ViewBag.Field = new SelectList(db.FieldAccesses.OrderBy(o => o.FieldAccessName), "FieldAccessName", "FieldAccessName");
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonName", "OffReasonName");
            ViewBag.Task = new SelectList(db.Tasks, "TaskName", "TaskName");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleName", "VehicleName");
            ViewBag.ProjectID = new SelectList(db.Projects.Where(w => w.Status == 1), "ProjectID", "ProjectName");
            ViewBag.Equipment = new MultiSelectList(db.Equipments, "EquipmentName", "EquipmentName");

            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            var currentEmployee = mUserInfo.getEmployee(User.Identity.GetUserId());
            ViewBag.Reviewers = tss.getSupervisorListWithValues(currentEmployee.EmployeeID, working);

            return View(working);
        }

        // POST: Workings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accountant, SUPERADMIN, Employee")]
        public ActionResult Edit(int[] Supervisors, [Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,PD,JobDescription,OffReason,Hours, Equipment,Field")] Working working)
        {
            var employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (mUserInfo.isInRole(User.Identity.GetUserId(), "Employee") && employee.EmployeeID != working.EmployeeID)
            {
                // Only allow users to edit their own time sheets, exception for Accountant, Superadmin
                return HttpNotFound();
            }

            SupervisorValidator supervisorValidator = new SupervisorValidator();
            var result = supervisorValidator.validate(Supervisors);

            if (ModelState.IsValid && result == null)
            {


                // Check date to see if it is valid.
                string validationError = TimesheetDateValidator.ValidateTimesheetDateRange(working.Date, working.EndDate);
                string userId = User.Identity.GetUserId();

                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    editWorking(working, Supervisors);
                    return RedirectToAction("Index");
                }
                else
                {
                    if (validationError != null)
                    {
                        ModelState.AddModelError("Date", validationError);
                    }
                    else
                    {
                        editWorking(working, Supervisors);
                        return RedirectToAction("Index");
                    }

                }



            }
            else if (result != null)
            {
                ModelState.AddModelError("Supervisors", result);
            }
            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            setEmployeeDropdowns();

            ViewBag.Field = new SelectList(db.FieldAccesses.OrderBy(o => o.FieldAccessName), "FieldAccessName", "FieldAccessName");
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonName", "OffReasonName");
            ViewBag.Task = new SelectList(db.Tasks, "TaskName", "TaskName");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleName", "VehicleName");
            ViewBag.ProjectID = new SelectList(db.Projects.Where(w => w.Status == 1), "ProjectID", "ProjectName");
            ViewBag.Equipment = new MultiSelectList(db.Equipments, "EquipmentName", "EquipmentName");
            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            var currentEmployee = mUserInfo.getEmployee(User.Identity.GetUserId());
            ViewBag.Reviewers = tss.getSupervisorListWithValues(currentEmployee.EmployeeID, working);
            return View(working);
        }

        private void editWorking(Working working, int[] sids)
        {
            VehicleService vehicleService = new VehicleService();
            TimesheetSupervisorService tss = new TimesheetSupervisorService();

            vehicleService.updateVehicleStatus(working.Veh, working.EndKm);
            // When modified by someone, clear the reviewer name to this person.
            working.isReviewed = false;
            working.Reviewer = "";
            db.Entry(working).State = EntityState.Modified;
            db.SaveChanges();

            tss.delete(working.WorkingID);
            tss.create(working.WorkingID, sids);
        }

        // GET: Workings/Delete/5
        [Authorize(Roles = "Accountant, SUPERADMIN, Employee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Working working = db.Workings.Find(id);
            if (working == null)
            {
                return HttpNotFound();
            }

            var employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (mUserInfo.isInRole(User.Identity.GetUserId(), "Employee") && employee.EmployeeID != working.EmployeeID)
            {
                // Only allow users to edit their own time sheets, exception for Accountant, Superadmin
                return HttpNotFound();
            }

            return View(working);
        }

        // POST: Workings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accountant, SUPERADMIN, Employee")]
        public ActionResult DeleteConfirmed(int id)
        {


            Working working = db.Workings.Find(id);

            var employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (mUserInfo.isInRole(User.Identity.GetUserId(), "Employee") && employee.EmployeeID != working.EmployeeID)
            {
                // Only allow users to edit their own time sheets, exception for Accountant, Superadmin
                return HttpNotFound();
            }

            TimesheetSupervisorService tss = new TimesheetSupervisorService();
            tss.delete(working.WorkingID);

            db.Workings.Remove(working);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Review([ModelBinder(typeof(IntArrayModelBinder))] int[] ids)
        {

            //string[] sids = ids.Split(',');

            // Get current supervisor
            Employee employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (employee != null)
            {

                string userId = User.Identity.GetUserId();

                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    // User is Accountant or Superadmin, they can review everyone's timesheet
                    var wlist = from work in db.Workings
                                where ids.Contains(work.WorkingID)
                                select work;



                    foreach (var work in wlist)
                    {
                        work.isReviewed = true;
                        work.Reviewer = employee.FullName;
                    }

                    db.SaveChanges();
                }
                else if (mUserInfo.isInRole(userId, "Employee"))
                {
                    // User is an employee, check if he is supervisor or not.
                    // Get all time sheets from employees who are under THIS PERSON's supervision
                    var result = from ws in db.WorkingSupervisors
                                  join tm in db.Workings
                                  on ws.WorkingID equals tm.WorkingID
                                  where ws.EmployeeID == employee.EmployeeID && tm.isReviewed == false
                                  select tm;

                    foreach (var work in result)
                    {
                        if (ids.Contains(work.WorkingID))
                        {
                            work.isReviewed = true;
                            work.Reviewer = employee.FullName;
                        }
                    }

                    db.SaveChanges();
                }
                else
                {
                    //Response.Write("unknown role");
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }

        


        private void setEmployeeDropdowns()
        {
            string userId = User.Identity.GetUserId();
            var employee = mUserInfo.getEmployee(userId);

            if (employee != null)
            {
                IQueryable<Employee> es = db.Employees.Where(w => w.Status == 1).OrderBy(o => o.FirstMidName);

                if (!mUserInfo.isInRole(userId, "SUPERADMIN") && !mUserInfo.isInRole(userId, "Accountant"))
                {
                    es = es.Where(w => w.EmployeeID == employee.EmployeeID);
                }

                ViewBag.EmployeeID = new SelectList(es, "EmployeeID", "FullName");

            }
            else
            {
                ViewBag.EmployeeID = new SelectList(new List<Employee>(), "EmployeeID", "FullName");
            }
        }

        public ActionResult demo()
        {
            return View();
        }
    }
}
