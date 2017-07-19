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
    public class AccountController : Controller
    {
        public IAccountRepository _AccountRepository;
        public IAppointmentSheet _AppointmentRepository;
        public IUserRepository _UserRepository;
        public ILeadRepository _LeadRepository;
        public ITicketRepository _TicketRepository;
        public IAAPSReportRepository _ReportRepository;
        public IEquipmentRepository _EquipmentRepository;
        public AccountController(IAppointmentSheet AppointmentRepository, IAAPSReportRepository ReportRepository, IAccountRepository AccountRepository, IUserRepository UserRepository, ILeadRepository LeadRepos, ITicketRepository TicketRepos, IEquipmentRepository EquipmentRepository)
        {
            _AccountRepository = AccountRepository;
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepos;
            _TicketRepository = TicketRepos;
            _ReportRepository = ReportRepository;
            _AppointmentRepository = AppointmentRepository;
            _EquipmentRepository = EquipmentRepository;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            AccountInformationViewModel avm = new AccountInformationViewModel();

            var username = HttpContext.User.Identity.Name;

            avm.user = _UserRepository.GetUserByUsername(username);

            var results = GetAllAccounts();
            foreach (var result in results)
            {
                var salesagentid = result.AssignedSalesRep;
                User newUser = _UserRepository.GetUserById(salesagentid);
                if (newUser == null)
                {

                    result.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    result.AssignedUser = newUser;
                }
            }

            

            //to get the SA dropdown
            IList<User> users = new List<User>();
            var UsersResult = _UserRepository.GetAllUsers();

            foreach (var result in UsersResult)
            {
                if (result.AssignedRoleId == 3)
                {
                    users.Add(result);
                }
            }
            avm.SAUsersDropdown = users.Select(row => new SelectListItem()
            {
                Text = row.FirstName + " " + row.LastName,
                Value = row.UserId.ToString()
            });
            avm.Account = results;
            return View(avm);
        }
        public IEnumerable<Data.Domain.Account> GetAllAccounts()
        {
            return _AccountRepository.Accounts;
        }
        public ActionResult AccountInformation(int AccountId)
        {
            //return View(GetAccountInformation(AccountId));
            AccountInformationViewModel accountModel = new AccountInformationViewModel();
            var results = _AccountRepository.GetAccountByAccountId(AccountId);
            accountModel.account = results;
            return View(accountModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AccountInformation(Web.ViewModel.AccountInformationViewModel avm)
        {
            //SaveAccountInformation(avm);
            return Content("Contact: " + avm.account.AccountName + " has been saved");
            //return RedirectToActionPermanent("Index");
        }
        public void SaveAccountInformation(ViewModel.AccountInformationViewModel avm)
        {
            _AccountRepository.SaveAccounts(avm.account);
        }
        public ActionResult Create(int leadid)
        {
            Web.ViewModel.AccountInformationViewModel acc = new Web.ViewModel.AccountInformationViewModel();
           
            var username = HttpContext.User.Identity.Name;

            acc.user = _UserRepository.GetUserByUsername(username);
            acc.leadId = leadid;
            Lead accountlead = _LeadRepository.LeadByLeadID(leadid);
            acc.lead = accountlead;
           // int userid = acc.lead.AssignedAAUserId;

            User assignedaauser = new User();
            assignedaauser = _UserRepository.GetUserById(accountlead.AssignedAAUserId);
            acc.assignedaa = assignedaauser;
            if (assignedaauser != null)
            {
                acc.AAName = assignedaauser.FirstName + " " + assignedaauser.LastName;
            }

            User assignedsauser = new User();
            assignedsauser = _UserRepository.GetUserById(accountlead.AssignedSAUserId);
            acc.assignedsa = assignedsauser;
            if (assignedsauser != null)
            {
                acc.SAName = assignedsauser.FirstName + " " + assignedsauser.LastName;
                acc.EID = assignedsauser.FirstName.Substring(0, 1) + assignedsauser.LastName.Substring(0, 1) + "1000" + assignedsauser.UserId.ToString();
            }

            var term = _EquipmentRepository.GetEquipmentByType("Terminal");
            var pinpad = _EquipmentRepository.GetEquipmentByType("Pinpad");
            var equip = _EquipmentRepository.GetEquipmentByType("Check");

            IList<string> terms = new List<string>();
            IList<string> pins = new List<string>();
            IList<string> equips = new List<string>();

            foreach (var t in term)
            {
                terms.Add(t.Name);
            }
            acc.terminals = acc.terminals.Concat(terms);

            foreach (var p in pinpad)
            {
                pins.Add(p.Name);
            }
            acc.pinpad = acc.pinpad.Concat(pins);

            foreach (var e in equip)
            {
                equips.Add(e.Name);
            }
            acc.equipment = acc.equipment.Concat(equips);
            // uc.AssignedZoneList = _ZoneRepository.Zones.DefaultIfEmpty();//
            return View(acc);
        }
        [HttpPost]
        public ActionResult Create(Web.ViewModel.AccountInformationViewModel vm)
        {
            Report newRecord = new Report();
            var newAccount = vm.account;
            vm.account.ParentLead = vm.leadId;
            vm.account.AssignedSalesRep = vm.assignedsa.UserId;
            vm.account.AACreator = vm.assignedaa.UserId;
            var appointmentforlead = _AppointmentRepository.GetAppointmentByLeadId(vm.leadId);
            foreach (var appointment in appointmentforlead)
            {
                if (appointment.Score == "Good")
                {
                    newRecord = _ReportRepository.CheckExistingRecord(appointment.AssignedSalesAgent);
                    newRecord.MonthlyAccounts++;
                    var accountsforlead = _AccountRepository.GetAccountsByLeadId(vm.leadId);
                    if (accountsforlead.Count() == 0)
                    {
                        newRecord.MonthlyCloses++;
                    }
                    _ReportRepository.SaveReports(newRecord);

                    break;
                }

            }
            _AccountRepository.SaveAccounts(newAccount);
            
            //TO assign status to the lead for which account was created.

            Lead lead = new Lead();
            lead = _LeadRepository.LeadByLeadID(vm.leadId);
            lead.Status = "Customer";
            _LeadRepository.SaveLead(lead);

            
           
           //return RedirectToActionPermanent("Index");
           return Json(new { redirectToUrl = Url.Action("Index") });
        }
        public ActionResult Edit(Int32 AccountId)
        {
            Web.ViewModel.AccountInformationViewModel avm = new AccountInformationViewModel();
            var username = HttpContext.User.Identity.Name;
            avm = GetAccountInformation(AccountId);
            avm.user = _UserRepository.GetUserByUsername(username);
            avm.tickets = _TicketRepository.GetTicketsByAccountID(AccountId);

            Lead accountlead = _LeadRepository.LeadByLeadID(avm.account.ParentLead);
            // int userid = acc.lead.AssignedAAUserId;

            User assignedaauser = new User();
            assignedaauser = _UserRepository.GetUserById(accountlead.AssignedAAUserId);
            avm.assignedaa = assignedaauser;
            if (assignedaauser != null)
            {
                avm.AAName = assignedaauser.FirstName + " " + assignedaauser.LastName;
            }

            User assignedsauser = new User();
            assignedsauser = _UserRepository.GetUserById(accountlead.AssignedSAUserId);
            avm.assignedsa = assignedsauser;
            if (assignedsauser != null)
            {
                avm.SAName = assignedsauser.FirstName + " " + assignedsauser.LastName;
                avm.EID = assignedsauser.FirstName.Substring(0, 1) + assignedsauser.LastName.Substring(0, 1) + "1000" + assignedsauser.UserId.ToString();
            }

            var term = _EquipmentRepository.GetEquipmentByType("Terminal");
            var pinpad = _EquipmentRepository.GetEquipmentByType("Pinpad");
            var equip = _EquipmentRepository.GetEquipmentByType("Check");

            IList<string> terms = new List<string>();
            IList<string> pins = new List<string>();
            IList<string> equips = new List<string>();

            foreach (var t in term)
            {
                terms.Add(t.Name);
            }
            avm.terminals = avm.terminals.Concat(terms);

            foreach (var p in pinpad)
            {
                pins.Add(p.Name);
            }
            avm.pinpad = avm.pinpad.Concat(pins);

            foreach (var e in equip)
            {
                equips.Add(e.Name);
            }
            avm.equipment = avm.equipment.Concat(equips);
            return View(avm);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Web.ViewModel.AccountInformationViewModel avm)
        {
            SaveAccountInformation(avm);
            //return Content("Account "+ avm.account.AccountName +" updated to leadID "+ avm.account.ParentLead);
            return RedirectToActionPermanent("Index");
 
        }
        public AccountInformationViewModel GetAccountInformation(int AccountID)
        {
            AccountInformationViewModel accountModel = new AccountInformationViewModel();
            var results = _AccountRepository.GetAccountByAccountId(AccountID);
            
                var salesagentid = results.AssignedSalesRep;
                User newUser = _UserRepository.GetUserById(salesagentid);
                if (newUser == null)
                {

                    results.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    results.AssignedUser = newUser;
                }
          
            accountModel.account = results;
            //var aaUsersResult = _UserRepository.Users.Where(row => row.AssignedRole.Permissions.Contains(new Permission()) == true);
            //var aaUsersResult = _UserRepository.GetAllUsersByPermission(Data.Constants.Permissions.LEAD_ASSIGNABLE);

            return accountModel;
        }
        public ActionResult Delete(int id)
        {
            //var result = _AccountRepository.GetUserById(id);
            //var result = _UserRepository.Users.ToList()[id - 1];
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id, User u)
        {
            _UserRepository.DeleteUser(id);
            return View("Deleted");
        }
    }
}
