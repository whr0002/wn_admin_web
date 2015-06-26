using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class LinkViewModel
    {
        public string LinkName { get; set; }
        public string Link { get; set; }
    }

    public class ListLinkViewModel
    {
        public string ListName { get; set; }
        public string color { get; set; }
        public List<LinkViewModel> ListLinks { get; set; }

        public ListLinkViewModel()
        {
            this.ListLinks = new List<LinkViewModel>();
        }
    }

    public class LinkGroupViewModel
    {
        public List<ListLinkViewModel> sections { get; set; }

        public LinkGroupViewModel()
        {
            this.sections = new List<ListLinkViewModel>();
        }
    }
}