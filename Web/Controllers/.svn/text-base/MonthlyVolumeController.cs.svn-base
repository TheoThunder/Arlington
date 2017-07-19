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
    public class MonthlyVolumeController : Controller
    {
        public IAccountRepository _AccountRepository;
        public IUserRepository _UserRepository;
        public ILeadRepository _LeadRepository;
        public IAppointmentSheet _AppointmentRepository;
        ILeadProfileService _service;
        public IMonthlyVolumeReportRepository _ReportRepository;
        public IGenericUsageRepositoryInterface _UsageRepos;
        public IZoneRepository _zoneRepos;

        public MonthlyVolumeController(IZoneRepository ZoneRepository, IGenericUsageRepositoryInterface UsageRepos, IMonthlyVolumeReportRepository ReportRepos, ILeadProfileService service, IAccountRepository AccountRepository, IUserRepository UserRepository, ILeadRepository LeadRepos, IAppointmentSheet AppointmentRepos)
        {
            _service = service;
            _AccountRepository = AccountRepository;
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepos;
            _AppointmentRepository = AppointmentRepos;
            _ReportRepository = ReportRepos;
            _UsageRepos = UsageRepos;
            _zoneRepos = ZoneRepository;
        }

        public ActionResult Index()
        {
            MonthlyVolumeReportViewModel vm = new MonthlyVolumeReportViewModel();

            var username = HttpContext.User.Identity.Name;
            vm.user = _UserRepository.GetUserByUsername(username);

            var aaUsersResult = _UserRepository.GetAllUsersByRole(4);
            vm.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.LastName + "," + row.FirstName
            });

            var saUsersResult = _UserRepository.GetAllUsersByRole(3);
            vm.SAUsersDropdown = saUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.LastName + "," + row.FirstName
            });

            IEnumerable<Zone> zones = _zoneRepos.Zones;
            IList<int> zonelist = new List<int>();
            foreach (var zone in zones)
            {
                zonelist.Add(zone.ZoneNumber);
            }

            vm.ZoneDropdown = zonelist;


            // Getting the values for the tables

            IEnumerable<Account> AccountList = new List<Account>();
            IList<Account> ActualList = new List<Account>();
            AccountList = _AccountRepository.Accounts;
            foreach (var account in AccountList)
            {
                account.AssignedSA = _UserRepository.GetUserById(account.AssignedSalesRep);
                account.AssignedUser = _UserRepository.GetUserById(account.AACreator);
                if (account.MailingZipcode != "")
                {
                    account.Zone = _zoneRepos.GetZoneByZipcode(account.MailingZipcode);
                }
                else
                {
                    account.Zone = 0;
                }
                ActualList.Add(account);
            }
            vm.Accounts = ActualList;

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MonthlySummary(DateTime id, DateTime text, string value, string volume)
        {
            MonthlyVolumeReportViewModel vm = new MonthlyVolumeReportViewModel();
            
            Report totalRecord = new Report();

            string[] names = value.Split(',');
            User user = _UsageRepos.GetUserIDByName(names[0], names[1]);
            if (user.AssignedRoleId == 4)
            {
                vm.MonthlyReports = _ReportRepository.GetMonthlyReport(id, text, user.UserId, volume);
            }
            else if (user.AssignedRoleId == 3)
            {
                vm.MonthlyReports = _ReportRepository.GetMonthlySAReport(id, text, user.UserId, volume);
            }

           // To display the total of the report
            foreach (var report in vm.MonthlyReports)
            {
                TotalAccounts = TotalAccounts + report.MonthlyAccounts;
            }
            totalRecord.Month = "Total";
            totalRecord.MonthlyAccounts = TotalAccounts;
            
            vm.TotalMonthlyReport = totalRecord;


            return PartialView(vm);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TotalSummary(DateTime id, DateTime text, string value, string savalue, string volume)
        {
            MonthlyVolumeReportViewModel vm = new MonthlyVolumeReportViewModel();

            Report totalRecord = new Report();

            string[] names = value.Split(',');
            string[] saname = savalue.Split(',');
            User aauser = _UsageRepos.GetUserIDByName(names[0], names[1]);
            User sauser = _UsageRepos.GetUserIDByName(saname[0], saname[1]);
           
            vm.MonthlyReports = _ReportRepository.GetTotalReport(id, text, aauser.UserId, sauser.UserId, volume);
            

            // To display the total of the report
            foreach (var report in vm.MonthlyReports)
            {
                TotalAccounts = TotalAccounts + report.MonthlyAccounts;
            }
            totalRecord.Month = "Total";
            totalRecord.MonthlyAccounts = TotalAccounts;

            vm.TotalMonthlyReport = totalRecord;


            return PartialView(vm);
        }
        public int TotalAccounts { get; set; }
    }
}
