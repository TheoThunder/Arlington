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
    public class BusinessInformationViewModel
    {
        public Lead lead { get; set; }
        public User displayuser { get; set; }
        public Zone zones { get; set; }
        public ZipCodes zipc { get; set; }
        public IEnumerable<string> states = Data.Constants.StateList.States;
        public IEnumerable<string> decisionMakers = Data.Constants.DecisionMakerList.DecisionMakers;
        public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }
        //public IEnumerable<string> CallTypes = Data.Constants.CallTypeList.CallTypes;
    }
}