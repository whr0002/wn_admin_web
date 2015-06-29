using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CModels
{
    public class Vehicle
    {
        
        public int VehicleID { get; set; }
        [Index(IsUnique=true)]
        [MaxLength(100)]
        [DisplayName("Vehicle Number")]
        public string VehicleName { get; set; }
    }
}