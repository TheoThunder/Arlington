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
    public class MonthlyVolumeReportViewModel
    {
        public MonthlyVolumeReportViewModel()
        {


        }
        //
        // GET: /AllCardsViewModel/
        public User user { get; set; }
        public IEnumerable<SelectListItem> AAUsersDropdown { get; set; }
        public IEnumerable<SelectListItem> SAUsersDropdown { get; set; }

        
        public IEnumerable<int> ZoneDropdown { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        // for displaying the accounts
        public IEnumerable<Account> Accounts { get; set; }
       
        public int i { get; set; }

        // To get the information of Total Summary

        public IEnumerable<Report> MonthlyReports { get; set; }
        public Report TotalMonthlyReport { get; set; }
       
    }
}
