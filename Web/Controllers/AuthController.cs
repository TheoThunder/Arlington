using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Infrastructure.Authentication;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        TrinityMembershipProvider provider = (TrinityMembershipProvider)Membership.Provider;
        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            User newUser = new User();
            newUser.ErrorMessage = " ";
            return View(newUser);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(User login)
        {
            User loggedINUser = new User();
            loggedINUser = login;
            if (ValidateLogin(login.UserName, login.newPassword))
            {
                FormsAuthentication.SetAuthCookie(login.UserName, false);

                var temp = login.newPassword;

                login = provider.AssignUser(login.UserName);
                login.newPassword = temp;


                if (login.AssignedRoleId == 1)
                    return RedirectToAction("Index", "Home");
                else if (login.AssignedRoleId == 2)
                    return RedirectToAction("Index", "Home");
                else if (login.AssignedRoleId == 3)
                    return RedirectToAction("Indexsa", "Home");
                else if (login.AssignedRoleId == 4)
                    return RedirectToAction("Indexaa", "Home");
                else if (login.AssignedRoleId == 5)
                    return RedirectToAction("Indexcsr", "Home");
                else
                    return View();
            }

            //if (!String.IsNullOrEmpty(returnurl) && returnurl != "/")
            //{
            //    return Redirect(returnurl);
            //}
            else
            {
                login.ErrorMessage = "Invalid Username or Password!";
                return View(login);
            }

        }

        private bool ValidateLogin(string username, string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "You must specify a username");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password");
            }
            if (ModelState.IsValid)
            {
                if (!provider.ValidateUser(username, password))
                {
                    ModelState.AddModelError("_FORM", "The username or password is incorrect");
                }

            }
            
            foreach (var modelStateValue in ViewData.ModelState.Values)
            {
                foreach (var error in modelStateValue.Errors)
                {
                    // Do something useful with these properties
                    var errorMessage = error.ErrorMessage;
                    var exception = error.Exception;
                }
            }
            return ModelState.IsValid;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToActionPermanent("Login");
        }
    }
}
