using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.Safety
{
    public class MinorAccidentForm
    {
        public int MinorAccidentFormID { get; set; }

        public string NamePosition { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime DateReported { get; set; }

        public string LocationOfEvent { get; set; }
        public string TaskConducted { get; set; }

        //public int AccidentTypeID { get; set; }
        //public int RelatingToID { get; set; }

        public string EventDesc { get; set; }
        public string CauseAnalysis { get; set; }

        // 1,2,3,4
        public int FreqExpo { get; set; }
        public int HazardProb { get; set; }

        // Yes, No
        public string FirstAid { get; set; }

        public string FirstAidDesc { get; set; }

        public string CorrAction { get; set; }




    }
}