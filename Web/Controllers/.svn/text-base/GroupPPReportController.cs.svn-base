using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using Web.ViewModel;
using Web.Service.Abstract;
using Data.Domain;

namespace Web.Controllers
{
    public class GroupPPReportController : Controller
    {
        public IAccountRepository _AccountRepository;
        public IUserRepository _UserRepository;
        public ILeadRepository _LeadRepository;
        public IAppointmentSheet _AppointmentRepository;
        ILeadProfileService _service;
        public IGroupPPReportRepository _ReportRepository;
        public IGenericUsageRepositoryInterface _UsageRepos;

        public GroupPPReportController(IGenericUsageRepositoryInterface UsageRepos, IGroupPPReportRepository ReportRepos, ILeadProfileService service, IAccountRepository AccountRepository, IUserRepository UserRepository, ILeadRepository LeadRepos, IAppointmentSheet AppointmentRepos)
        {
            _service = service;
            _AccountRepository = AccountRepository;
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepos;
            _AppointmentRepository = AppointmentRepos;
            _ReportRepository = ReportRepos;
            _UsageRepos = UsageRepos;

        }

        public ActionResult Index()
        {
            GroupPPReportViewModel vm = new GroupPPReportViewModel();
            
            var username = HttpContext.User.Identity.Name;
            vm.user = _UserRepository.GetUserByUsername(username);

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MonthlyPerformance(int id, string text)
        {
            GroupPPReportViewModel vm = new GroupPPReportViewModel();
            int first = 1;
            Report monthly_report = new Report();
            DateTime startdate;
            DateTime enddate;

            string start_date = GetIntForStringMonth(text).ToString() + "/" + first.ToString() + "/" + id.ToString();
            startdate = DateTime.Parse(start_date);
            enddate = startdate.AddMonths(1).AddSeconds(-1);

           
            monthly_report = _ReportRepository.GetMonthlyReport(startdate, enddate);

            //Assignment ----------------------------------
            vm.NumberOfWeeks = (((double)enddate.Day) / (double)7);
            vm.TotalAppointments = monthly_report.MonthlyAppointments;
            vm.TotalGoodAppointments = monthly_report.MonthlyGoodAppointments;
            vm.TotalCloses = monthly_report.MonthlyCloses;
            if(vm.TotalAppointments != 0)
            {
                vm.HoldRatio = ((double)vm.TotalGoodAppointments / (double)vm.TotalAppointments) * (double)100;
            }
            if (vm.TotalGoodAppointments != 0)
            {
                vm.CloseRatio = ((double)vm.TotalCloses / (double)vm.TotalGoodAppointments) * (double)100;
            }

            return PartialView(vm);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult WeeklyPerformance(int id, string text)
        {
            GroupPPReportViewModel vm = new GroupPPReportViewModel();
            int first = 1;
            Report monthly_report = new Report();
            DateTime startdate;
            DateTime enddate;

            string start_date = GetIntForStringMonth(text).ToString() + "/" + first.ToString() + "/" + id.ToString();
            startdate = DateTime.Parse(start_date);
            enddate = startdate.AddMonths(1).AddSeconds(-1);


            monthly_report = _ReportRepository.GetMonthlyReport(startdate, enddate);

            //Assignment ----------------------------------
            vm.NumberOfWeeks = (((double)enddate.Day) / (double)7);
            vm.WorkingAAPerWeek = monthly_report.NumberOfAAPerMonth;
            vm.AppointmentPerWeek = (double)monthly_report.MonthlyAppointments / vm.NumberOfWeeks;
            vm.AppointmentPWPAA = ((double)vm.AppointmentPerWeek / vm.NumberOfWeeks) / (double)vm.WorkingAAPerWeek;
            vm.GoodAppointmentsPerWeek = (double)monthly_report.MonthlyGoodAppointments / vm.NumberOfWeeks;
            vm.ClosesPerWeek = (double)monthly_report.MonthlyCloses / vm.NumberOfWeeks;
            
            return PartialView(vm);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SalesAgentPerformance(int id, string text)
        {
            GroupPPReportViewModel vm = new GroupPPReportViewModel();
            int first = 1;
            Report monthly_report = new Report();
            IList<GroupReport> group_monthly_report = new List<GroupReport>();
            DateTime startdate;
            DateTime enddate;

            string start_date = GetIntForStringMonth(text).ToString() + "/" + first.ToString() + "/" + id.ToString();
            startdate = DateTime.Parse(start_date);
            enddate = startdate.AddMonths(1).AddSeconds(-1);

            IEnumerable<User> sa_list = _UserRepository.GetAllUsersByRole(3);



            group_monthly_report = _ReportRepository.GetMonthlyReportForSA(startdate, enddate, sa_list);

            vm.SAGroupReport = group_monthly_report;

            return PartialView(vm);
        }
        public int GetIntForStringMonth(string month)
        {

            int monthvalue = 0;
            switch (month)
            {
                case "January":
                    monthvalue = 1;
                    break;
                case "February":
                    monthvalue = 2;
                    break;
                case "March":
                    monthvalue = 3;
                    break;
                case "April":
                    monthvalue = 4;
                    break;
                case "May":
                    monthvalue = 5;
                    break;
                case "June":
                    monthvalue = 6;
                    break;
                case "July":
                    monthvalue = 7;
                    break;
                case "August":
                    monthvalue = 8;
                    break;
                case "September":
                    monthvalue = 9;
                    break;
                case "October":
                    monthvalue = 10;
                    break;
                case "November":
                    monthvalue = 11;
                    break;
                case "December":
                    monthvalue = 12;
                    break;
            }


            return monthvalue;
        }
    }
}
