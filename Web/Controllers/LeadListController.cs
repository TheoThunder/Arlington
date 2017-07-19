using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using Data.Domain;
using Data.Repositories.Static;
using Web.Service.Abstract;
using Web.ViewModel;
namespace Web.Controllers
{
    public class LeadListController : Controller
    {
        //
        // GET: /LeadList/
        ILeadProfileService _service;
        IZoneRepository _zoneRepos;
        IUserRepository _UserRepository;
        IGenericUsageRepositoryInterface _IncodeQueriesRepos;
        ITicketRepository _TicketRepository;
         public LeadListController(IGenericUsageRepositoryInterface IncodeRepos, ILeadProfileService service, IZoneRepository zoneRepository, IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _service = service;
            _zoneRepos = zoneRepository;
            _UserRepository = userRepository;
            _IncodeQueriesRepos = IncodeRepos;
            _TicketRepository = ticketRepository;
        }

        public ActionResult Index()
        {
            Web.ViewModel.LeadListViewModel ll = new Web.ViewModel.LeadListViewModel();
            IList<Lead> leads = new List<Lead>();
            var username = HttpContext.User.Identity.Name;
            ll.user = _UserRepository.GetUserByUsername(username);

            var results = _IncodeQueriesRepos.GetWarmLeads(-1);

            IEnumerable<Zone> zones = _zoneRepos.Zones;
            IList<int> zonelist = new List<int>();
            foreach (var zone in zones)
            {
                zonelist.Add(zone.ZoneNumber);
            }
            ll.ZoneDropdown = zonelist;
            foreach (var result in results)
            {
                result.accounts = GetNumberOfAccounts(result.LeadId);

                User user = _UserRepository.GetUserById(result.AssignedAAUserId);
                if (user != null)
                {
                    result.AssignedAA = user;
                }
                else
                {
                    result.AssignedAA = new User();
                    result.AssignedAA.FirstName = "Not Assigned";
                    result.AssignedAA.LastName = "";
                }
                
                leads.Add(result);
            }

            ll.Allleads = leads;
            
            //to get the AA dropdown
            //By: Mutaaf 
            IList<User> users = new List<User>();
            var UsersResult = _UserRepository.GetAllUsers();

            foreach (var result in UsersResult)
            {
                if (result.AssignedRoleId == 4)
                {
                    users.Add(result);
                }
            }
            ll.AssignedAAList = users.Select(row => new SelectListItem()
            {
                Text = row.FirstName + " " + row.LastName,
                Value = row.UserId.ToString()
            });
            

            return View(ll);
        }
       
        private int GetNumberOfAccounts(int leadid)
        {
            var results = _service.GetAllAccountsForLead(leadid);
            int number = results.Count();
            return number;
        }
    }
}
