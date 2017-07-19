using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class TicketHistory
    {
        public TicketHistory()
        {

            AssignedUser = new User();
        }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int HistoryId { get; set; } //Datbase Id

        
        public string TicketId { get; set; }

        public int UserWorked { get; set; }

        public DateTime HistoryDate { get; set; }
       
        public string Comment { get; set; }
        public string Action { get; set; }

        public int AccountId { get; set; }

        // To display the User's Name
        public User AssignedUser { get; set; }

    }
}
