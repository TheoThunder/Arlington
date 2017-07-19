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
    public class SAPSReportViewModel
    {
        public SAPSReportViewModel()
        {


        }
        //
        // GET: /AllCardsViewModel/
        public User user { get; set; }
        public IEnumerable<SelectListItem> SAUsersDropdown { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        // for displaying the appointments
        public IEnumerable<AppointmentSheet> Appointments { get; set; }
        //For displaying monthly reports
        public IEnumerable<Report> MonthlyReports { get; set; }
        public Report TotalMonthlyReport { get; set; }

        //counter
        public int i { get; set; }

        // To get the information of Total Summary

        public int TotalCalls { get; set; }
        public int TotalAppointments { get; set; }
        public int TotalGoodAppointments { get; set; }
        public double AppointmentRatio { get; set; }
        public int TotalCloses { get; set; }
        public int TotalAccounts { get; set; }
        public double CloseRatio { get; set; }
    }
}
