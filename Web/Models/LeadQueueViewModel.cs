using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Domain;
using Data.Repositories;
using Data.Repositories.Static;
using System.Web.Mvc;
using Data;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class LeadQueueViewModel
    {
        public LeadQueueViewModel()
        {

        }
       
        public Lead lead { get; set; }
        public IEnumerable<string> statuslist = Data.Constants.StatusListLeadQueue.status;
        
        //All the different types of lead queues
        public IEnumerable<Lead> Allleads { get; set; }
        public IEnumerable<Lead> Wrongleads { get; set; }
        public IEnumerable<Lead> Notleadleads { get; set; }
        public IEnumerable<Lead> NotInterestedleads { get; set; }
        public IEnumerable<Lead> DNCleads { get; set; }
        public IEnumerable<Lead> Leftvmleads { get; set; }


        public IEnumerable<User> users { get; set; }
        public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }
        public User user { get; set; }
        public ICollection<int> Services { get; set; }

    }
}