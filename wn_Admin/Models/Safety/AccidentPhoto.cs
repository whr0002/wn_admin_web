using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.Safety
{
    public class AccidentPhoto
    {
        public int AccidentPhotoID { get; set; }
        public int MajorAccidentFormID { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }


        public virtual MajorAccidentForm MajorAccidentForm { get; set; }
    }
}