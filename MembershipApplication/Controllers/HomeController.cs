using MembershipApplication.DAL;
using MembershipApplication.Global;
using MembershipApplication.Models;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace MembershipApplication.Controllers
{
    public class HomeController : Controller
    {
        private MembershipContext db = new MembershipContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMember([Bind(Include = "Name,Age,Gender,PhoneNumber,Address,Email")]User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userexist = db.Users.Where(p => p.Email == user.Email).Count();
                    if (userexist <= 0)
                    {
                        MembershipNumberGenerator.MembershipNum++;
                        
                        user.MembershipNumber = "gomic" + MembershipNumberGenerator.MembershipNum;
                        db.Users.Add(user);
                        db.SaveChanges();

                        TempData["NewMemberAdded"] = "You are now our member, Your membership Id is " + user.MembershipNumber + "!!!";
                        return RedirectToAction("AddMember", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User already exist!!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct!!!");
                }
            }
            catch (DbEntityValidationException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator !!!");
            }
            return View();
        }

    }
}