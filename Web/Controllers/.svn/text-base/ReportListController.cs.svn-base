using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using INF = Infrastructure;
using System;
using Newtonsoft.Json;
using Data.Domain;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReportListController : Controller
    {

        public IUserRepository _UserRepository;

        public ReportListController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public ActionResult Index()
        {
            ReportsViewModel vm = new ReportsViewModel();
            var username = HttpContext.User.Identity.Name;
            vm.user = _UserRepository.GetUserByUsername(username);

            return View(vm);
        }

    }
}
