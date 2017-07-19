using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class Ticket
    {
        public Ticket()
        { 
          
            AssignedUser = new User();
        }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int TicketId { get; set; } //Datbase Id

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int TicketNumber { get; set; }
        public string CustomerName { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
       

        public int Creator { get; set; }
        public int CurrentOwner { get; set; }
        public int ClosedBy { get; set; }

        public int Zone { get; set; }
        public string Priority { get; set; }

        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }

        public string AccountName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string TicketType { get; set; }
        public string Reason { get; set; }
        public string TicketOrigin { get; set; }
        public string ReceivedFrom { get; set; }
        public string CallBackNumber { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Comments { get; set; }
        public string Action { get; set; }

        public int AccountId { get; set; }

        // To display the current owner's name
        public User AssignedUser { get; set; }

        //To display the creator's name
        public User CreatorName { get; set; }
        public string TicketHistoryID { get; set; }


        // TO display Creator and CLoser's name in Reports
        public User CreaterBy { get; set; }
        public User ClosedByUser { get; set; }

    }
}
