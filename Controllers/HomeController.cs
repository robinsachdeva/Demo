using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCoEPhase2.ViewModelCoE;
using WebCoEPhase2.DAL;
using WebCoEPhase2.Repository;
using WebCoEPhase2.Models;
using System.Web.Security;

namespace WebCoEPhase2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        LoginVM oVm = new LoginVM();
        DAL.Dal odal = new DAL.Dal();
        UserRepository rpObj = new UserRepository();
        
        public ActionResult Index()
        {
            string Id = Session["Id"].ToString();
            UserDetails obj = (from o in odal.Users
                               where o.user_id == Id
                               select o).SingleOrDefault();
            oVm.use = obj;

            List<transaction> trans = (from o in odal.transactions
                                       orderby o.date_ descending
                                       where o.userid == Id
                                       select o).ToList();
            oVm.transactions = trans;
            return View(oVm);
        }

       
        public ActionResult UpdateUser()
        {
            oVm.use = new UserDetails();
            return View("UpdateUser", oVm);

            //return RedirectToAction("UpdateUser", "Home");
        }

       
        public ActionResult UserUpdateSubmit(UserDetails obj)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg = "";
                UserRepository rp = new UserRepository();

                obj.user_id = Session["Id"].ToString();
                
                int result = rp.Update(obj);
                if (result == 1)
                {
                    ViewBag.msg = "Updated successfully....";

                }
                else
                {
                    ViewBag.msg = "Problem in Updation....";

                }
            }
            return View("UpdateUser");
        }

        //Search using angular
        public ActionResult Search()
        {

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        public ActionResult transferfund()
        {

            //ViewBag.Banks = odal.banks.ToList();
            return View();
        }

        public ActionResult transferfundsubmit()
        {
            string Id = Session["Id"].ToString();
            ViewBag.msg = "";
            
            if (ModelState.IsValid)
            {
                string userid = Id;
                string beneficiary = Request.Form["trans.beneficiary"].ToString().Trim();
                string bankname = Request.Form["trans.bankname"].ToString();
                string ifsc = Request.Form["trans.ifsc"].ToString().Trim();
                string naration = Request.Form["trans.naration"].ToString().Trim();
                double transactionfund = Convert.ToDouble(Request.Form["trans.transferfund"].ToString().Trim());
                transaction obj = new transaction();
                UserDetails use = (from o in odal.Users
                                   where o.user_id == Id
                                   select o).SingleOrDefault();

                double banl = Convert.ToDouble(use.amount);

                if (transactionfund > banl)
                {

                    ViewBag.msg = "Fund Transfer should be less then available balance!!";
                }
                else
                {
                    double availablebal = banl - transactionfund;
                    obj.userid = Session["Id"].ToString();
                    obj.beneficiary = Request.Form["trans.beneficiary"].ToString().Trim();
                    obj.bankname = Request.Form["trans.bankname"].ToString();
                    obj.ifsc = Request.Form["trans.ifsc"].ToString().Trim();
                    obj.naration = Request.Form["trans.naration"].ToString().Trim();

                    obj.date_ = DateTime.Now;
                    obj.transferfund = Convert.ToDouble(Request.Form["trans.transferfund"].ToString().Trim());
                    obj.availablefund = availablebal;

                    
                    odal.transactions.Add(obj);
                   
                    odal.SaveChanges();
                    use.amount = availablebal.ToString();
                    odal.SaveChanges();

                    ViewBag.msg = "Done!..";

                }
            }

            return View("transferfund");
        }

        public ActionResult utility()
        {
            return View("utility");
        }

        public ActionResult Report()
        {
            //oVm.transactions = new List<transaction>();
            return View();
        }

        public ActionResult SubmitReport()
        {
      

            //DateTime start = Convert.ToDateTime(Request.Form["fromdate"]);
            //DateTime to = Convert.ToDateTime(Request.Form["todate"]);
            //using(var obj=new DAL.Dal())
            //{
            //   List<transaction> kk=obj.transactions.ToList();
            //   oVm.transactions = kk;
            //    return View(oVm);
            //}

            return View("Report");
           
        }

    }
}
