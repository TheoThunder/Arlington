using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IAppointmentSheet
    {
        IQueryable<AppointmentSheet> AppointmentSheets { get; }
        void SaveAppointmentSheet(AppointmentSheet lead);
        void SaveAppointmentSheetFromCalendar(AppointmentSheet lead);
        void DeleteAppointmentSheet(AppointmentSheet lead);
        IEnumerable<AppointmentSheet> GetAppointmentByLeadId(int leadId);
        AppointmentSheet GetAppointmentByAppointmentId(int appointmentid);
    }
}
