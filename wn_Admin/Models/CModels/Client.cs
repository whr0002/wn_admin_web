using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models
{
    public class Client
    {
        [DisplayName("Client")]
        public int ClientID { get; set; }
        [Index(IsUnique=true)]
        [MaxLength(100)]
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
    }
}