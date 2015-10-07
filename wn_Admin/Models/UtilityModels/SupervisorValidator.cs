using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class SupervisorValidator
    {
        public string validate(int[] sids)
        {
            if (sids == null || sids.Length == 0) return "Supervisor field is required";
            if (sids[0] == 0) return "The Supervisor field is required";

            return null;
            
        }
    }
}