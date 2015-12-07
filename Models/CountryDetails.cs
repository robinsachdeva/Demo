using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCoEPhase2.Models
{
    public class CountryDetails
    {
        [Key]
        public string location_id { get; set; }
        public string country_name { get; set; }
        public string country_id { get; set; }
    }
}