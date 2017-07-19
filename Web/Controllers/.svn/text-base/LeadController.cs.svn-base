using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Data.Domain;
using Data.Repositories.Abstract;
using Data.Repositories.Postgres;
using Infrastructure;
using Infrastructure.Logging;
using Web.Service.Abstract;
using Web.ViewModel;

namespace Web.Controllers
{
   
    public class LeadController : Controller
    {
        ILeadRepository _leadRepos;
        ILeadProfileService _service;
        IAccountRepository _AccountRepository;
        IUserRepository _UserRepository;
        IGenericUsageRepositoryInterface _genericRepos;
 		IAppointmentSheet _AppointmentSheetRepository;
        ICardRepository _CardRepository;
        IAAPSReportRepository _ReportRepository;
        ICalendarEventRepository _EventsRepository;
        IPhoneUserRepository _PhoneUserRepos;
        ITimeSlotRepository _TimeSlotRepository;
        IThresholdRepository _ThresholdRepository;
        IAppointmentSheet _appointmentsRepository;

        ILogger _logger = new Log4NetLogger();

        public LeadController(IThresholdRepository Threshold, IAAPSReportRepository ReportRepostitory, ILeadProfileService service, IAccountRepository accountservice, IUserRepository UserRepository, 
            ILeadRepository leadRepos, IGenericUsageRepositoryInterface genericUsage, ICardRepository cardRepos, IAppointmentSheet appointmentSheetRepos,
            ICalendarEventRepository eventsRepository, IPhoneUserRepository PhoneUserRepos, ITimeSlotRepository timeSlotRepository, IAppointmentSheet appointmentsRepository)
        {
            _service = service;
            _AccountRepository = accountservice;
            _UserRepository = UserRepository;
            _leadRepos = leadRepos;
            _genericRepos = genericUsage;
            _CardRepository = cardRepos;
            _AppointmentSheetRepository = appointmentSheetRepos;
            _ReportRepository = ReportRepostitory;
            _EventsRepository = eventsRepository;
            _PhoneUserRepos = PhoneUserRepos;
            _TimeSlotRepository = timeSlotRepository;
            _ThresholdRepository = Threshold;
            _appointmentsRepository = appointmentsRepository;
        }



        public ActionResult Index()
        {
            var results = _service.GetAllLeads();
            return View(results);
        }
        public ActionResult Details(int leadId)
        {
            return View();
        }
        public ActionResult ColdLeads()
        {
            ColdLeadsViewModel clvm = new ColdLeadsViewModel();
            IList<Lead> leads = new List<Lead>();
            
           
            var username = HttpContext.User.Identity.Name;
            clvm.user = _UserRepository.GetUserByUsername(username);

			var results = _service.GetAllColdLeads(clvm.user.UserId);

            //if (clvm.user.AssignedRoleId == 4)
            //{
                foreach (var result in results)
                {
                    if (result.AssignedAAUserId == clvm.user.UserId)
                    {
                        leads.Add(result);
                    }
                    else
                    {
                        //do nothing
                    }
                }
                clvm.ColdLeads = leads;
            //}
            //else if (clvm.user.AssignedRoleId == 3)
            //{
            //    foreach (var result in results)
            //    {
            //        if (result.AssignedSAUserId == clvm.user.UserId)
            //        {
            //            leads.Add(result);
            //        }
            //        else
            //        {
            //            //do nothing
            //        }
            //    }
            //    clvm.ColdLeads = leads;
            //}
            return View(clvm);
        }

        public ActionResult WarmLeads()
        {
            WarmLeadsViewModel wlvm = new WarmLeadsViewModel();
            IList<Lead> leads = new List<Lead>();

            

            var username = HttpContext.User.Identity.Name;
            wlvm.user = _UserRepository.GetUserByUsername(username);

			var results = _genericRepos.GetWarmLeads(wlvm.user.UserId);

            //if (wlvm.user.AssignedRoleId == 4)
            //{
                foreach (var result in results)
                {
                    if (result.AssignedAAUserId == wlvm.user.UserId)
                    {
                        leads.Add(result);
                    }
                    else
                    {
                        //do nothing
                    }
                }
                //wlvm.WarmLeads = leads;


                var customers = _service.GetAllFollowUpLeads(wlvm.user.UserId);
                foreach (var c in customers)
                {
                    if (!leads.Any().Equals(c))
                    {
                        leads.Add(c);
                    }

                }

                wlvm.WarmLeads = leads;
            //}
            //else if (wlvm.user.AssignedRoleId == 3)
            //{
            //    foreach (var result in results)
            //    {
            //        if (result.AssignedSAUserId == wlvm.user.UserId)
            //        {
            //            leads.Add(result);
            //        }
            //        else
            //        {
            //            //do nothing
            //        }
            //    }
            //    wlvm.WarmLeads = leads;
            //}
            return View(wlvm);
        }

        public ActionResult CallBack()
        {
            CallBackViewModel cvm = new CallBackViewModel();
            IList<Lead> leads = new List<Lead>();
            IList<Lead> newleads = new List<Lead>();
           
            IList<Lead> reassignedLeads = new List<Lead>();
            IList<Lead> newreassignedLeads = new List<Lead>();

         
            //To display the username on top right
            var username = HttpContext.User.Identity.Name;
            cvm.user = _UserRepository.GetUserByUsername(username);


            //Get the callback results
            var callbackresults = GetCallBackLeads();
           
            //Get all other Leads who had a last card type as either DNC, Wrong# LeftVm, No Interest, Not Lead
            var wrongresult = GetWrongNumberLeads();
            var dncresults = GetDNCLeads();
            var leftvmresults = GetLeftVMResults();
            var nointresults = GetNoInterestLeads();
            var notleadresults = GetNotLeadLeads();

            /////////////////////////////////////////////////////////////////////
            foreach (var res in wrongresult)
            {
                reassignedLeads.Add(res);
            }
            foreach (var res in dncresults)
            {
                reassignedLeads.Add(res);
            }
            foreach (var res in leftvmresults)
            {
                reassignedLeads.Add(res);
            }
            foreach (var res in nointresults)
            {
                reassignedLeads.Add(res);
            }
            foreach (var res in notleadresults)
            {
                reassignedLeads.Add(res);
            }
            //////////////////////////////////////////////////////////////////////


            // From all these leads find ones which were assigned to this guy
            //if (cvm.user.AssignedRoleId == 4)
            //{
                foreach (var result in reassignedLeads)
                {
                    if (result.AssignedAAUserId == cvm.user.UserId)
                    {
                        result.CardType = "Reassigned";
                        newreassignedLeads.Add(result);
                    }
                    else
                    {
                        //do nothing
                    }
                }
                leads = newreassignedLeads;
                
                foreach (var result in callbackresults)
                {
                    if (result.AssignedAAUserId == cvm.user.UserId)
                    {
                        result.CardType = "Call Back";
                        leads.Add(result);
                    }
                    else
                    {
                        //do nothing
                    }
                }
               

            //}
            //else if (cvm.user.AssignedRoleId == 3)
            //{
            //    foreach (var result in reassignedLeads)
            //    {
            //        if (result.AssignedSAUserId == cvm.user.UserId)
            //        {
            //            result.CardType = "Reassigned";
            //            newreassignedLeads.Add(result);
            //        }
            //        else
            //        {
            //            //do nothing
            //        }
            //    }
            //    leads = newreassignedLeads;

            //    foreach (var result in callbackresults)
            //    {
            //        if (result.AssignedSAUserId == cvm.user.UserId)
            //        {
            //            result.CardType = "Call Back";
            //            leads.Add(result);
            //        }
            //        else
            //        {
            //            //do nothing
            //        }
            //    }
               
            //}

            cvm.Leads = leads;

            return View(cvm);

        }

        public ActionResult Reschedule()
        {
            CallBackViewModel cvm = new CallBackViewModel();
            IList<Lead> leads = new List<Lead>();
           
            //To display the username on top right
            var username = HttpContext.User.Identity.Name;
            cvm.user = _UserRepository.GetUserByUsername(username);


            //Get  reschedule results
            var rescheduleresults = GetRescheduleAppointments();


            // From all these leads find ones which were assigned to this guy
            //if (cvm.user.AssignedRoleId == 4)
            //{
     
                foreach (var res in rescheduleresults)
                {
                    if (res.CreatorId == cvm.user.UserId)
                    {
                        var lead = _leadRepos.LeadByLeadID(res.ParentLeadId);
                        lead.CardType = "Reschedule";
                        lead.CallbackDate = res.CreatedAt;
                        leads.Add(lead);
                    }
                    else
                    {
                        //do nothing
                    }
                }

            //}
            //else if (cvm.user.AssignedRoleId == 3)
            //{
               
            //    foreach (var res in rescheduleresults)
            //    {
            //        if (res.CreatorId == cvm.user.UserId)
            //        {
            //            var lead = _leadRepos.LeadByLeadID(res.ParentLeadId);
            //            lead.CardType = "Reschedule";
            //            lead.CallbackDate = res.CreatedAt;
            //            leads.Add(lead);
            //        }
            //        else
            //        {
            //            //do nothing
            //        }
            //    }
            //}

            cvm.Leads = leads;

            return View(cvm);

        }

        #region reschedule and call back methods
        public IEnumerable<AppointmentSheet> GetRescheduleAppointments()
        {

            IEnumerable<AppointmentSheet> results = _AppointmentSheetRepository.AppointmentSheets.Where(row => row.Reschedule == true);

            return results;
        }
        private IEnumerable<Data.Domain.Lead> GetCallBackLeads()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultscallback = _genericRepos.GetLeadByCardType("Call Back");
            foreach (var result in resultscallback)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "Call Back"))
                {
                    result.CallbackDate = results.Last().CreatedOn;
                    if (lead.CompanyName != result.CompanyName)
                    {
                        lead = result;
                        result.CallbackDate = results.Last().CallBackDate;
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
        private IEnumerable<Data.Domain.Lead> GetNoInterestLeads()
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            Lead lead = new Lead();
            var resultsnoint = _genericRepos.GetLeadByCardType("No Interest");
            foreach (var result in resultsnoint)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "No Interest") && (results.Last().Reassigned == true))
                {
                    result.CallbackDate = results.Last().CreatedOn;
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
            var resultsvm = _genericRepos.GetLeadByCardType("No Answer");
            foreach (var result in resultsvm)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "No Answer") && (results.Last().Reassigned == true))
                {
                    result.CallbackDate = results.Last().CreatedOn;
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
            var resultsdnc = _genericRepos.GetLeadByCardType("DNC");
            foreach (var result in resultsdnc)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "DNC") && (results.Last().Reassigned == true))
                {
                    result.CallbackDate = results.Last().CreatedOn;
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
            var resultsnotl = _genericRepos.GetLeadByCardType("Not Lead");
            foreach (var result in resultsnotl)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "Not Lead") && (results.Last().Reassigned == true))
                {
                    result.CallbackDate = results.Last().CreatedOn;
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
            var resultswrong = _genericRepos.GetLeadByCardType("Wrong#");
            foreach (var result in resultswrong)
            {
                var results = _CardRepository.GetCardByLeadId(result.LeadId);
                if ((results.Last().CardType == "Wrong#") && (results.Last().Reassigned == true))
                {
                    result.CallbackDate = results.Last().CreatedOn;
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

        #endregion
 		 public ActionResult ScheduledAppointment()
        {
            ScheduledAppointmentViewModel sapp = new ScheduledAppointmentViewModel();
            IList<Lead> leads = new List<Lead>();
            Lead newlead = new Lead();

            //To display the username on top right
            var username = HttpContext.User.Identity.Name;
            sapp.user = _UserRepository.GetUserByUsername(username);
            Lead lead = new Lead();

            var results = _AppointmentSheetRepository.AppointmentSheets.Where(row => row.AssignedSalesAgent == sapp.user.UserId);
            

            foreach (var result in results)
            {
                if (result.Score == "Good")
                {
                    //do nothing
                }
                else if (result.Score == "Bad")
                {
                    //do nothing
                }
                else
                {
                    newlead = _leadRepos.LeadByLeadID(result.ParentLeadId);
                    newlead.appointmentdate = result.DayOfAppointment;
                    newlead.AptDateFrom = result.AppointmentDateFrom;
                    newlead.AptDateTo = result.AppointmentDateTo;
                    newlead.volume = result.Volume;
                    IEnumerable<Account> accounts = _AccountRepository.GetAccountsByLeadId(newlead.LeadId);
                    if (accounts.Count() != 0)
                    {
                        var account = accounts.Last();
                       
                    }
                    lead = newlead;
                    if (!leads.Any().Equals(lead))
                    {
                        leads.Add(lead);
                    }
                    else
                    {
                        //should not add
                    }
                }
               
            }
            sapp.Leads = leads;
            return View(sapp);

        }
		//public ActionResult ColdLeadsdev()
		//{
		//    var result = _service.GetAllColdLeads();
		//    return View(result);

		//}
        public ActionResult FollowUp()
        {
            FollowUpViewModel fuvm = new FollowUpViewModel();
            IList<Lead> leads = new List<Lead>();

            var username = HttpContext.User.Identity.Name;
            fuvm.user = _UserRepository.GetUserByUsername(username);
            var results = _service.GetAllFollowUpLeads(fuvm.user.UserId);
            foreach(var result in results)
            {
                if (!leads.Any().Equals(result))
                {
                    leads.Add(result);
                }
                
            }
            fuvm.FollowUP = leads;
            return View(fuvm);
        }
        public ActionResult AppointmentQueue()
        {
            Web.ViewModel.AppointmentSheetViewModel avm = new ViewModel.AppointmentSheetViewModel();

            var results = _service.GetAllAppointments();
            avm.appts = results;
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);
            avm.user = user;
            return View(avm);
        }
        //public ActionResult Edit(int AccountID)
        //{
        //    return RedirectToAction("Edit", "Account", AccountID);
        //}
        public ActionResult Ignore(int leadId)
        {
            var message = _service.IgnoreLead(leadId);
            TempData["message"] = message;
            return RedirectToAction ("ColdLeads","Lead");
        }
        //public ActionResult Call(int LeadId) {TempData["message"] = "Click to Dial Not Yet Implemented";
        //    //return RedirectToAction("ColdLeads","Lead");}
        public ActionResult Profile(int LeadId)
        {
            ProfileViewModel pvm = new ProfileViewModel();
            var username = HttpContext.User.Identity.Name;
            pvm.user = _UserRepository.GetUserByUsername(username);
            // Please remember to include this part of the code whenever Click to Dial is implemented.
            Lead lead = new Lead();
            lead = _service.GetLeadByLeadId(LeadId);
            pvm.lead = lead;

            pvm.accounts = GetNumberOfAccounts(lead.LeadId);

            // To get the number of appointments
          
            return View(pvm);

        }

        public ActionResult CallProfile(int LeadId)
        {
            ProfileViewModel pvm = new ProfileViewModel();
            Report newRecord = new Report();

            _logger.Debug("Trying to make a call.");
            var username = HttpContext.User.Identity.Name;
            pvm.user = _UserRepository.GetUserByUsername(username);

            newRecord = _ReportRepository.CheckExistingRecord(pvm.user.UserId);
            newRecord.MonthlyAppointments = newRecord.MonthlyAppointments + 1;
            newRecord.AssignedAAUserID = pvm.user.UserId;
            _ReportRepository.SaveReports(newRecord);

            // Please remember to include this part of the code whenever Click to Dial is implemented.
            Lead lead = new Lead();
            lead = _service.GetLeadByLeadId(LeadId);
            pvm.lead = lead;
            pvm.accounts = GetNumberOfAccounts(lead.LeadId);

            // Call to Extend API for calling the customer

            var phoneuser = _PhoneUserRepos.GetPhoneUser(pvm.user.UserId);
            //string user = phoneuser.UserName;
            //string password = phoneuser.Password;
            var num = lead.PrimaryPhoneNumber;
            if (num.Contains("("))
            {
                num = num.Remove(0, 1);
                num = num.Remove(3, 1);
                num = num.Remove(7, 1);
                num = num.Remove(3, 1);
            }
            string user = "admin";
            string password = "Pbx2011";
            //string snum = num;
            //num = "1" + num;
            long phoneNumberDialing = Convert.ToInt64(num);

            //if (lead.PrimaryPhoneChecked == true)
            //{
            //    phoneNumberDialing = int.Parse(lead.PrimaryPhoneNumber);
            //}
            //else
            //{
            //    phoneNumberDialing = int.Parse(lead.AddtionalPhoneNumber);
            //}
            string xml = "<request method= \"switchvox.users.call\"> <parameters> <account_id>" + /*phoneuser.AccountId*/ 1155 + "</account_id><dial_first>" + phoneuser.Extension + "</dial_first> <dial_second>" + phoneNumberDialing + "</dial_second> <variables> <variable>balance=300</variable> </variables> </parameters> </request> ";
            //string xml = "<request method= \"switchvox.users.call\"> <parameters> <account_id>1106</account_id><dial_first>326</dial_first> <dial_second>326</dial_second> <variables> <variable>balance=300</variable> </variables> </parameters> </request> ";
            //string xml = "<request method=\"switchvox.extensions.getInfo\"> <parameters> <extensions> <extension>327</extension> <extension>326</extension> </extensions> </parameters></request> ";
            string url = ConfigReader.VoipIP; 

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //string s = "id="+Server.UrlEncode(xml);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);
            req.Method = "POST";
            req.ContentType = "text/xml;charset=utf-8";
            req.ContentLength = requestBytes.Length;
            req.Credentials = new NetworkCredential(user, password);
            ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();

            sr.Close();
            res.Close();


            //Make sure that Customer is not converted to Warm Lead here
            if (lead.Status != "Customer")
            {
                lead.Status = "Warm Lead";
            }
            _leadRepos.SaveLead(lead);
            return View(pvm);
        }

        public ActionResult ChangeStatus(Web.ViewModel.CardStackViewModel csvm)
        {
            Lead lead = _service.GetLeadByLeadId(csvm.card.ParentLeadId);
            if (lead.Status != "Customer")
            {
                lead.Status = "Warm Lead";
            }
            _leadRepos.SaveLead(lead);

            var leadid = csvm.card.ParentLeadId.ToString();
            return Content(leadid);

            //return RedirectToAction("Profile", "Lead", new { leadid });
        }


        //To get the number of accounts for that lead.
        private int GetNumberOfAccounts(int leadid)
        {
            var results = _service.GetAllAccountsForLead(leadid);
            int number = results.Count();
            return number;
        }
        public ActionResult ViewProfile(int ParentLeadId)
        {
            int LeadId = ParentLeadId; 
           // return View(_service.GetLeadByLeadId(ParentLeadId));
            return RedirectToAction("Profile", "Lead", new { LeadId });
        }
        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult Upload2()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]   
        public ActionResult BusinessInformation(int LeadId)
        {
            Web.ViewModel.BusinessInformationViewModel bivm = new BusinessInformationViewModel();
            bivm = _service.GetBusinessInformation(LeadId);
            User assignedUser = new User();
            if (bivm.lead.AssignedAAUserId != 0)
            {
                assignedUser = _UserRepository.GetUserById(bivm.lead.AssignedAAUserId);
            }
            else
            {
                assignedUser = _UserRepository.GetUserById(bivm.lead.AssignedSAUserId);
            }
            bivm.lead.AssignedUser = assignedUser;
            var username = HttpContext.User.Identity.Name;
            bivm.displayuser  = _UserRepository.GetUserByUsername(username);
            //bivm.lead.AssignedUserRoleId = assignedUser.AssignedRoleId;

            return View(bivm);
        }
        [AcceptVerbs(HttpVerbs.Post)]   
        public ActionResult BusinessInformation(Web.ViewModel.BusinessInformationViewModel bivm)
        {
            try
            {
                _service.SaveBusinessInformation(bivm);
                return Content("This Contact: " + bivm.lead.Contact1FirstName + " " + bivm.lead.Contact1LastName + " has been saved");
            }
            catch
            {
                return Content("This Lead was removed from the system. Please contact your Trinity Manager for details");
            }
        }
        public ActionResult CreateCard(int LeadId)
        {
            Web.ViewModel.CardStackViewModel singleCard = new ViewModel.CardStackViewModel();
           
            //To get the current user whos is creating this card.
            var username = HttpContext.User.Identity.Name;
            singleCard.user = _UserRepository.GetUserByUsername(username);
            singleCard.card = new Card() { ParentLeadId = LeadId };
            singleCard.card.CallBackDate = DateTime.Now.Date;
            singleCard.card.CreatedOn = DateTime.Now;
            singleCard.card.LastUpdated = DateTime.Now;
            return View(singleCard);
        }

        public ActionResult EditCard(int leadId)
        {
            Web.ViewModel.CardStackViewModel singleCard = new ViewModel.CardStackViewModel();
            Web.ViewModel.AllCardsViewModel multipleCards = new AllCardsViewModel();
            var results = _service.GetAllCardsForLead(leadId);

            multipleCards.cards = results;

            var username = HttpContext.User.Identity.Name;
            singleCard.user = _UserRepository.GetUserByUsername(username);
            singleCard.card = multipleCards.cards.Last();
            singleCard.card = new Card() { ParentLeadId = leadId };
            singleCard.card.LastUpdated = DateTime.Now;

            singleCard.card = multipleCards.cards.Last();

            return View(singleCard);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCard(Web.ViewModel.CardStackViewModel csvm)
        {
            try
            {
               
                csvm.card.CreatorId = csvm.user.UserId;
                
                csvm.card.CallBackDate = Convert.ToDateTime(csvm.card.CallBackDate);
                if (csvm.card.CreatorName == null)
                {
                    csvm.card.CreatorName = csvm.user.UserName;
                }
                csvm.card.LastUpdated = DateTime.Now;
                //When creator creates a card, the boolean value is set to false. It can be set to true only by manager/admin when they reassign the lead to someone else
                csvm.card.Reassigned = false;

                _service.SaveCard(csvm);
                var id = csvm.card.ParentLeadId;
                

                return Content("card with ID: " + csvm.card.CardId + " is saved to lead " + csvm.card.ParentLeadId);
            }
            catch
            {
                return Content("This Lead was removed from the system. Please contact your Trinity Manager for details");
            }
        }
        public ActionResult CreateAppointment(int LeadId)
        {
            Web.ViewModel.AppointmentSheetViewModel singleAppointment = new ViewModel.AppointmentSheetViewModel();
            var saUsersResult = _UserRepository.GetAllUsersByRole(3);
            //To get the user who calls this appointment
            var username = HttpContext.User.Identity.Name;
            singleAppointment.user = _UserRepository.GetUserByUsername(username);
            singleAppointment.appointmentSheet = new AppointmentSheet() { ParentLeadId = LeadId };
            singleAppointment.SAUsersDropdown = saUsersResult.Select(row => new SelectListItem()
                {
                    Text = row.LastName,
                    Value = row.UserId.ToString()
                });


            return View(singleAppointment);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateAppointment(Web.ViewModel.AppointmentSheetViewModel asvm)
        {
             
           try
           {
            #region record
           
                Report newRecord = new Report();
                Report saveRecord = new Report();
                User creator = new User();
                var username = HttpContext.User.Identity.Name;
                creator = _UserRepository.GetUserByUsername(username);
                Web.ViewModel.BusinessInformationViewModel bivm = new BusinessInformationViewModel();
                bivm = _service.GetBusinessInformation(asvm.appointmentSheet.ParentLeadId);
                int leadid = asvm.appointmentSheet.ParentLeadId;
               // get the previous appointsheet date and time values 

                var appts = _appointmentsRepository.GetAppointmentByLeadId(leadid);
                var t = appts.Last();

                string tempDayofApt1 = t.DayOfAppointment.ToString();
                tempDayofApt1 = tempDayofApt1.Remove(9);
                string starttemp1 = t.AppointmentDateFrom.ToString();
                starttemp1 = starttemp1.Remove(0, 9);
                starttemp1 = tempDayofApt1 + " " + starttemp1;
                string endtemp1 = t.AppointmentDateTo.ToString();
                endtemp1 = endtemp1.Remove(0, 9);
                endtemp1 = tempDayofApt1 + " " + endtemp1;
                DateTime tempstart1 = DateTime.Parse(starttemp1);
                string z9 = tempstart1.ToString("yyyy-MM-ddTHH':'mm':'ss");
                DateTime tempend1 = DateTime.Parse(endtemp1);
                string z10 = tempend1.ToString("yyyy-MM-ddTHH':'mm':'ss");





                string EventReferencingId = asvm.appointmentSheet.Event_Reference;
                if (asvm.appointmentSheet.CreatorId == 0)
                {
                    asvm.appointmentSheet.CreatorId = creator.UserId;
                    asvm.appointmentSheet.CreatedAt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    // assigning userSales Agent to userid
                    asvm.appointmentSheet.AssignedUser.UserId =  asvm.appointmentSheet.AssignedSalesAgent;
                    string tempid = asvm.appointmentSheet.ParentLeadId.ToString();
                    IEnumerable<AppointmentSheet> appointmentsCount = new List<AppointmentSheet>();
                    appointmentsCount = _AppointmentSheetRepository.GetAppointmentByLeadId(asvm.appointmentSheet.ParentLeadId);
                    int tempcount = appointmentsCount.Count();
                    EventReferencingId = tempid + (tempcount).ToString();
                }
                // To check if a SA is logged in to score the appointment

                asvm.appointmentSheet.LastUpdated = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                if (asvm.appointmentSheet.CreatorId != 0)
                {
                        asvm.appointmentSheet.AssignedUser.UserId = asvm.appointmentSheet.AssignedSalesAgent;
                }
                asvm.appointmentSheet.Event_Reference = EventReferencingId;
                _service.SaveAppointmentSheet(asvm);
            #endregion 
               
                #region createEventToo
                CalendarEventViewModel cevm = new CalendarEventViewModel();
                CalendarEvent events = new CalendarEvent(); 

                events = _EventsRepository.GetEventByAppointmentID(EventReferencingId);

                Lead tempLead = new Lead();// This lead is created so we can have the company name
                string tempDayofApt = asvm.appointmentSheet.DayOfAppointment.ToString();
                tempDayofApt = tempDayofApt.Remove(9);
                string starttemp = asvm.appointmentSheet.AppointmentDateFrom.ToString();
                starttemp = starttemp.Remove(0, 9);
                starttemp = tempDayofApt + " " + starttemp;
                string endtemp = asvm.appointmentSheet.AppointmentDateTo.ToString();
                endtemp = endtemp.Remove(0, 9);
                endtemp = tempDayofApt + " " + endtemp; 
                DateTime tempstart = DateTime.Parse(starttemp); //parse causing it to skip to catch, could be due to 12h time
                events.start = tempstart.ToString("yyyy-MM-ddTHH':'mm':'ss'.'fff'-'05':'00");
                DateTime tempend = DateTime.Parse(endtemp);
                events.end = tempend.ToString("yyyy-MM-ddTHH':'mm':'ss'.'fff'-'05':'00");
               // int tempLeadId = asvm.appointmentSheet.ParentLeadId;
                //tempLead = _service.GetLeadByLeadId(tempLeadId);
                events.title = tempLead.CompanyName;
                events.street = asvm.appointmentSheet.Street;
                events.city = asvm.appointmentSheet.City;
                events.state = asvm.appointmentSheet.State;
                events.zipcode = asvm.appointmentSheet.ZipCode;
                events.creator = asvm.appointmentSheet.CreatorId;
                events.Parent_Appointment_Id = asvm.appointmentSheet.AppointmentSheetId;
                events.Appointment_Reference = EventReferencingId;
                if (asvm.appointmentSheet.AssignedSalesAgent != 0)
                {
                    events.Parent_User_Id = asvm.appointmentSheet.AssignedSalesAgent;
                }
                //This the event type set to "appointment"
                events.appointment = true;
                // The description by default empty so the SA can edit it at will later
                events.description = "";
                events.zone = bivm.lead.ZoneNumber;
                cevm.calendarEvent = events;
                _EventsRepository.SaveCalendarEvent(cevm.calendarEvent);
                #endregion 

                #region updateTimeslotToo


                var timeslots = _TimeSlotRepository.TimeSlots;   // get the timeslots 
               
                int lasttimeslotid = 0;
                int lastavailableSA = 0;
                Boolean update = false;


               // string  z9 = (string)Session["starttime"];
               // string z10 = (string)Session["endtime"];
               //...////
               
                Threshold threshold = new Threshold();
                threshold = _ThresholdRepository.Thresholds.First();

                string updateStartTime = events.start.Remove(19);
                string updateEndTime = events.end.Remove(19);

                if (string.Compare(updateStartTime, z9) != 0) // if the previous ts and current are not the same
                {
                    foreach (var timeslot in timeslots)
                    {
                        if (string.Compare(timeslot.StartTime, z9) == 0)   // find out the previous timeslot
                        {
                            lasttimeslotid = timeslot.TimeSlotId;
                            lastavailableSA = timeslot.Num_Available_SA;
                            update = true;               // note down the timeslot id and available SA , set the updt flg
                        }
                    }
                }


                TimeSlot updateTimeSlot = new TimeSlot();
                updateTimeSlot = _TimeSlotRepository.TimeSlots.SingleOrDefault(row => row.StartTime == updateStartTime );
                if (updateTimeSlot == null)
                {
                    return Content("Time slot is not available ,please refresh the page and reschedule the appointment");
                }
                else
                {
                    if (updateTimeSlot.Num_Available_SA != 0)
                    {
                        if (string.Compare(z9, updateTimeSlot.StartTime) == 0) // the same time is clicked again
                        {
                            updateTimeSlot.Num_Available_SA = updateTimeSlot.Num_Available_SA ;
                        }
                        else // A different timedate is chosen for the appointment, the sa is reduced by 1
                        {
                            updateTimeSlot.Num_Available_SA = updateTimeSlot.Num_Available_SA - 1;
                        }
                        updateTimeSlot.Title = updateTimeSlot.Num_Available_SA;
                    }
                    //Now update the color
                    if (updateTimeSlot.Num_Available_SA > threshold.Upper_Calendar)
                    {
                        updateTimeSlot.Color = "green";
                    }
                    if (updateTimeSlot.Num_Available_SA >= threshold.Lower_Calendar && updateTimeSlot.Num_Available_SA <= threshold.Upper_Calendar)
                    {
                        updateTimeSlot.Color = "yellow";
                    }
                    if (updateTimeSlot.Num_Available_SA == 0)
                    {
                        updateTimeSlot.Color = "red";
                    }
                    updateTimeSlot.All_Day = false;
                    _TimeSlotRepository.SaveTimeSlot(updateTimeSlot);

                   // If this is change in timedate for the appointment sheet , then ,
                   //  change the available sa for the previous TS .

                    if (update)
                    {
                        TimeSlot updateTimeSlotprev = new TimeSlot();

                        updateTimeSlotprev.TimeSlotId = lasttimeslotid;
                        updateTimeSlotprev.Num_Available_SA = lastavailableSA + 1;
                        updateTimeSlotprev.StartTime = z9;
                        updateTimeSlotprev.EndTime = z10;
                        updateTimeSlotprev.Title = updateTimeSlotprev.Num_Available_SA;
                        updateTimeSlotprev.All_Day = false;

                        if (updateTimeSlotprev.Num_Available_SA > threshold.Upper_Calendar)
                        {
                            updateTimeSlotprev.Color = "green";
                        }
                        else if (updateTimeSlotprev.Num_Available_SA >= threshold.Lower_Calendar && updateTimeSlotprev.Num_Available_SA <= threshold.Upper_Calendar)
                        {
                            updateTimeSlotprev.Color = "yellow";
                        }
                        else
                        {
                            updateTimeSlotprev.Color = "red";
                        }

                        _TimeSlotRepository.SaveTimeSlot(updateTimeSlotprev);
                    }

                }


                #endregion  
                return Content("Appointment in City " + asvm.appointmentSheet.City + " has been saved");
            }
            catch
            {
                return Content("This Lead was removed from the system. Please contact your Trinity Manager for details");
            }
        }
        //You should never enter this.   For development purposes only
        public ActionResult AppointmentSheet(int LeadId)
        {
            Web.ViewModel.AppointmentSheetViewModel asvm = new ViewModel.AppointmentSheetViewModel();
            //This following lines are for testing purposes and need to be deleted
            string today = DateTime.Now.ToShortTimeString();
            DateTime tempDate = Convert.ToDateTime(today);   
            asvm.appointmentSheet = new AppointmentSheet() {DayOfAppointment = new DateTime(2011,11,09), 
                                                             //AppointmentDateFrom = new DateTime(2011, 10, 10),
                                                             AppointmentDateTo = new DateTime(2011, 11, 10),
                                                             LastUpdated = tempDate };
            return View(asvm);
        }

        public ActionResult AllCardsForLead(int leadId)
         {
            Web.ViewModel.AllCardsViewModel multipleCards = new AllCardsViewModel();
            var results = _service.GetAllCardsForLead(leadId);
            
            multipleCards.cards = results;
            
            return View(multipleCards);  
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AllCardsForLead(Web.ViewModel.AllCardsViewModel allcardsvm)
        {
            return Content("Card Stack Saved");  
        }
        public ActionResult AllAppointmentsForLead(int leadId)
        {
            Web.ViewModel.AllAppointmentsViewModel multipleAppointments = new AllAppointmentsViewModel();
            User assigneduser = new User();
            User appointmentagent = new User();
            var saUsersResult = _UserRepository.GetAllUsersByRole(3);            
            var results = _service.GetAllAppointmentsForLead(leadId);
        
                multipleAppointments.appointments = results;
                var username = HttpContext.User.Identity.Name;
                multipleAppointments.user = _UserRepository.GetUserByUsername(username);
                foreach (var appointment in multipleAppointments.appointments)
                {
                    assigneduser = _UserRepository.GetUserById(appointment.AssignedSalesAgent);
                    appointment.AssignedUser = assigneduser;
                    appointmentagent = _UserRepository.GetUserById(appointment.CreatorId);
                    if(appointmentagent !=null)
                    {
                        appointment.CreatorName = appointmentagent.UserName;
                    }
                }
            // This will get all the users with role Id = 3 , the SAs
                multipleAppointments.UserNameDropdown = saUsersResult.Select(row => new SelectList(_UserRepository.GetAllUsersByRole(3)));
                multipleAppointments.SAUsersDropdown = saUsersResult.Select(row => new SelectListItem()
                {
                    Text = row.LastName,
                    Value = row.UserId.ToString(),


                });
                
                return View(multipleAppointments);
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AllAppointmentsForLead(Web.ViewModel.AllCardsViewModel allappointmentsvm)
        {
            return Content("Appointment Stack Saved");
        }

        public ActionResult AllAccountsForLead(int leadId)
        {
            Web.ViewModel.AccountInformationViewModel AccountVM = new AccountInformationViewModel();
            AccountVM.leadId = leadId;
            var results = _service.GetAllAccountsForLead(leadId);
            AccountVM.accounts = results;

            var username = HttpContext.User.Identity.Name;
            AccountVM.user = _UserRepository.GetUserByUsername(username);

            // To check if any of the appointments in scored for displaying Create Account in AllAccountsforLead
            var scored = false;
            var appointmentforlead = _AppointmentSheetRepository.GetAppointmentByLeadId(leadId);
            foreach (var appointment in appointmentforlead)
            {
                if (appointment.Score == "Good" || appointment.Score == "Bad")
                {
                    scored = true;
               
                    break;
                }
               
            }
            AccountVM.Scored = scored;

            return View(AccountVM);
        }

        public ActionResult Create(int accountId)
        {

            return null;
        }

        // For creating a new profile from the scratch.
        public ActionResult CreateProfile()
        {
            Web.ViewModel.BusinessInformationViewModel bivm = new BusinessInformationViewModel();
            var username = HttpContext.User.Identity.Name;
            bivm.displayuser = _UserRepository.GetUserByUsername(username);


            return View(bivm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateProfile(Web.ViewModel.BusinessInformationViewModel bivm)
        {
            try
            {
                bivm.lead.Status = "Cold Lead";
                bivm.lead.Ignored = false;
                bivm.lead.Reassigned = false;
                bivm.lead.DateTimeImported = DateTime.Now;

                _service.SaveBusinessInformation(bivm);
                //return Content("This Contact: " + bivm.lead.Contact1FirstName + " " + bivm.lead.Contact1LastName + " has been saved");
                return RedirectToActionPermanent("Index", "LeadList");
            }
            catch
            {
                return Content("This Lead was removed from the system. Please contact your Trinity Manager for details");
            }
            
        }
        
        public ActionResult GetStatementFiles(int leadId)
        {
            var fileRepo = new PGUploadedfileRepository();
            List<UploadedFile> files = fileRepo.GetFileByLeadId(leadId);
            return PartialView("_FileList", files);
        }
    }
}
