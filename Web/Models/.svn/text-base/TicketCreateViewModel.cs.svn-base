using System.ComponentModel;
using Data.Domain;
using System.Collections.Generic;
using System;
using System.Web.Mvc;

namespace Web.ViewModel
{
    public class TicketCreateViewModel
    {

        public User user { get; set; }
        public Ticket ticket { get; set; }
        public TicketHistory ticketHistory { get; set; }
        public Account account { get; set; }


        public IEnumerable<string> Priority = Data.Constants.Priority.ticketPriority;
        public IEnumerable<string> Status = Data.Constants.TicketStatus.ticketStatus;
        public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }

        public IEnumerable<string> Type = Data.Constants.TicketType.tickettype;
        public IEnumerable<string> Reason = Data.Constants.TicketReason.reason;
        public IEnumerable<string> Origin = Data.Constants.TicketOrigin.origin;
        public IEnumerable<int> ZoneDropdown { get; set; }



    }
}