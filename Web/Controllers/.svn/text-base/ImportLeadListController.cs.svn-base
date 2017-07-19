using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Web.ViewModel;
using Data.Repositories.Static;
using Web.Service.Abstract;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Web.Controllers
{
    public class ImportLeadListController : Controller
    {
         public ILeadAccessRepository _LeadRepository;
        public ILeadRepository _LeadRepos;
        public IGenericUsageRepositoryInterface _UsageRepos;
        public ICardRepository _CardRepository;
        public IAppointmentSheet _AppointmentRepository;
        public IAccountRepository _AccountRepository;

        public ImportLeadListController(IAccountRepository Accounts, ILeadAccessRepository LeadRepository, ILeadRepository LRepository, IGenericUsageRepositoryInterface URepository, ICardRepository CRepository, IAppointmentSheet ARepository)
        {
            _LeadRepository = LeadRepository;
            _LeadRepos = LRepository;
            _UsageRepos = URepository;
            _CardRepository = CRepository;
            _AppointmentRepository = ARepository;
            _AccountRepository = Accounts;
        }
        //
        // GET: /ImportLeadList/

        public ActionResult Index(string text, string id)
        {
            if (text != "undefined")
            {
                Web.ViewModel.ImportLeadViewModel il = new Web.ViewModel.ImportLeadViewModel();

                string[] ids = id.Split(',');
                string[] names = text.Split(',');

                IList<Lead> leads;

                User user = _UsageRepos.GetUserIDByName(names[0], names[1]);

                foreach (string leadid in ids)
                {
                    int lid = int.Parse(leadid);
                    leads = _UsageRepos.Assignleads(lid);


                    foreach (var lead in leads)
                    {
                        //Get all cards for this Lead
                        var cards = _CardRepository.GetCardByLeadId(lead.LeadId);
                        //Change the Reassigned value to card to true so that it does not show up in the list.
                        if (cards.Count() != 0)
                        {
                            var card = cards.Last();
                            card.Reassigned = true;
                            lead.Reassigned = true;
                            //save the card so that when the list refreshes, you do not get it.
                            _CardRepository.SaveCard(card);
                        }
                        lead.Ignored = false;
                        //Added by Mutaaf
                        //lead.Reassigned = true;
                        //if (user.AssignedRoleId == 4)
                        //{
                            _UsageRepos.SaveAssignedAALeads(lead, user.UserId);
                        //}
                        //else if (user.AssignedRoleId == 3)
                        //{

                        //    _UsageRepos.SaveAssignedSALeads(lead, user.UserId);
                        //}

                        //else
                        //    _UsageRepos.SaveAssignedSALeads(lead, user.UserId);
                    }
                }
                
                return Content("/ImportLead/Index");
            }
            else
            {
                //string errorMessage = "Please make a selection";
                return Redirect("/Home/Error");
            }


            //return RedirectToAction("Index", "ImportLead");
           
        }

        public ActionResult Suppress(string id)
        {
            Web.ViewModel.ImportLeadViewModel il = new Web.ViewModel.ImportLeadViewModel();

            string[] ids = id.Split(',');
            // IList<Lead> leads;
            foreach (string leadid in ids)
            {
                int lid = int.Parse(leadid);
            
                var leadSuppress = GetLeadByLeadId(lid);
                var cardsforlead = _CardRepository.GetCardByLeadId(lid);
                var appointmentsforlead = _AppointmentRepository.GetAppointmentByLeadId(lid);
                var accountsforlead = _AccountRepository.GetAccountsByLeadId(lid);
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

               // leadSuppress.Suppressed = true;
                DeleteLead(leadSuppress);
               
            }

            return Redirect("/LeadQueue/Index");
        }

        
        public Data.Domain.Lead GetLeadByLeadId(int leadId)
        {
            return _LeadRepos.LeadByLeadID(leadId);
        }

        private void DeleteLead(Data.Domain.Lead leadSuppress)
        {
            _LeadRepos.DeleteLead(leadSuppress);
        }

        public ActionResult Appointment(string text, string id)
        {
           

            string[] ids = id.Split(',');
            string[] names = text.Split(',');

            AppointmentSheet appointment;

            User user = _UsageRepos.GetUserIDByName(names[0], names[1]);

            foreach (string appointmentid in ids)
            {
                int aid = int.Parse(appointmentid);
                appointment = _AppointmentRepository.GetAppointmentByAppointmentId(aid);

               _UsageRepos.SaveAssignedAppointments(appointment, user.UserId);
                
            }


            //return RedirectToAction("Index", "ImportLead");
            return Redirect("/AppointmentQueue/Index");
        }

        public ActionResult Reschedule(string text, string id)
        {
            
            string[] ids = id.Split(',');
            // IList<Lead> leads;
            foreach (string appointmentid in ids)
            {
                int aid = int.Parse(appointmentid);
                var appointment = _AppointmentRepository.GetAppointmentByAppointmentId(aid);
                appointment.Reschedule = true;
                 _AppointmentRepository.SaveAppointmentSheet(appointment);
               
            }

           return Redirect("/AppointmentQueue/Index");
        }

    }
}
