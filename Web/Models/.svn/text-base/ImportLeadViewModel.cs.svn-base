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
    public class ImportLeadViewModel
    {
        public ImportLeadViewModel()
        {

        }
        [DisplayName("Import")]
        public Lead lead { get; set; }
        public AppointmentSheet appointment { get; set; }
        public User user { get; set; }
        public IEnumerable<string> statuslist = Data.Constants.StatusList.status;
        public IEnumerable<string> sourcelist = Data.Constants.SourceDropDown.source;
        public IEnumerable<Lead> Unassignedleads { get; set; }
        public IEnumerable<Lead> Ignoredleads { get; set; }
        public IEnumerable<User> users { get; set; }
        public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }
        
        public ICollection<int> Services { get; set; }

    }
}