using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class TimesheetSupervisorService
    {
        private wn_admin_db db = new wn_admin_db();

        public void create(int timesheetID, int[] supervisors)
        {
            if (supervisors != null && supervisors.Length > 0)
            {
                foreach (var s in supervisors)
                {
                    if (s != 0)
                    {
                        WorkingSupervisor ws = new WorkingSupervisor();
                        ws.WorkingID = timesheetID;
                        ws.EmployeeID = s;

                        db.WorkingSupervisors.Add(ws);      
                    }
                }
                db.SaveChanges();
            }
        }

        public void read(int timesheetID)
        {
            
        }

        public void delete(int timesheetID)
        {
            var ws = db.WorkingSupervisors.Where(w => w.WorkingID == timesheetID);
            foreach (var w in ws) {
                db.WorkingSupervisors.Remove(w);
            }
            db.SaveChanges();
        }

        public MultiSelectList getSupervisorList(int selfEID) 
        { 
            return new MultiSelectList(db.EmployeeRoles.Where(w => w.RoleID == 1 && w.EmployeeID != selfEID).Select(s => s.Employee).OrderBy(o => o.FirstMidName), "EmployeeID", "FirstMidName");
        }

        public MultiSelectList getSupervisorListWithValues(int selfEID, Working working)
        {
            return new MultiSelectList(db.EmployeeRoles.Where(w => w.RoleID == 1 && w.EmployeeID != selfEID).Select(s => s.Employee).OrderBy(o => o.FirstMidName), "EmployeeID", "FirstMidName", db.WorkingSupervisors.Where(w => w.WorkingID == working.WorkingID).Select(s => s.EmployeeID));
        }

        [Authorize(Roles="SUPERADMIN")]
        public void migrate() {
            var tsss = from w in db.Workings
                       join s in db.EmployeeSupervisions
                       on w.EmployeeID equals s.EmployeeID
                       select new EmployeeWorking
                       {
                           WorkingID = w.WorkingID,
                           EmployeeID = s.SupervisorID
                       };

            foreach (var ts in tsss)
            {
                WorkingSupervisor ws = new WorkingSupervisor();
                ws.WorkingID = ts.WorkingID;
                ws.EmployeeID = ts.EmployeeID;
                db.WorkingSupervisors.Add(ws);
            }
            


            db.SaveChanges();

        }

        [Authorize(Roles = "SUPERADMIN")]
        public void addSupervisors()
        {
            var sids = db.EmployeeSupervisions.GroupBy(g => g.SupervisorID).Select(s => s.FirstOrDefault().SupervisorID);
            foreach (var id in sids)
            {
                EmployeeRole er = new EmployeeRole();
                er.EmployeeID = id;
                er.RoleID = 1;
                db.EmployeeRoles.Add(er);
            }

            db.SaveChanges();

        }


        class EmployeeWorking {
            public int WorkingID { get; set; }
            public int EmployeeID { get; set; }
        }
    }
}