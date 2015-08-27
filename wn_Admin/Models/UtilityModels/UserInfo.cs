using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class UserInfo
    {

        private UserManager<ApplicationUser> mUserManager;
        private ApplicationDbContext mContext;
        private wn_admin_db mDb;

        public UserInfo()
        {

            mContext = new ApplicationDbContext();
            mDb = new wn_admin_db();
            mUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(mContext));

        }

        public List<string> getRoles(string userId)
        {
            var roles = mUserManager.GetRoles(userId) as List<string>;
            return roles;
        }

        public string getFirstRole(string userId)
        {
            var roles = mUserManager.GetRoles(userId) as List<string>;
            if (roles != null && roles.Count > 0)
            {
                return roles[0];
            }

            return null;
        }

        public Employee getEmployee(string userId)
        {
            var ue = mDb.UserEmployees.Find(userId);

            if (ue != null)
            {
                return ue.Employee;
            }

            return null;
        }

        public bool isInRole(string userId, string role)
        {
            return mUserManager.IsInRole(userId, role);
        }

        public bool isInRole(int employeeID, string role)
        {
            var userId = mDb.UserEmployees.Where(w => w.EmployeeID == employeeID).Select(s => s.UserID).FirstOrDefault();
            return mUserManager.IsInRole(userId, role);
        }
    }
}