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
    public class LeadListViewModel
    {
        public LeadListViewModel()
        {

        }

        public Lead lead { get; set; }
        public IEnumerable<Ticket> tickets { get; set; }
        public IEnumerable<Account> Acct { get; set; }

        //All the different types of lead queues
        public IEnumerable<Lead> Allleads { get; set; }
        public User user;
        public IEnumerable<string> leadstatuslist = Data.Constants.LeadStatusList.leadstatus;
        public IEnumerable<int> ZoneDropdown { get; set; }
        public IEnumerable<SelectListItem> AssignedAAList { get; set; }
    }
}