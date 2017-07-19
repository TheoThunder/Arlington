
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Domain;
using Data.Repositories;
using Data.Repositories.Static;
using System.Web.Mvc;

namespace Web.ViewModel
{
    public class CalendarEventViewModel 
    {
        public CalendarEventViewModel()
        {

        }
        public CalendarEvent calendarEvent { get; set; }
        // To display the user information
        public User User { get; set; }
        public IEnumerable<string> states = Data.Constants.StateList.States;
    }
}
