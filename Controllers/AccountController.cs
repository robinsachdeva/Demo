using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCoEPhase2.ViewModelCoE;
using WebCoEPhase2.DAL;
using WebCoEPhase2.Models;
using System.Web.Security;
using WebCoEPhase2.Repository;


namespace WebCoEPhase2.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        LoginVM obj = new LoginVM();
        [HttpGet]
        public ActionResult Index()
        {
            //Dal oDb = new Dal();



            obj.login = new Login();
            //List<Login> loginList = (from o in oDb.Logins
            //                         select o).ToList<Login>();

            //oLoginVM.logins = loginList;
            return View("Index", obj);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Submit(Login user)
        {
            
            if (ModelState.IsValid)
            {
                Dal oDb = new Dal();
                LoginVM obj = new LoginVM();
                string username = user.username.Trim();
                string pass = user.password.Trim();

                List<Login> loginlist = (from o in oDb.Logins
                                         where ((o.username == username) && (o.password == pass))
                                         select o).ToList<Login>();//This code should come from Repository pattern..

                if (loginlist.Count == 1)
                {

                    var id = loginlist[0].user_id;
                    var uname = loginlist[0].username;
                    var flag = loginlist[0].flag;

                    Session["Id"] = id.ToString();
                    Session["User"] = username.ToString();
                    if (flag == 0)
                    {
                       
                        ViewBag.country = oDb.countries.ToList();
                        return View("UserRegInfo");
                    }
                    FormsAuthentication.SetAuthCookie("Cookie", true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter valid Username and Password");
                return View("Index");
            }

        }

        public ActionResult UserRegistration()
        {
            

            return View("UserRegistration");
        }
        public ActionResult SubmitUser(LoginVM user)
        {
            ViewBag.errmsg = "";
            string paas1 = user.login.password;
            string paas2 = Request.Form["confirmpassword"].ToString();
            if (paas1 == paas2)
            {
                Dal oDb = new Dal();
                Login obj = new Login();
                obj.username = user.login.username.ToString();
                obj.password = user.login.password.ToString();
                oDb.Logins.Add(obj);
                oDb.SaveChanges();
            }
            else
            {
                ViewBag.errmsg = "Password does't match";
            }
            return View("UserRegistration");
        }
        [Authorize]
        public ActionResult UserRegInfo()
        {
            obj.login = new Login();
            Dal oDb = new Dal();
            ViewBag.country = oDb.countries.ToList();
            return View("UserRegInfo", obj);
        }
        [Authorize]

        public ActionResult ChangePass()
        {

            return View("ChangePass");
        }

        [Authorize]
        public ActionResult ChangePassword(Login lg)
        {
            ViewBag.mg = "";
            Dal odb = new Dal();
            string oldpass = Request.Form["password1"].ToString();
            string newpass = Request.Form["password"].ToString();
            string confirmpass = Request.Form["password2"].ToString();
            string userid = Session["Id"].ToString();
            var actualpass = (from o in odb.Logins
                              where o.user_id == userid
                              select o.password).FirstOrDefault();

            string dddd = actualpass.ToString();
            if (oldpass != actualpass.ToString())
            {
                ViewBag.msg = "Old password is not correct!...";

            }
            if (newpass != confirmpass)
            {
                ViewBag.msg = "New and Confirm password must match!...";
            }
            if (ModelState.IsValid)
            {

                UserRepository rp = new UserRepository();
                lg.user_id = Session["Id"].ToString();

                Login user = (from o in odb.Logins
                              where o.user_id == lg.user_id
                              select o).SingleOrDefault();
                user.password = lg.password.Trim();
                odb.SaveChanges();
                ViewBag.msg = "Password is changed!...";
            }


            return View("ChangePass");
        }

      
        public ActionResult SubmitUserReg()
        {
            string pass1=Request.Form["password"].ToString();
            string pass2=Request.Form["confirmpassword"].ToString();
            string username = Request.Form["username"].ToString();
            string password = Request.Form["password"].ToString(); 
            ViewBag.msg = "";
            if (pass1 == pass2)
            {

                Dal odal = new Dal();
                List<Login> loginList = (from o in odal.Logins
                                         select o).ToList<Login>();

                bool aa = loginList.Any(a => a.username == username);
                if (aa == true)
                {
                    
                    ViewBag.msg = "User alreday exists!!";
                }
                else
                {
                    var maxval = loginList.Max(a => a.user_id);

                    int maxval1 = Convert.ToInt32(maxval) + 1;

                    string real = maxval1.ToString();
                    Login obj = new Login();
                    obj.user_id = real;
                    obj.username = username;
                    obj.password = password;
                    obj.flag = 0;
                    odal.Logins.Add(obj);
                    odal.SaveChanges();
                    ViewBag.msg = "User registered!!";
                }
            }
            else
            {
                ViewBag.msg = "Password must match!!";
            }
            return View("UserRegistration");
           
        }

       
        public ActionResult SubmitUserInfo(LoginVM cou)
        {
            string userid = Session["Id"].ToString();
            string firstname = Request.Form["firstname"].ToString();
            string lastname = Request.Form["lastname"].ToString();
            string emailid = Request.Form["emailid"].ToString();
            string phoneno = Request.Form["phoneno"].ToString();

            using (Dal odal = new Dal())
            {
                UserDetails obj = new UserDetails();
                Login kk = new Login();
                obj.user_id = userid;
                obj.firstname = firstname.Trim();
                obj.lastname = lastname.Trim();
                obj.emailid = emailid.Trim();
                obj.phoneno = phoneno.Trim();
                obj.location_id = cou.country.location_id;
                obj.p_id = cou.country.location_id;
                obj.amount = "100000";
                odal.Users.Add(obj);
                odal.SaveChanges();
              
                ViewBag.country = odal.countries.ToList();
                ViewBag.msg = "User Information Saved!!";
            }
            using (Dal oda = new Dal())
            {
               
                Login loginList = (from o in oda.Logins
                                   where o.user_id == userid
                                   select o).SingleOrDefault();
                loginList.flag = 1;
               
                oda.SaveChanges();
            }
          
            return View("UserRegInfo");
        }

      



    }
}
