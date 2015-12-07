using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCoEPhase2.Models;
using WebCoEPhase2.DAL;
using WebCoEPhase2.ViewModelCoE;

namespace WebCoEPhase2.Repository
{
    public class UserRepository : IUserRepository
    {

        Dal odb = new Dal();


        public IEnumerable<UserDetails> GetAll()
        {

            return odb.Users.ToList();
            
        }

        public UserDetails Get(string customerID)
        {
            UserDetails user = (from o in odb.Users
                                where o.user_id == customerID
                                select o).SingleOrDefault();

            return user;
        }

        public void Add(UserDetails item)
        {
            odb.Users.Add(item);
            odb.SaveChanges();
        }

        public void Remove(string userid)
        {
            UserDetails query = (from o in odb.Users
                                 where o.user_id == userid
                                 select o).SingleOrDefault();
            odb.Users.Remove(query);
            odb.SaveChanges();
        }

        public int Update(UserDetails item)
        {

            int i = 0;
            if (item != null)
            {

                UserDetails cut = (from o in odb.Users
                                   where o.user_id == item.user_id
                                   select o).SingleOrDefault();
                cut.emailid = item.emailid;

                cut.phoneno = item.phoneno;
                odb.SaveChanges();
                i = 1;

            }
            else
            {
                i = 0;

            }
            return i;
        }
    }
}