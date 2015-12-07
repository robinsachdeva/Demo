using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCoEPhase2.Models
{
    public class transaction
    {
        [Key]
        public int tid { get; set; }
        public string userid { get; set; }
        [Required]

        public string bankname { get; set; }
             [Required]
        public string ifsc { get; set; }
        public double transferfund { get; set; }
        public double availablefund { get; set; }
        public DateTime date_ { get; set; }
           [Required]
        public string beneficiary { get; set; }
        public string naration { get;set; }


    }
}