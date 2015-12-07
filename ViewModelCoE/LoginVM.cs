using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCoEPhase2.Models;

namespace WebCoEPhase2.ViewModelCoE
{
    public class LoginVM
    {
        public Login login { get; set; }

        public List<Login> logins { get; set; }

        public UserDetails use { get; set; }

        public List<UserDetails> users { get; set; }

        public CountryDetails country { get; set; }

        public CountryDetails countries { get; set; }

        public bank bank { get; set; }

        public List<bank> banks { get; set; }

        public transaction trans { get; set; }

        public List<transaction> transactions { get; set; }
    }
}