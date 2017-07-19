using Data.Repositories.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;

namespace Web.Service
{
    class LeadProfileService : Web.Service.Abstract.ILeadProfileService
    {
        private ILeadRepository _LeadRepository;
        private IUserRepository _UserRepository;
        private ICardRepository _CardRepository;
        private IAccountRepository _AccountRepository;
        private IAppointmentSheet _AppointmentSheetRepository;
        private IGenericUsageRepositoryInterface _IncodeRepository;

        public LeadProfileService(IGenericUsageRepositoryInterface IncodeRepos, ILeadRepository LeadRepository, IUserRepository UserRepository, ICardRepository CardRepository, IAppointmentSheet AppointmentSheetRepository, IAccountRepository AccountRepository)
        {
            _LeadRepository = LeadRepository;
            _UserRepository = UserRepository;
            _CardRepository = CardRepository;
            _AppointmentSheetRepository = AppointmentSheetRepository;
            _AccountRepository = AccountRepository;
            _IncodeRepository = IncodeRepos;
        }
        // This guy generate the cards for a lead
        public ViewModel.CardStackViewModel GenerateCardStackViewModel(int leadId)
        {
            ViewModel.CardStackViewModel viewmodel = new ViewModel.CardStackViewModel();
            var card = _CardRepository. GetCardByLeadId(leadId);
            if (card == null)
            {
                viewmodel.card = new Data.Domain.Card() { ParentLeadId = leadId };
            }
            else
            {
                //to be modified
                //viewmodel.card = card;

            }
            //var user = _UserRepository.GetUserById(viewmodel.card.AssignedAAId);
            //viewmodel.UserName = user.UserName;

            return viewmodel;
        }
        // this guy generates the accounts for a lead
        public ViewModel.AccountInformationViewModel GenerateAccountsViewModel(int leadId)
        {
            ViewModel.AccountInformationViewModel viewmodel = new ViewModel.AccountInformationViewModel();
            var account = _AccountRepository.GetAccountsByLeadId(leadId);
            if (account == null)
            {
                viewmodel.account = new Data.Domain.Account() { ParentLead = leadId };
            }
            else
            {
                //to be modified
                //viewmodel.account = account;

            }
           
            return viewmodel;
        }
        public Data.Domain.Lead GetLeadByLeadId(int leadId)
        {
            return _LeadRepository.LeadByLeadID(leadId);
        }
        public Web.ViewModel.BusinessInformationViewModel GetBusinessInformation(int leadId)
        {
            Web.ViewModel.BusinessInformationViewModel leadModel = new Web.ViewModel.BusinessInformationViewModel();
            //todo: need to handle if no results are returned
            var results = _LeadRepository.LeadByLeadID(leadId);
            leadModel.lead = results;
            //var aaUsersResult = _UserRepository.Users.Where(row => row.AssignedRole.Permissions.Contains(new Permission()) == true);
          //  var aaUsersResult = _UserRepository.GetAllUsersByPermission(Data.Constants.Permissions..LEAD_ASSIGNABLE);
            var aaUsersResult = _UserRepository.GetAllUsers();

            leadModel.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                //Text = row.LastName + "," + row.FirstName,
                Text = row.LastName,
                Value = row.UserId.ToString()
            });
            return leadModel;
        }

        public Web.ViewModel.AppointmentSheetViewModel GetAppointmentSheet(int leadId)
        {
            Web.ViewModel.AppointmentSheetViewModel appointmentModel = new Web.ViewModel.AppointmentSheetViewModel();
            var results = _AppointmentSheetRepository.AppointmentSheets.Single(row => row.ParentLeadId == leadId);
            appointmentModel.appointmentSheet = results;
            var aaUsersResult = _UserRepository.GetAllUsersByPermission(Data.Constants.Permissions.LEAD_ASSIGNABLE);
            appointmentModel.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + " , " + row.LastName,
                Value = row.UserId.ToString()
            });
            return appointmentModel;
        }

        public IEnumerable<Data.Domain.Lead> GetAllLeads()
        {
            return _LeadRepository.Leads;
        }
        public IEnumerable<Data.Domain.Lead> GetAllColdLeads(int userId)
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            //Take s out of Cold Lead(s) when you have the new list of leads.
            var results = _LeadRepository.LeadByStatus("Cold Lead", userId);

            return results;

        }

        public IEnumerable<Data.Domain.Lead> GetAllWarmLeads(int userId)
        {
            var results = _IncodeRepository.GetWarmLeads(userId);
            return results;
        }
        public IEnumerable<Data.Domain.Lead> GetAllFollowUpLeads(int sauserid)
        {
            IList<Data.Domain.Lead> leads = new List<Data.Domain.Lead>();
            int lid = 0;
            Data.Domain.Lead lead = new Data.Domain.Lead();
            //var results = _LeadRepository.Leads.Where(row => row.Status == "Warm Lead");
            var appointments = _AppointmentSheetRepository.AppointmentSheets.Where(row => row.AssignedSalesAgent == sauserid);

            foreach (var appointment in appointments)
            {
                if (lid != appointment.ParentLeadId)
                {
                    lid = appointment.ParentLeadId;

                    lead = _LeadRepository.LeadByLeadID(appointment.ParentLeadId);
                    lead.appointmentdesc = appointment.Comment;
                    leads.Add(lead);
                }
            }
            
            return leads;
        }

        public IEnumerable<Data.Domain.AppointmentSheet> GetAllAppointments()
        {
            var results = _AppointmentSheetRepository.AppointmentSheets.Where(row => row.DayOfAppointment == DateTime.Now);
            return results;
        }
          
        public IEnumerable<Data.Domain.Card> GetAllCardsForLead(int leadId)
        {
            var results = _CardRepository.GetCardByLeadId(leadId);
            return results;
        }
        public IEnumerable<Data.Domain.Account> GetAllAccountsForLead(int leadId)
        {
            var results = _AccountRepository.GetAccountsByLeadId(leadId);
            return results;
        }
        public IEnumerable<Data.Domain.AppointmentSheet> GetAllAppointmentsForLead(int leadId)
        {
            var results = _AppointmentSheetRepository.GetAppointmentByLeadId(leadId);
            return results;
        }
        public void SaveBusinessInformation(ViewModel.BusinessInformationViewModel bivm)
        {
            _LeadRepository.SaveLead(bivm.lead);
        }
        public void SaveAppointmentSheet(ViewModel.AppointmentSheetViewModel asvm)
        {
            _AppointmentSheetRepository.SaveAppointmentSheet(asvm.appointmentSheet);
        }
        public void SaveCard(ViewModel.CardStackViewModel csvm)
        {
            _CardRepository.SaveCard(csvm.card);
        }
        public string IgnoreLead(int leadId)
        {
            var IgnoredLead = _LeadRepository.LeadByLeadID(leadId);
            IgnoredLead.Ignored = true;
            IgnoredLead.Reassigned = false;
            IgnoredLead.IgnoredDate = DateTime.Now;
            _LeadRepository.SaveLead(IgnoredLead);

             return "Contact: " + IgnoredLead.CompanyName + " was ignored";
        }
    }
}