using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.Safety
{
    public class AccidentType
    {
        public int AccidentTypeID { get; set; }
        public string AccidentTypeName { get; set; }

        
        public int MajorAccidentFormID { get; set; }

        public virtual MajorAccidentForm MajorAccidentForm { get; set; }
    }
}