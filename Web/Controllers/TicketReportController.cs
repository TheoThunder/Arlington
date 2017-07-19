using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using INF = Infrastructure;
using System;
using Newtonsoft.Json;
using Data.Domain;
using Web.ViewModel;

namespace Web.Controllers
{
    public class TicketReportController : Controller
    {
        public ITicketRepository _ticketRepository;
        public IUserRepository _UserRepository;
        public IZoneRepository _zoneRepos;
        public IGenericUsageRepositoryInterface _UsageRepos;
        public IAccountRepository _AccountRepository;
        public ITicketHistoryRepository _ticketHistoryRepository;
        public ITicketReportRepository _ReportRepository;

        public TicketReportController(ITicketHistoryRepository HistoryRepos, ITicketReportRepository ReportRepos, IAccountRepository AccountRepos, IGenericUsageRepositoryInterface URepository, ITicketRepository TicketRepository, IUserRepository UserRepos, IZoneRepository ZoneRepos)
        {
            _ticketRepository = TicketRepository;
            _UserRepository = UserRepos;
            _zoneRepos = ZoneRepos; 
            _UsageRepos = URepository;
            _AccountRepository = AccountRepos;
            _ticketHistoryRepository = HistoryRepos;
            _ReportRepository = ReportRepos;
          
        }

        public ActionResult Index()
        {
            TicketReportViewModel tvm = new TicketReportViewModel();

            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            tvm.user = user;
            tvm.Tickets = _ticketRepository.Tickets;
            foreach (var ticket in tvm.Tickets)
            {
                if (ticket.Creator != 0)
                {
                    ticket.CreaterBy = _UserRepository.GetUserById(ticket.Creator);
                }
                if (ticket.ClosedBy != 0)
                {
                    ticket.ClosedByUser = _UserRepository.GetUserById(ticket.ClosedBy);
                }
            }
            IEnumerable<User> SAUsers = new List<User>();
            IEnumerable<User> CSRUsers = new List<User>();
            IList<User> UsersResult = new List<User>();

            SAUsers = _UserRepository.GetAllUsersByRole(3);
            CSRUsers = _UserRepository.GetAllUsersByRole(5);
            foreach (var usr in SAUsers)
            {
                UsersResult.Add(usr);
            }
            foreach (var usr in CSRUsers)
            {
                UsersResult.Add(usr);
            }
           
            tvm.UsersDropdown = UsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.LastName + "," + row.FirstName
            });
        
            return View(tvm);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MonthlySummary(DateTime id, DateTime text, string value)
        {
            TicketReportViewModel vm = new TicketReportViewModel();
           

            int TotalTicketsOpened = 0;
            int TotalTicketsClosed = 0;
         
            Report totalRecord = new Report();

            string[] names = value.Split(',');
            User user = _UsageRepos.GetUserIDByName(names[0], names[1]);
            //int[] newList = _ReportRepository.GetTotalValue(id, text, user.UserId);
            vm.MonthlyReports = _ReportRepository.GetMonthlyReport(id, text, user.UserId);

            //To display the total of the report
            foreach (var report in vm.MonthlyReports)
            {

                TotalTicketsOpened = TotalTicketsOpened + report.TicketsOpened;
                TotalTicketsClosed = TotalTicketsClosed + report.TicketsClosed;
            }
            totalRecord.Month = "Total";
      
            totalRecord.TicketsOpened = TotalTicketsOpened;
            totalRecord.TicketsClosed = TotalTicketsClosed;
            vm.TotalMonthlyReport = totalRecord;


            return PartialView(vm);
        }

    }
}
