using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class VehicleService
    {
        private wn_admin_db db = new wn_admin_db();
        public Boolean updateVehicleStatus(string vName, int? km){

            if (vName == null || km == null) return false;

            var vehicle = db.Vehicles.Where(w => w.VehicleName.Equals(vName)).FirstOrDefault();
            if (vehicle != null)
            {
                vehicle.currentKm = km;
                db.SaveChanges();
                return true;
            }

            return false;  
        }
    }
}