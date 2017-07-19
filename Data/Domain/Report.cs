using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class Report
    {
        public Report()
        {
            
            AssignedUser = new User();
          
        }
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int ReportId { get; set; } //Database Id

        public string Month { get; set; }
        public int Year { get; set; }
        public int MonthlyCalls { get; set; }
        public int MonthlyAppointments { get; set; }
        public int MonthlyGoodAppointments { get; set; }
        public int MonthlyCloses { get; set; }
        public int MonthlyAccounts { get; set; }

        public int AssignedAAUserID { get; set; }
        public int AssignedSAUserID { get; set; }
        public User AssignedUser { get; set; }


        public int TicketsOpened { get; set; }
        public int TicketsClosed { get; set; }

        public int NumberOfAAPerMonth { get; set; }
        
    }
}
