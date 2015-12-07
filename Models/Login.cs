using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCoEPhase2.Models
{
    public class Login
    {
        [Key]
        public string user_id { get; set; }
        [Required(ErrorMessage = "Enter User name")]
        public string username { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string password { get; set; }

      

        public int flag { get; set; }
    }
}