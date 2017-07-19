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

namespace Web.Controllers
{
    public class LeadQueueController : Controller
    {

        public ILeadAccessRepository _LeadRepository;
        public ILeadRepository _LeadRepos;
        private IUserRepository _UserRepository;
        public IGenericUsageRepositoryInterface _GetLeadByCardType;
        public ICardRepository _CardRepository;
        public IAppointmentSheet _AppointmentRepository;
        public IAccountRepository _AccountRepository;

        public LeadQueueController(IAccountRepository Accounts, IAppointmentSheet Appointments, ILeadAccessRepository LeadRepository, ILeadRepository LRepository, IUserRepository UserRepository, IGenericUsageRepositoryInterface ToGetLeadByCardType, ICardRepository CardRepos)
        {
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepository;
            _LeadRepos = LRepository;
            _GetLeadByCardType = ToGetLeadByCardType;
            _CardRepository = CardRepos;
            _AppointmentRepository = Appointments;
            _AccountRepository = Accounts;
        }


        //
        // GET: /ImportLead/

        
        public ActionResult Index()
        {

            Web.ViewModel.LeadQueueViewModel lq = new Web.ViewModel.LeadQueueViewModel();

            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);

                       
            var wrongresults = GetWrongNumberLeads();
            foreach (var wrongresult in wrongresults)
            {
                var aaid = wrongresult.AssignedAAUserId;
                User newUser = _UserRepository.GetUserById(aaid);
                if (newUser == null)
                {

                    wrongresult.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    wrongresult.AssignedUser = newUser;
                }
            }
            lq.Wrongleads = wrongresults;
           

            var notLeadResults = GetNotLeadLeads();
            foreach (var notleadresult in notLeadResults)
            {
                var aaid = notleadresult.AssignedAAUserId;
                User newnotleadassignedUser = _UserRepository.GetUserById(aaid);
                if (newnotleadassignedUser == null)
                {

                    notleadresult.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    notleadresult.AssignedUser = newnotleadassignedUser;
                }
            }
            lq.Notleadleads = notLeadResults;

           
            var dncResults = GetDNCLeads();
            foreach (var dncresult in dncResults)
            {
                var aaid = dncresult.AssignedAAUserId;
                User newdncUser = _UserRepository.GetUserById(aaid);
                if (newdncUser == null)
                {
                    dncresult.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    dncresult.AssignedUser = newdncUser;
                }
            }
            lq.DNCleads = dncResults;
            
           
            var leftVMResults = GetLeftVMResults();
            foreach (var leftvmresult in leftVMResults)
            {
                var aaid = leftvmresult.AssignedAAUserId;
                User newleftvmassignedUser = _UserRepository.GetUserById(aaid);
                if (newleftvmassignedUser == null)
                {
                    leftvmresult.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    leftvmresult.AssignedUser = newleftvmassignedUser;
                }
            }
            lq.Leftvmleads = leftVMResults;

            var notInterestedResults = GetNoInterestLeads();
            foreach (var nointresult in notInterestedResults)
            {
                var aaid = nointresult.AssignedAAUserId;
                User newnointassignedUser = _UserRepository.GetUserById(aaid);
                if (newnointassignedUser == null)
                {
                    nointresult.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    nointresult.AssignedUser = newnointassignedUser;
                }
            }
            lq.NotInterestedleads = notInterestedResults;
            lq.user = user;
            var allleadsresults = lq.Wrongleads.Concat(lq.NotInterestedleads).Concat(lq.DNCleads).Concat(lq.Leftvmleads).Concat(lq.Notleadleads);

           
            
            
            lq.Allleads = allleadsresults;



            IList<User> dropdownUsers = new List<User>();
            var aaUsersResult = _UserRepository.GetAllUsers();

            foreach (var result in aaUsersResult)
            {
                if (result.AssignedRoleId == 3 || result.AssignedRoleId == 4)
                {
                    dropdownUsers.Add(result);
                }
                else
                {
                    //do nothing
                }
            }
       
            lq.AAUsersDropdown = dropdownUsers.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.UserId.ToString()
            });
            return View(lq);
        }


        [HttpPost]
        public ActionResult Index(string text)
        {
            Web.ViewModel.LeadQueueViewModel lq = new Web.ViewModel.LeadQueueViewModel();
            return Redirect("Index");

        }
        
        private IEnumerable<Data.Domain.Lead> GetNoInterestLeads()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultsnoint = _GetLeadByCardType.GetLeadByCardType("No Interest");
            foreach (var result in resultsnoint)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "No Interest") && (results.Last().Reassigned == false))
                {
                    var res = results.Last().CardType;
                    if (lead.CompanyName != result.CompanyName)
                    {
                        lead = result;
                        result.CallbackDate = results.Last().CreatedOn;
                        leads.Add(result);

                    }
                }
                else
                {
                    var elseres = results.Last().CardType;
                    //dont do anything
                }
            }
            return leads;
        }
      
        private IEnumerable<Data.Domain.Lead> GetLeftVMResults()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultsvm = _GetLeadByCardType.GetLeadByCardType("No Answer");
            foreach (var result in resultsvm)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "No Answer") && (results.Last().Reassigned == false))
                {
                    var res = results.Last().CardType;
                    if (lead.CompanyName != result.CompanyName)
                    {
                        lead = result;
                        result.CallbackDate = results.Last().CreatedOn;
                        leads.Add(result);

                    }
                }
                else
                {
                    var elseres = results.Last().CardType;
                    //dont do anything
                }
            }
            return leads;
            
        }

        private IEnumerable<Data.Domain.Lead> GetDNCLeads()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultsdnc = _GetLeadByCardType.GetLeadByCardType("DNC");
            foreach (var result in resultsdnc)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "DNC") && (results.Last().Reassigned == false))
                {
                    var res = results.Last().CardType;

                    if (lead.CompanyName != result.CompanyName)
                    {
                        lead = result;
                        result.CallbackDate = results.Last().CreatedOn;
                        leads.Add(result);
                        
                    }
                   
                }
                else
                {
                    var elseres = results.Last().CardType;
                    //dont do anything
                }
            }
            return leads;
        }

        private IEnumerable<Data.Domain.Lead> GetNotLeadLeads()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultsnotl = _GetLeadByCardType.GetLeadByCardType("Not Lead");
            foreach (var result in resultsnotl)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "Not Lead") && (results.Last().Reassigned == false))
                {
                    var res = results.Last().CardType;
                    if (lead.CompanyName != result.CompanyName)
                    {
                        lead = result;
                        result.CallbackDate = results.Last().CreatedOn;
                        leads.Add(result);

                    }
                   
                
                }
                else
                {
                    var elseres = results.Last().CardType;
                    //dont do anything
                }
            }
            return leads;
        }

        private IEnumerable<Data.Domain.Lead> GetWrongNumberLeads()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultswrong = _GetLeadByCardType.GetLeadByCardType("Wrong#");
            foreach (var result in resultswrong)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "Wrong#") && (results.Last().Reassigned == false))
                {
                    var res = results.Last().CardType;
                    if (lead.CompanyName != result.CompanyName)
                    {
                        lead = result;
                        result.CallbackDate = results.Last().CreatedOn;
                        leads.Add(result);

                    }
                }
                else
                {
                    var elseres = results.Last().CardType;
                   //dont do anything
                }
            }
            return leads;
        }

        public IEnumerable<Data.Domain.Lead> GetAllColdLeads()
        {
            var resultscl = _LeadRepos.Leads.Where(row => row.Ignored == false);
            return resultscl;
        }

        //
        // GET: /ImportLead/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ImportLead/Create
        public ActionResult Profile(int LeadId)
        {
            return View(GetLeadByLeadId(LeadId));
        }

       
        public Data.Domain.Lead GetLeadByLeadId(int leadId)
        {
            return _LeadRepos.LeadByLeadID(leadId);
        }

        public ActionResult Suppress(int LeadId)
        {
            //Get Lead, Cards for Lead, Appointments for Lead, Accounts for Lead
            var leadSuppress = GetLeadByLeadId(LeadId);
            var cardsforlead = _CardRepository.GetCardByLeadId(LeadId);
            var appointmentsforlead = _AppointmentRepository.GetAppointmentByLeadId(LeadId);
            var accountsforlead = _AccountRepository.GetAccountsByLeadId(LeadId);
            //Delete all of them
            foreach (var card in cardsforlead)
            {
                _CardRepository.DeleteCard(card);
            }
            foreach (var appointment in appointmentsforlead)
            {
                _AppointmentRepository.DeleteAppointmentSheet(appointment);
            }
            foreach (var account in accountsforlead)
            {
                _AccountRepository.DeleteAccounts(account);
            }

            //leadSuppress.Suppressed = true;
            //We are deleting the lead here after deleting all the cards, accounts and appointmentsheets for him
            DeleteLead(leadSuppress);
            return RedirectToAction("Index");
        }

        private void DeleteLead(Data.Domain.Lead leadSuppress)
        {
            _LeadRepos.DeleteLead(leadSuppress);
        }


        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ImportLead/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ImportLead/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ImportLead/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ImportLead/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ImportLead/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
