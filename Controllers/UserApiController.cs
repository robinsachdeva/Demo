using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCoEPhase2.Models;
using WebCoEPhase2.DAL;
using WebCoEPhase2.ViewModelCoE;
using WebCoEPhase2.Repository;

namespace WebCoEPhase2.Controllers
{
    public class UserApiController : ApiController
    {
        static readonly UserRepository repository = new UserRepository();

        [HttpGet]
        public IEnumerable<UserDetails> GetAllUsers()
        {
            return repository.GetAll();
        }
        [HttpGet]
        public UserDetails GetById(string Id)
        {
            UserDetails obj = repository.Get(Id);

            return obj;


        }
        [HttpPost]
        public void UserUpdate(UserDetails user)
        {
            if (user != null)
            {
                repository.Update(user);
            }
        }
    }
}
