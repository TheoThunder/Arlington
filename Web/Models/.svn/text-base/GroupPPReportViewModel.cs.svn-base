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
    public class GroupPPReportViewModel
    {
        public GroupPPReportViewModel()
        {


        }
       
        public User user { get; set; }
        public IEnumerable<string> Monthlist = Data.Constants.MonthList.Months;
        public IEnumerable<int> Yearlist = Data.Constants.YearList.Year;

        // Region Monthly Performance Summary

        public double NumberOfWeeks { get; set; }
        public double HoldRatio { get; set; }
        public int TotalAppointments { get; set; }
        public int TotalGoodAppointments { get; set; }
        public int TotalCloses { get; set; }
        public double CloseRatio { get; set; }

        ///////////////////////////////////////////

        // Region Weekly Summary

        public int WorkingAAPerWeek { get; set; }
        public double AppointmentPerWeek { get; set; }
        public double AppointmentPWPAA { get; set; }
        public double GoodAppointmentsPerWeek { get; set; }
        public double ClosesPerWeek { get; set; }

        //////////////////////////////////////////////

        // Region Sales Agent Monthly breakdown summary

        public IList<GroupReport> SAGroupReport { get; set; }

        /////////////////////////////////////////////////


    }
}
