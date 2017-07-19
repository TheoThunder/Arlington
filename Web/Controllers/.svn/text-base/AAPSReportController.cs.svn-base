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
	public class AAPSReportController : Controller
	{
		public IAccountRepository _AccountRepository;
		public IUserRepository _UserRepository;
		public ILeadRepository _LeadRepository;
		public IAppointmentSheet _AppointmentRepository;
		ILeadProfileService _service;
		public IAAPSReportRepository _ReportRepository;
		public IGenericUsageRepositoryInterface _UsageRepos;


		public AAPSReportController(IGenericUsageRepositoryInterface UsageRepos, IAAPSReportRepository ReportRepos, ILeadProfileService service, IAccountRepository AccountRepository, IUserRepository UserRepository, ILeadRepository LeadRepos, IAppointmentSheet AppointmentRepos)
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
			AAPSReportViewModel vm = new AAPSReportViewModel();
			var username = HttpContext.User.Identity.Name;
			vm.user = _UserRepository.GetUserByUsername(username);
			var aaUsersResult = _UserRepository.GetAllUsersByRole(4);
			vm.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
			{
				Text = row.LastName + "," + row.FirstName,
				Value = row.LastName + "," + row.FirstName
			});
			return View(vm);
		}

		private int GetNumberOfAccounts(int leadid)
		{
			var results = _service.GetAllAccountsForLead(leadid);
			int number = results.Count();
			return number;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult AppointmentSummary(DateTime id, DateTime text, string value)
		{
			AAPSReportViewModel vm = new AAPSReportViewModel();

			IList<Data.Domain.AppointmentSheet> appointments = new List<Data.Domain.AppointmentSheet>();

			vm.i = 0;

			// Get the number of appointments

			var results = _AppointmentRepository.AppointmentSheets;
			foreach (var result in results)
			{
				var assignedaaagentid = result.CreatorId;

				//to get the hidden appointment agent field in datatable
				User newUser = _UserRepository.GetUserById(assignedaaagentid);
				if (newUser == null)
					result.AssignedUser.UserName = "Not Assigned";
				else
					result.AssignedUser = newUser;

				// To get the number of accounts for this person
				result.Accounts = GetNumberOfAccounts(result.ParentLeadId);
				if (result.Accounts == 0)
					result.Closed = "No";
				else
					result.Closed = "Yes";

				result.companyname = _LeadRepository.LeadByLeadID(result.ParentLeadId).CompanyName;
				appointments.Add(result);
				vm.Appointments = appointments;
			}
			return PartialView(vm);
		}
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult MonthlySummary(DateTime id, DateTime text, string value)
		{
			AAPSReportViewModel vm = new AAPSReportViewModel();
			// vm.MonthlyReports = _ReportRepository.Reports;

			int TotalAppointments = 0;
			int TotalAccounts = 0;
			int TotalCalls = 0;
			int TotalGoodAppointments = 0;
			int TotalCloses = 0;
			Report totalRecord = new Report();

			string[] names = value.Split(',');
			User user = _UsageRepos.GetUserIDByName(names[0], names[1]);
			//int[] newList = _ReportRepository.GetTotalValue(id, text, user.UserId);
			vm.MonthlyReports = _ReportRepository.GetMonthlyReport(id, text, user.UserId);

			//To display the total of the report
			foreach (var report in vm.MonthlyReports)
			{
				TotalAppointments += report.MonthlyAppointments;
				TotalAccounts += report.MonthlyAccounts;
				TotalCalls += report.MonthlyCalls;
				TotalGoodAppointments += report.MonthlyGoodAppointments;
				TotalCloses += report.MonthlyCloses;
			}
			totalRecord.Month = "Total";
			totalRecord.MonthlyAppointments = TotalAppointments;
			totalRecord.MonthlyAccounts = TotalAccounts;
			totalRecord.MonthlyCalls = TotalCalls;
			totalRecord.MonthlyCloses = TotalCloses;
			totalRecord.MonthlyGoodAppointments = TotalGoodAppointments;
			vm.TotalMonthlyReport = totalRecord;


			return PartialView(vm);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult TotalSummary(DateTime id, DateTime text, string value)
		{

			//Get the selected user information
			string[] names = value.Split(',');
			User user = _UsageRepos.GetUserIDByName(names[0], names[1]);

			int[] newList = _ReportRepository.GetTotalValue(id, text, user.UserId);

			AAPSReportViewModel vm = new AAPSReportViewModel();
			vm.TotalAppointments = newList[0];
			vm.TotalAccounts = newList[1];
			vm.TotalCalls = newList[2];
			vm.TotalCloses = newList[3];
			vm.TotalGoodAppointments = newList[4];

			//To check if denominator is not zero
			if (vm.TotalAppointments != 0)
				vm.AppointmentRatio = (vm.TotalGoodAppointments / (double)vm.TotalAppointments);
			else
				vm.AppointmentRatio = 0;

			//To check if denominator is not zero
			if (vm.TotalGoodAppointments != 0)
				vm.CloseRatio = (vm.TotalCloses / (double)vm.TotalGoodAppointments);

			return PartialView(vm);
		}
	}
}
