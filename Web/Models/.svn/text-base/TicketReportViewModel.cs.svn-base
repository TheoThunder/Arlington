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
    public class TicketReportViewModel
    {
        public TicketReportViewModel()
        {


        }
        //
        // GET: /AllCardsViewModel/
        public User user { get; set; }
        public IEnumerable<SelectListItem> UsersDropdown { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        // for displaying the appointments
        public IEnumerable<Ticket> Tickets { get; set; }
        //For displaying monthly reports
        public IEnumerable<Report> MonthlyReports { get; set; }
        public Report TotalMonthlyReport { get; set; }

        //counter
        public int i { get; set; }

    }
}
