using MembershipApplication.DAL;
using MembershipApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MembershipApplication.Controllers
{
    public class AdminController : Controller
    {
        private MembershipContext db = new MembershipContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var user = db.Users.Where(p => p.Email == User.Identity.Name).FirstOrDefault();

                if (user != null && user.IsAdmin)
                {
                    var users = db.Users.Where(u => !u.IsAdmin).ToList();
                    return View(users);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return RedirectToAction("Login", "Admin");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (IsValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["LoginStatus"] = "Login Failed, Invalid admin creadentials!!!";
                return View(user);
            }
        }

        public ActionResult LogOut()
        {
            HttpSessionStateBase session = Session;
            HttpResponseBase response = Response;
            session.Abandon();
            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();
            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);


            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            bool IsValid = false;

            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                if (user.Password == password && user.IsAdmin)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }
    }
}