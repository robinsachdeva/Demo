using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCoEPhase2.Models
{
    public class bank
    {
        [Key]
        public int idBank { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
    }
}