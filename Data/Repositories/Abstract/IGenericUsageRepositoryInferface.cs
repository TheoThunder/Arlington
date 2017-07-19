using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IGenericUsageRepositoryInterface
    {
        IList<Lead> Assignleads(int leadid);
        IList<AppointmentSheet> AssignAppointments(int appointmentid);
        void SaveAssignedAALeads(Lead lead, int userid);
        void SaveAssignedSALeads(Lead lead, int userid);
        User GetUserIDByName(string lastname, string firstname);
        IList<Lead> GetLeadByCardType(string cardtype);
        void SaveAssignedAppointments(AppointmentSheet lead, int userid);
       
        IList<AppointmentSheet> AppointmentQueue(int said, Boolean reschedule);

        IList<Lead> UnAssignedleads();
        IList<Lead> Ignoredleads();
        IList<Lead> GetWarmLeads(int userId);
        IList<User> GetAllSalesAndAppointmentAgents();
    }
}
