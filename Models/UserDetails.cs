using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCoEPhase2.Models
{
    public class UserDetails
    {
        [Key]
        public string user_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string emailid { get; set; }
        public string phoneno { get; set; }
        public string location_id { get; set; }
        public string p_id { get; set; }
        public string amount { get; set; }
    }
}