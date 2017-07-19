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
    public class AppointmentSheetViewModel 
    {
        public AppointmentSheet appointmentSheet { get; set; }
        //public IEnumerable<SelectListItem> UsersDropdown { get; set; }

        public IEnumerable<AppointmentSheet> appts { get; set; }
        public IEnumerable<Card> cards { get; set; }
        public User user { get; set; }
        public Lead lead { get; set; }
        
        public IEnumerable<SelectListItem> SAUsersDropdown { get; set; }
        public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }
        public IEnumerable<AppointmentSheet> AppointmentQueue { get; set; }
        public IEnumerable<string> states = Data.Constants.StateList.States;
        public IEnumerable<string> processors = Data.Constants.ForCurrentProcessor.Processors;
        public IEnumerable<string> locations = Data.Constants.Location.Locations;
        public IEnumerable<string> scores = Data.Constants.Score.Scores;
        public IEnumerable<string> volumes = Data.Constants.VolumeList.Volume;
    }
}
