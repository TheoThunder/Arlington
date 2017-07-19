using Web.ViewModel;
using Data.Domain;
using System.Collections.Generic;
using System.Collections;

namespace Web.Service.Abstract
{
    public interface ILeadProfileService
    {
        CardStackViewModel GenerateCardStackViewModel(int leadid);

        AccountInformationViewModel GenerateAccountsViewModel(int leadid);

        Lead GetLeadByLeadId(int leadId);
        IEnumerable<Lead> GetAllLeads();

        void SaveBusinessInformation(BusinessInformationViewModel bivm);
        void SaveAppointmentSheet(AppointmentSheetViewModel asvm);
        void SaveCard(CardStackViewModel csvm);
        string IgnoreLead(int leadId);

        IEnumerable<Lead> GetAllColdLeads(int userId);
        IEnumerable<Lead> GetAllWarmLeads(int userId);
        IEnumerable<Card> GetAllCardsForLead(int leadId);
        IEnumerable<Account> GetAllAccountsForLead(int leadId);
        IEnumerable<AppointmentSheet> GetAllAppointmentsForLead(int leadId);

        Web.ViewModel.BusinessInformationViewModel GetBusinessInformation(int LeadId);
        Web.ViewModel.AppointmentSheetViewModel GetAppointmentSheet(int LeadId);

        //void SaveCard(BusinessInformationViewModel bivm);

        IEnumerable<Lead> GetAllFollowUpLeads(int userid);
        IEnumerable<AppointmentSheet> GetAllAppointments();
    }
}