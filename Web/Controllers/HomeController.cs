using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IUserRepository _UserRepository;

        public HomeController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);

            if (user.AssignedRoleId == 1)
                return RedirectToAction("Indexad", "Home");
            else if (user.AssignedRoleId == 3)
                return RedirectToAction("Indexsa", "Home");
            else if (user.AssignedRoleId == 4)
                return RedirectToAction("Indexaa", "Home");
            else if (user.AssignedRoleId == 5)
                return RedirectToAction("Indexcsr", "Home");

            else
            {
                Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
                uc.user = user;

                return View(uc);
            }
        }
        public ActionResult Indexsa()
        {
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);
            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
            uc.user = user;

            return View(uc);
        }
        public ActionResult Indexaa()
        {
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);
            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
            uc.user = user;

            return View(uc);
        }
        public ActionResult Indexad()
        {
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);

            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
            uc.user = user;

            return View(uc);
        }

        public ActionResult Indexcsr()
        {
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);

            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
            uc.user = user;

            return View(uc);
        }

        public ActionResult Error(String id)
        {
            Web.ViewModel.ErrorViewModel evm = new ViewModel.ErrorViewModel();

            if (id == null)
            {
                id = "Unknown Error";
            }
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            evm.user = user;
            evm.Content = id;
            return View(evm);
        }
    }
}
