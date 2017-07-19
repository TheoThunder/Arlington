using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using Data.Domain;
using Data.Repositories.Static;
using Web.Service.Abstract;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Web.ViewModel;

namespace Web.Controllers
{
    public class UserListController : Controller
    {
        //
        // GET: /UserList/
         private IUserRepository _UserRepository;

         public UserListController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
           
        }

        public ActionResult AssignIndex()
        {

            Web.ViewModel.ImportLeadViewModel il = new Web.ViewModel.ImportLeadViewModel();


            var results = _UserRepository.GetAllUsers();
            il.users = results;

            return View(il);
        }

    }
}
