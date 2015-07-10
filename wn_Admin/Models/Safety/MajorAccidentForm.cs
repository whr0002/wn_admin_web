using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.Safety
{
    public class MajorAccidentForm
    {
        public int MajorAccidentFormID { get; set; }


        public string Name { get; set; }
        public string TypeInjury { get; set; }

        public bool BodyLeft { get; set; }
        public bool BodyRight { get; set; }

        public string HospitalName { get; set; }
        public string DoctorName { get; set; }
        public string FirstAidAttendent { get; set; }
        public string EquipmentInvolved { get; set; }

        public string WCBFormCompSubm { get; set; }
        public string OHSNotified { get; set; }
        public string OHSNameContact { get; set; }

        public string DescLoss { get; set; }

        public string InjuriesDueToIncident { get; set; }

        public string LostTime { get; set; }

        [DisplayName("Equipment taken out of service")]
        public string EquipOut { get; set; }

        public string OtherLosses { get; set; }

        public string WitnessesOccur { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DisplayName("Date of Occurance")]
        public DateTime DateOccur { get; set; }

        public string TimeOccur { get; set; }

        public string LocationOccur { get; set; }



        public string AccidentPPEOption { get; set; }

        public string ListPPE { get; set; }

        public string WasPersonTrained { get; set; }

        public string PotReoccur { get; set; }

        public string LossPotEval { get; set; }

        public string LocationSpill { get; set; }

        public string SourceSpill { get; set; }

        public string TypeSubstance { get; set; }

        public string AmountSubstance { get; set; }

        public string AdvEffOn { get; set; }

        public string AccidentDesc { get; set; }

        public bool PhotoTaken { get; set; }

        public string WitnessStatement { get; set; }


        // Accident Cause Analysis
        public string ImmediateCauses { get; set; }
        public string KeyStates { get; set; }
        public string SubstandardActions { get; set; }
        public string SubstandardConditions { get; set; }
        public string BasicCauses { get; set; }
        public string PersonalFactors { get; set; }
        public string JobFactors { get; set; }
        public string RiskAssess { get; set; }
        public string TempActionTaken { get; set; }
        public string SuggestedCorrActions { get; set; }

        public string PreparedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime PrepareDate { get; set; }

        public string ApprovedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime ApprovedDate { get; set; }







        public virtual ICollection<AccidentType> AccidentTypes { get; set; }
        public virtual ICollection<AccidentPhoto> AccidentPhotos { get; set; }
    }
}