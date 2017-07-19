using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;

namespace Data.Repositories.Static
{
    public class StaticAppointmentSheetRepository : IAppointmentSheet
    {
        private static IList<AppointmentSheet> fakeAppointment = new List<AppointmentSheet>();
        private static int counter = 1;

        public IQueryable<Domain.AppointmentSheet> AppointmentSheets
        {
            get { return fakeAppointment.AsQueryable(); }
        }

        public void SaveAppointmentSheet(AppointmentSheet appointment)
        {
            // If it's a new appointment, just add it to the list
            if (appointment.AppointmentSheetId == 0)       
            {
                appointment.AppointmentSheetId = counter;
                counter += 1;
                fakeAppointment.Add(appointment);
            }
            else if (fakeAppointment.Count(row => row.AppointmentSheetId == appointment.AppointmentSheetId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteAppointmentSheet(appointment);
                fakeAppointment.Add(appointment);
            }
        }

        public IEnumerable<AppointmentSheet> GetAppointmentByLeadId(int leadId)
        {
            //Not implemented for static
            IList<Domain.AppointmentSheet> leadAppointments = new List<Domain.AppointmentSheet>();
            
            return leadAppointments;
        }

        public void DeleteAppointmentSheet(AppointmentSheet appointment)
        {
            var temp = fakeAppointment.ToList();
            temp.RemoveAll(row => row.AppointmentSheetId == appointment.AppointmentSheetId);
            //temp.RemoveAll(row => row.AppointmentSheetId == appointment.AppointmentSheetId && row.ParentLeadId == row.ParentLeadId);
            //temp.RemoveAll(row => row.ParentLeadId == appointment.ParentLeadId);
            fakeAppointment = temp;
        }

        public AppointmentSheet GetAppointmentByAppointmentId(int appointmentid)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            Domain.AppointmentSheet newAppointment = new Domain.AppointmentSheet();
            
            return newAppointment;
        }

        public void SaveAppointmentSheetFromCalendar(AppointmentSheet lead)
        {

        }


        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeAppointment.Clear();
        }
    }    
}
