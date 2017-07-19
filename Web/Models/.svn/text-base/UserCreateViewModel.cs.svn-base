using System.ComponentModel;
using Data.Domain;
using System.Collections.Generic;
using System;

namespace Web.ViewModel
{
    public class UserCreateViewModel
    {
        [DisplayName("User")]
        public User user { get; set; }
        public User displayuser { get; set; }
        public UserZone userzone { get; set; }
        public IEnumerable<User> users { get; set; }
        public IEnumerable<string> states = Data.Constants.StateList.States;
        public IEnumerable<string> types = Data.Constants.UserType.type;
        public IEnumerable<int> team = Data.Constants.TeamNumbers.teams;
        public IEnumerable<string> status = Data.Constants.UserStatus.user_status;
        public IEnumerable<string> roleList = Data.Constants.RoleList.role_list;
        public IEnumerable<Zone> ZoneList { get; set; }
        public IList<int> AssignedZoneList { get; set; }
        public string SelectedZoneNumber { get; set; }
        public string eId { get; set; }
        public int extensionNum { get; set; }
    }
}