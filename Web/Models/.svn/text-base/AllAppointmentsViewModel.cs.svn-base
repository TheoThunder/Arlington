using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Domain;
using Data.Repositories;
using Data.Repositories.Abstract;
using System.Web.Mvc;

namespace Web.ViewModel
{
    public class AllAppointmentsViewModel 
    {
        //
        // GET: /AllAppointmentsViewModel/
        public User user { get; set; }
        public string UserName { get; set; }
        public User AssignedUser { get; set; }
        public IEnumerable<SelectListItem> SAUsersDropdown { get; set; }
        public IEnumerable<SelectList> UserNameDropdown { get; set; }

        public IEnumerable<AppointmentSheet> appointments { get; set; }
        public IEnumerable<Card> cards { get; set; }

        //public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }
        //public IEnumerable<AppointmentSheet> AppointmentQueue { get; set; }
        public IEnumerable<string> states = Data.Constants.StateList.States;
        public IEnumerable<string> processors = Data.Constants.ForCurrentProcessor.Processors;
        public IEnumerable<string> locations = Data.Constants.Location.Locations;
        public IEnumerable<string> scores = Data.Constants.Score.Scores;
        public IEnumerable<string> volumes = Data.Constants.VolumeList.Volume;


    }
}
