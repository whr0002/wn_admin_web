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

        [DisplayName("Name of involved")]
        public string Name { get; set; }

        [DisplayName("Type of injury")]
        public string TypeInjury { get; set; }

        [DisplayName("Body Left")]
        public bool BodyLeft { get; set; }
        [DisplayName("Body Right")]
        public bool BodyRight { get; set; }

        [DisplayName("Name of hospital")]
        public string HospitalName { get; set; }

        [DisplayName("Name of Doctor")]
        public string DoctorName { get; set; }

        [DisplayName("First aid attendant")]
        public string FirstAidAttendent { get; set; }

        [DisplayName("Equipment involved")]
        public string EquipmentInvolved { get; set; }

        [DisplayName("WCB forms completed and submitted?")]
        public string WCBFormCompSubm { get; set; }

        [DisplayName("Was OH&S / AB Environment notified?")]
        public string OHSNotified { get; set; }

        [DisplayName("Name of contact")]
        public string OHSNameContact { get; set; }

        [DisplayName("Description of loss")]
        public string DescLoss { get; set; }

        [DisplayName("Injuries resulted due to incident?")]
        public string InjuriesDueToIncident { get; set; }

        [DisplayName("Lost time")]
        public string LostTime { get; set; }

        [DisplayName("Equipment taken out of service")]
        public string EquipOut { get; set; }

        [DisplayName("Other losses")]
        public string OtherLosses { get; set; }

        [DisplayName("Witnesses to the occurrence")]
        public string WitnessesOccur { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DisplayName("Date of Occurance")]
        public DateTime DateOccur { get; set; }

        [DisplayName("Time of occurrence")]
        public string TimeOccur { get; set; }

        [DisplayName("Exact location of occurrence")]
        public string LocationOccur { get; set; }


        [DisplayName("Personal protective equipment")]
        public string AccidentPPEOption { get; set; }

        [DisplayName("List PPE")]
        public string ListPPE { get; set; }

        [DisplayName("Was the person trained to perform this activity / task?")]
        public string WasPersonTrained { get; set; }

        [DisplayName("Potential for re-occurrence")]
        public string PotReoccur { get; set; }

        [DisplayName("Loss Potential Evaluation")]
        public string LossPotEval { get; set; }

        [DisplayName("Location of spill / release")]
        public string LocationSpill { get; set; }

        [DisplayName("Source of spill / release")]
        public string SourceSpill { get; set; }

        [DisplayName("Type of substance involved")]
        public string TypeSubstance { get; set; }

        [DisplayName("Amount of substance spilled / released")]
        public string AmountSubstance { get; set; }

        [DisplayName("Adverse effect of spill / release on")]
        public string AdvEffOn { get; set; }

        [DisplayName("Employee Statement")]
        public string AccidentDesc { get; set; }

        [DisplayName("Photos Taken")]
        public bool PhotoTaken { get; set; }

        [DisplayName("Witness Statement(s)")]
        public string WitnessStatement { get; set; }


        // Accident Cause Analysis
        [DisplayName("Immediate causes/Critical Error (circle)")]
        public string ImmediateCauses { get; set; }

        [DisplayName("Key States")]
        public string KeyStates { get; set; }

        [DisplayName("Substandard actions")]
        public string SubstandardActions { get; set; }

        [DisplayName("Substandard conditions")]
        public string SubstandardConditions { get; set; }

        [DisplayName("Basic causes")]
        public string BasicCauses { get; set; }

        [DisplayName("Personal Factor")]
        public string PersonalFactors { get; set; }

        [DisplayName("Job Factors")]
        public string JobFactors { get; set; }

        [DisplayName("Risk Assessment")]
        public string RiskAssess { get; set; }

        [DisplayName("Temporary actions taken to address incident")]
        public string TempActionTaken { get; set; }

        [DisplayName("Suggested corrective actions that should be taken to prevent reoccurrence")]
        public string SuggestedCorrActions { get; set; }

        [DisplayName("Prepared by")]
        public string PreparedBy { get; set; }

        [DisplayName("Prepare Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime PrepareDate { get; set; }

        [DisplayName("ApprovedBy")]
        public string ApprovedBy { get; set; }

        [DisplayName("Approved Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime ApprovedDate { get; set; }







        public virtual ICollection<AccidentType> AccidentTypes { get; set; }
        public virtual ICollection<AccidentPhoto> AccidentPhotos { get; set; }
    }
}