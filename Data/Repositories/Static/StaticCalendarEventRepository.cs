using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Data.Repositories.Static
{
    public class StaticCalendarEventRepository : ICalendarEventRepository
    {

        //fake hard-coded list of products
        public static IList<Domain.CalendarEvent> realEvents = new List<Domain.CalendarEvent>();
        public IQueryable<Domain.CalendarEvent> CalendarEvents
        {
            get
            {
                return realEvents.AsQueryable();
            }

        }

        public CalendarEvent GetEventByAppointmentID(string id)
        {
            CalendarEvent currentEvent = new CalendarEvent();
            
            return currentEvent;
        }
        public void SaveCalendarEvent(CalendarEvent calendarEvent)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CalendarEvent> GetEventsByZoneNumber(int zone)
        {
            IList<CalendarEvent> zoneEvents = new List<CalendarEvent>();
            
            return zoneEvents;
        }

        public IEnumerable<CalendarEvent> GetEventsByUserId(int userid)
        {
            IList<CalendarEvent> userEvents = new List<CalendarEvent>();

            return userEvents;
        }
        public void DeleteCalendarEvent(CalendarEvent calendarEvent)
        {
            throw new NotImplementedException();
        }
    }
}
