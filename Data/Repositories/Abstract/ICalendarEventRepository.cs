using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ICalendarEventRepository
    {
        IQueryable<CalendarEvent> CalendarEvents { get; }
        void SaveCalendarEvent(CalendarEvent calendarEvent);
        void DeleteCalendarEvent(CalendarEvent calendarEvent);
        CalendarEvent GetEventByAppointmentID(string id);
        IEnumerable<CalendarEvent> GetEventsByZoneNumber(int zone);
        IEnumerable<CalendarEvent> GetEventsByUserId(int userid);
    }
}
