using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCoEPhase2.Models;

namespace WebCoEPhase2.Repository
{
    interface IUserRepository
    {
        IEnumerable<UserDetails> GetAll();
        UserDetails Get(string customerID);
        void Add(UserDetails item);
        void Remove(string userid);
        int Update(UserDetails item);
    }
}
