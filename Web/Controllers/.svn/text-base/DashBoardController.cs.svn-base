using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure;
using System.Text;
using System.Web.UI;
using System.Timers;
using Web.ViewModel;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Windows;

namespace Web.Controllers
{
	public class DashBoardController : Controller
	{
		public IUserRepository _UserRepository;
		public IDashboardRepository _DashboardRespoitory;
		public IThresholdRepository _ThresholdRepository;

		public DashBoardController(IUserRepository UserRepos, IDashboardRepository DashRepos, IThresholdRepository ThresholdRepos)
		{
			_UserRepository = UserRepos;
			_ThresholdRepository = ThresholdRepos;
			_DashboardRespoitory = DashRepos;
		  
		} 

		public ActionResult Index()
		{
			DashboardViewModel dvm = new DashboardViewModel();
			var username = HttpContext.User.Identity.Name;

			var user = _UserRepository.GetUserByUsername(username);
			dvm.user = user;

			return View(dvm);
		}

		public ActionResult DashBoard(string text, double aw, double ah, int pos)
		{
			List<EmployeeDataType> data = GetSampleOrders(pos);
			/*System.Timers.Timer switchTimer = new System.Timers.Timer();
			switchTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			switchTimer.Interval = 100000;
			switchTimer.Enabled = true;*/
			string xmlnum = text;
			string chartXml = "";
			if (text.Equals("GetXML1"))
				chartXml = weekData(data, aw, ah);
			else if (text.Equals("GetXML2"))
				chartXml = todayData(data, aw, ah);
			else if (text.Equals("GetXML3"))
				chartXml = tableContents(data, aw, ah);


			return Content(chartXml);
		}

		/*private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
		}*/

		string high;
		int weekGAmtoh;
		int weekSAmtoh;
		int todaymtoh;
		string mid;
		int weekGAltom;
		int weekSAltom;
		int todayltom;
		string low;

		public List<EmployeeDataType> GetSampleOrders(int pos)
		{
			IEnumerable<Threshold> thresholds = new List<Threshold>();
			
			Threshold thresh = new Threshold();
			int temppos = -1;
			int count = 0;
			if (temppos != pos)
			{
				temppos = pos;
				while (thresholds.ToList().Count == 0 && count < 180)
				{
					if(count%60 == 0)
						thresholds = _ThresholdRepository.Thresholds; 
					count++;
					System.Threading.Thread.Sleep(1000);
				}
				thresh = thresholds.ToList()[0];
			}

			high = "Green";
			weekGAmtoh = thresh.WE_GA_Upper_Dashboard;
			weekSAmtoh = thresh.WE_SA_Upper_Dashboard;
			todaymtoh = thresh.NC_Upper_Dashboard;
			mid = "Yellow";
			weekGAltom = thresh.WE_GA_Lower_Dashboard;
			weekSAltom = thresh.WE_SA_Lower_Dashboard;
			todayltom = thresh.NC_Lower_Dashboard;
			low = "Red";
			// Get the appointments, closes, calls
		
			DateTime enddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1).AddSeconds(-1);
			DateTime startdate = enddate.AddDays(-4);
			switch (startdate.DayOfWeek.ToString())
			{
				   case "Sunday":
				   case "Saturday":
					   startdate = startdate.AddDays(-2);
					   break;
			}

			DateTime callstartdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			DateTime callenddate = callstartdate.AddDays(1).AddSeconds(-1);
			//For now we are considering 10 teams so the loop would toll 10 times.
			IEnumerable<User>[] userlist = new List<User>[20];
			Report week_record = new Report();
			Report day_record = new Report();
			count = 0;
		  //  Data.Domain.User[] userlist = new Data.Domain.User[10];
			for (int i = 0; i < userlist.Count(); i++)
			{
				userlist[i] = _UserRepository.GetAllUsersByTeam(i+1);
				if (userlist[i].Count() != 0)
				{
					count++;
					foreach (var user in userlist[i])
					{
						week_record = _DashboardRespoitory.GetWeeklyDashboard(startdate, enddate, user.UserId);
						day_record = _DashboardRespoitory.GetDailyDashboard(callstartdate, callenddate, user.UserId);
						//assignment
						user.totalAppointments = week_record.MonthlyAppointments;
						user.totalCloses = week_record.MonthlyCloses;
						user.totalGoodAppointments = week_record.MonthlyGoodAppointments;
						user.totalCalls = day_record.MonthlyCalls;
					}
				}
			}
			IEnumerable<User>[] reallist = new List<User>[count];
			int listid = 0;
			for (int j = 0; j < userlist.Count(); j++)
			{
				if (userlist[j].Count() != 0)
				{
					reallist[listid] = userlist[j];
					listid++;
				}
			}
			List<EmployeeDataType> orders = new List<EmployeeDataType>();

			int poscheck = reallist.Count();
			int displayteam = 0;

			/*if (pos%30 == 0)
			{
				pos %= pos;
			}*/

			  displayteam = (pos++) % poscheck;

			  foreach (User user in reallist[displayteam])
			  {
				  orders.Add(GenerateRandomOders(user.FirstName, user.totalGoodAppointments, user.totalAppointments, user.totalCloses, user.totalCalls, user.TeamNumber));
			  }
		   
			return orders;
		}
		private EmployeeDataType GenerateRandomOders(String employeeName, Int32 good, Int32 set, Int32 closes, Int32 today, Int32 teamNum)
		{

			try
			{
				string colorg;
				if (good > weekGAmtoh)
					colorg = high;
				else if (good >= weekGAltom)
					colorg = mid;
				else
					colorg = low;
				string colors;
				if (set > weekSAmtoh)
					colors = high;
				else if (set >= weekSAltom)
					colors = mid;
				else
					colors = low;
				//string colorc;
				//if (closes > weekmtoh)
				//    colorc = high;
				//else if (closes > weekltom)
				//    colorc = mid;
				//else
				//    colorc = low;
				string colort;
				if (today > todaymtoh)
					colort = high;
				else if (today >= todayltom)
					colort = mid;
				else
					colort = low;
				if (good > 20)
					good = 20;
				if (set > 20)
					set = 20;
				if (closes > 20)
					closes = 20;
				if (today > 300)
					today = 300;
				return new EmployeeDataType(employeeName, good, colorg, set, colors, closes, "Blue", today, colort, teamNum);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public string weekData(List<EmployeeDataType> data, double aw, double ah)
		{
			// Initialize StringBuilder class
			string chartXml = "";
			// Append chart XML data
			chartXml += "<vc:Chart xmlns:vc=\"clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts\" Width=\"" + aw + "\" Height=\"" + ah * 0.5 + "\" x:Name=\"WeekData\" Theme=\"Theme2\" BorderThickness=\"1\" View3D=\"False\" CornerRadius=\"7\" ShadowEnabled=\"True\" BorderBrush=\"#FF605E5E\" DataPointWidth=\"3\" AnimationEnabled=\"False\" Watermark=\"False\">";
			chartXml += "<vc:Chart.Titles>";
			//WeekData Title: Text
			string date = "";
			DateTime subdate = DateTime.Now.AddDays(-4);
			switch (subdate.DayOfWeek.ToString())
			{
				case "Thursday":
				case "Friday":
				case "Sunday":
				case "Saturday":
					subdate = subdate.AddDays(-2);
					break;
			}
			int count = 5;
			int dash = 0;
			date = subdate.ToString("MMMM dd, yyyy") + " - " + DateTime.Now.ToString("MMMM dd, yyyy");
			while (count > 0)
			{
				   switch (subdate.DayOfWeek.ToString())
				   {
					   case "Sunday":
					   case "Saturday":
						   dash = 0;
						   count++;
						   break;
					   default:
						   if (count == 1 || count == 5 || subdate.DayOfWeek.ToString().Equals("Friday") || subdate.DayOfWeek.ToString().Equals("Monday"))
						   {
							   //if (dash > 1)
								   //date += " - ";
							   //else
								   //if (!date.Equals(""))
									   //date += ", ";
							   //date += subdate.ToShortDateString();
						   }
						   dash++;
						   break;
				   }
				   subdate = subdate.AddDays(1);
				   count--;
			   }
			chartXml += "<vc:Title Text=\"Appointments  -  Team "+data[0].Team.ToString()+":  "+date+"\" FontSize=\"14\" FontWeight=\"Bold\" HorizontalAlignment=\"Center\" TextAlignment=\"Center\" Padding=\"0,4,0,2\"/>";
			chartXml += "</vc:Chart.Titles>";
		
			chartXml += "<vc:Chart.Background>";
			chartXml += "<LinearGradientBrush EndPoint=\"0.359,1.016\" StartPoint=\"0.319,0.078\">";
			chartXml += "<GradientStop Color=\"#EABFBFBF\" Offset=\"0.031\"/>";
			chartXml += "<GradientStop Color=\"White\" Offset=\"1\"/>";
			chartXml += "</LinearGradientBrush>";
			chartXml += "</vc:Chart.Background>";
		
			chartXml += "<vc:Chart.PlotArea>";
			chartXml += "<vc:PlotArea>";
			chartXml += "<vc:PlotArea.Background>";
			chartXml += "<LinearGradientBrush EndPoint=\"1,0.5\" StartPoint=\"0,0.5\">";
			chartXml += "<GradientStop Color=\"#FFD8D8D8\"/>";
			chartXml += "<GradientStop Color=\"#9FFEFCFC\" Offset=\"1\"/>";
			chartXml += "</LinearGradientBrush>";
			chartXml += "</vc:PlotArea.Background>";
			chartXml += "</vc:PlotArea>";
			chartXml += "</vc:Chart.PlotArea>";
		
			chartXml += "<vc:Chart.AxesX>";
			chartXml += "<vc:Axis>";
			chartXml += "<vc:Axis.AxisLabels>";
			chartXml += "<vc:AxisLabels Angle=\"0\" FontSize=\"12\" FontColor=\"Black\" FontWeight=\"Bold\"/>";
			chartXml += "</vc:Axis.AxisLabels>";
			chartXml += "</vc:Axis>";
			chartXml += "</vc:Chart.AxesX>";
			chartXml += "<vc:Chart.AxesY>";
			chartXml += "<vc:Axis AxisMinimum=\"0\" AxisMaximum=\"20\">";
			chartXml += "<vc:Axis.AxisLabels>";
			chartXml += "<vc:AxisLabels Interval=\"2\" Angle=\"0\" FontSize=\"12\" FontColor=\"Black\" FontWeight=\"Bold\"/>";
			chartXml += "</vc:Axis.AxisLabels>";
			chartXml += "<vc:Axis.Grids>";
			chartXml += "<vc:ChartGrid InterlacedColor=\"White\" />";
			chartXml += "</vc:Axis.Grids>";
			chartXml += "</vc:Axis>";
			chartXml += "</vc:Chart.AxesY>";
		
			chartXml += "<vc:Chart.Series>";
			chartXml += "<vc:DataSeries RenderAs=\"Column\" BorderColor=\"Black\" BorderThickness=\"1\" LabelEnabled=\"True\" LabelText=\"GA\" LabelFontWeight=\"Bold\" LabelFontSize=\"14\" ToolTipText=\"#AxisXLabel, Y = #YValue\" ShowInLegend=\"False\" >";
			chartXml += "<vc:DataSeries.DataPoints>";
			//WeekData DataPoint GA: AxisXLabel, Color, YValue   ~~~chartXml += "<vc:DataPoint AxisXLabel=\"Joe\" Color=\"RosyBrown\" YValue=\"2073\"/>";
			int pos = 1;
			foreach (EmployeeDataType empdata in data)
			{
				if (data.Count == 1)
				{
					chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
					chartXml += "<vc:DataPoint XValue=\"2\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorG + "\" YValue=\"" + empdata.Good + "\"/>";
					chartXml += "<vc:DataPoint XValue=\"3\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
				}
				else
				{
					if (pos == 1)
					{
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorG + "\" YValue=\"" + empdata.Good + "\"/>";
					}
					else
						chartXml += "<vc:DataPoint XValue=\"" + pos + "\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorG + "\" YValue=\"" + empdata.Good + "\"/>";
					pos++;
				}
			}
			chartXml += "</vc:DataSeries.DataPoints>";
			chartXml += "</vc:DataSeries>";
			chartXml += "<vc:DataSeries RenderAs=\"Column\" BorderColor=\"Black\" BorderThickness=\"1\" LabelEnabled=\"True\" LabelText=\"SA\" LabelFontWeight=\"Bold\" LabelFontSize=\"14\" ToolTipText=\"#AxisXLabel, Y = #YValue\" ShowInLegend=\"False\" >";
			chartXml += "<vc:DataSeries.DataPoints>";
			//WeekData DataPoint SA: AxisXLabel, Color, YValue
			pos = 1;
			foreach (EmployeeDataType empdata in data)
			{
				if (data.Count == 1)
				{
					chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
					chartXml += "<vc:DataPoint XValue=\"2\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorS + "\" YValue=\"" + empdata.Set + "\"/>";
					chartXml += "<vc:DataPoint XValue=\"3\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
				}
				else
				{
					if (pos == 1)
					{
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorS + "\" YValue=\"" + empdata.Set + "\"/>";
					}
					else
						chartXml += "<vc:DataPoint XValue=\"" + pos + "\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorS + "\" YValue=\"" + empdata.Set + "\"/>";
					pos++;
				}
			}
			chartXml += "</vc:DataSeries.DataPoints>";
			chartXml += "</vc:DataSeries>";
			chartXml += "<vc:DataSeries RenderAs=\"Column\" BorderColor=\"Black\" BorderThickness=\"1\" LabelEnabled=\"True\" LabelText=\"C\" LabelFontWeight=\"Bold\" LabelFontSize=\"14\" ToolTipText=\"#AxisXLabel, Y = #YValue\" ShowInLegend=\"False\" >";
			chartXml += "<vc:DataSeries.DataPoints>";
			//DataPoint C: AxisXLabel, Color, YValue
			pos = 1;
			foreach (EmployeeDataType empdata in data)
			{
				if (data.Count == 1)
				{
					chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\"/>";
					chartXml += "<vc:DataPoint XValue=\"2\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorC + "\" YValue=\"" + empdata.Closes + "\"/>";
					chartXml += "<vc:DataPoint XValue=\"3\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
				}
				else
				{
					if (pos == 1)
					{
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorC + "\" YValue=\"" + empdata.Closes + "\"/>";
					}
					else
						chartXml += "<vc:DataPoint XValue=\"" + pos + "\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorC + "\" YValue=\"" + empdata.Closes + "\"/>";
					pos++;
				}
			}
			chartXml += "</vc:DataSeries.DataPoints>";
			chartXml += "</vc:DataSeries>";
			chartXml += "</vc:Chart.Series>";
			chartXml += "</vc:Chart>";

			// Write object to an HTTP response stream
			return chartXml;
		}
		public string todayData(List<EmployeeDataType> data, double aw, double ah)
		{
			// Initialize StringBuilder class
			string chartXml = "";

			chartXml += "<vc:Chart xmlns:vc=\"clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts\" Width=\"" + aw * 0.7 + "\" Height=\"" + ah * 0.5 + "\" x:Name=\"TotalDay\" Theme=\"Theme1\" BorderThickness=\"1\" View3D=\"false\" CornerRadius=\"7\" ShadowEnabled=\"True\" BorderBrush=\"#FF605E5E\" DataPointWidth=\"9\" AnimationEnabled=\"False\" Watermark=\"False\">";

			chartXml += "<vc:Chart.Titles>";
			//TotalDay Title: Text
			string date = DateTime.Now.ToShortDateString();
			chartXml += "<vc:Title Text=\"Number of Calls  -  Team " + data[0].Team.ToString() + ":  " + DateTime.Now.ToString("MMMM dd, yyyy") + "\" FontSize=\"14\" FontWeight=\"Bold\" HorizontalAlignment=\"Center\" TextAlignment=\"Center\" Padding=\"0,4,0,2\"/>";
			chartXml += "</vc:Chart.Titles>";

			chartXml += "<vc:Chart.Background>";
			chartXml += "<LinearGradientBrush EndPoint=\"0.359,1.016\" StartPoint=\"0.319,0.078\">";
			chartXml += "<GradientStop Color=\"#EABFBFBF\" Offset=\"0.031\"/>";
			chartXml += "<GradientStop Color=\"White\" Offset=\"1\"/>";
			chartXml += "</LinearGradientBrush>";
			chartXml += "</vc:Chart.Background>";


			chartXml += "<vc:Chart.PlotArea>";
			chartXml += "<vc:PlotArea>";
			chartXml += "<vc:PlotArea.Background>";
			chartXml += "<LinearGradientBrush EndPoint=\"1,0.5\" StartPoint=\"0,0.5\">";
			chartXml += "<GradientStop Color=\"#FFD8D8D8\"/>";
			chartXml += "<GradientStop Color=\"#9FFEFCFC\" Offset=\"1\"/>";
			chartXml += "</LinearGradientBrush>";
			chartXml += "</vc:PlotArea.Background>";
			chartXml += "</vc:PlotArea>";
			chartXml += "</vc:Chart.PlotArea>";

			chartXml += "<vc:Chart.AxesX>";
			chartXml += "<vc:Axis>";
			chartXml += "<vc:Axis.AxisLabels>";
			chartXml += "<vc:AxisLabels Angle=\"0\" FontSize=\"12\" FontColor=\"Black\" FontWeight=\"Bold\"/>";
			chartXml += "</vc:Axis.AxisLabels>";
			chartXml += "</vc:Axis>";
			chartXml += "</vc:Chart.AxesX>";
			chartXml += "<vc:Chart.AxesY>";
			chartXml += "<vc:Axis AxisMinimum=\"0\" AxisMaximum=\"300\">";
			chartXml += "<vc:Axis.AxisLabels>";
			chartXml += "<vc:AxisLabels Interval=\"50\" Angle=\"0\" FontSize=\"12\" FontColor=\"Black\" FontWeight=\"Bold\"/>";
			chartXml += "</vc:Axis.AxisLabels>";
			chartXml += "<vc:Axis.Grids>";
			chartXml += "<vc:ChartGrid InterlacedColor=\"White\" />";
			chartXml += "</vc:Axis.Grids>";
			chartXml += "</vc:Axis>";
			chartXml += "</vc:Chart.AxesY>";

			chartXml += "<vc:Chart.Series>";
			chartXml += "<vc:DataSeries RenderAs=\"Bar\" BorderColor=\"Black\" BorderThickness=\"2\" LabelEnabled=\"True\" LabelFontWeight=\"Bold\" LabelFontSize=\"14\" ToolTipText=\"#AxisXLabel, Y = #YValue\" ShowInLegend=\"False\" >";
			chartXml += "<vc:DataSeries.DataPoints>";
			//WeekData DataPoint SA: AxisXLabel, Color, YValue    ~~~    chartXml += "<vc:DataPoint AxisXLabel=\"Joe\" Color=\"RosyBrown\" YValue=\"2073\"/>";
			int pos = 1;
			foreach (EmployeeDataType empdata in data)
			{
				if(data.Count == 1)
				{
					chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
					chartXml += "<vc:DataPoint XValue=\"2\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorT + "\" YValue=\"" + empdata.TodayT + "\"/>";
					chartXml += "<vc:DataPoint XValue=\"3\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
				}
				else
				{
					if (pos == 1)
					{
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\" \" BorderColor=\"Transparent\" LabelEnabled=\"False\" Color=\"Transparent\" YValue=\"1\" />";
						chartXml += "<vc:DataPoint XValue=\"1\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorT + "\" YValue=\"" + empdata.TodayT + "\"/>";
					}
					else
						chartXml += "<vc:DataPoint XValue=\"" + pos + "\" AxisXLabel=\"" + empdata.Employee + "\" Color=\"" + empdata.ColorT + "\" YValue=\"" + empdata.TodayT + "\"/>";
					pos++;
				}
			}
			
			chartXml += "</vc:DataSeries.DataPoints>";
			chartXml += "</vc:DataSeries>";
			chartXml += "</vc:Chart.Series>";
			chartXml += "</vc:Chart>";

			// Write object to an HTTP response stream
			return chartXml;
		}

		public string tableContents(List<EmployeeDataType> data, double aw, double ah)
		{
			// Initialize StringBuilder class
			string chartXml = "";

			chartXml += "<vc:Chart xmlns:vc=\"clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts\" Width=\"" + aw * 0.3 + "\" Height=\"" + ah * 0.5 + "\" x:Name=\"Legend\" Background=\"Transparent\" View3D=\"False\" BorderThickness=\"0\" CornerRadius=\"7\" ShadowEnabled=\"False\" BorderBrush=\"Transparent\" AnimationEnabled=\"False\" Watermark=\"False\">";

			chartXml += "<vc:Chart.Legends>";
			chartXml += "<vc:Legend Background=\"Transparent\" BorderThickness=\"1\" VerticalAlignment=\"Center\" HorizontalAlignment=\"Center\" FontSize=\"13\" CornerRadius=\"7\" />";
			chartXml += "</vc:Chart.Legends>";

			chartXml += "<vc:Chart.PlotArea>";
			chartXml += "<vc:PlotArea Background=\"Transparent\" BorderThickness=\"0\" Padding=\"0\" />";
			chartXml += "</vc:Chart.PlotArea>";

			chartXml += "<vc:Chart.AxesX>";
			chartXml += "<vc:Axis LineThickness=\"0\">";
			chartXml += "<vc:Axis.AxisLabels>";
			chartXml += "<vc:AxisLabels FontColor=\"Transparent\" BorderBrush=\"Transparent\"/>";
			chartXml += "</vc:Axis.AxisLabels>";
			chartXml += "<vc:Axis.Grids>";
			chartXml += "<vc:ChartGrid LineThickness=\"0\" BorderThickness=\"0\" BorderBrush=\"Transparent\" />";
			chartXml += "</vc:Axis.Grids>";
			chartXml += "</vc:Axis>";
			chartXml += "</vc:Chart.AxesX>";
			chartXml += "<vc:Chart.AxesY>";
			chartXml += "<vc:Axis LineThickness=\"0\">";
			chartXml += "<vc:Axis.AxisLabels>";
			chartXml += "<vc:AxisLabels FontColor=\"Transparent\" BorderBrush=\"Transparent\"/>";
			chartXml += "</vc:Axis.AxisLabels>";
			chartXml += "<vc:Axis.Grids>";
			chartXml += "<vc:ChartGrid LineThickness=\"0\" BorderThickness=\"0\" BorderBrush=\"Transparent\" />";
			chartXml += "</vc:Axis.Grids>";
			chartXml += "</vc:Axis>";
			chartXml += "</vc:Chart.AxesY>";

			chartXml += "<vc:Chart.Series>";
			chartXml += "<vc:DataSeries Visibility=\"Collapsed\" RenderAs=\"Bar\" ShowInLegend=\"True\">";
			chartXml += "<vc:DataSeries.DataPoints>";
			//Colored Table Contents: Color, LegendText    ~~~    chartXml += "<vc:DataPoint Color=\"RosyBrown\" XValue=\"0\" YValue=\"0\" LegendText=\"High Efficiency\"/>";
			chartXml += "<vc:DataPoint Color=\"Green\" XValue=\"0\" YValue=\"0\" LegendText=\"High Efficiency\"/>";
			chartXml += "<vc:DataPoint Color=\"Orange\" XValue=\"0\" YValue=\"0\" LegendText=\"Low Efficiency\"/>";
			chartXml += "<vc:DataPoint Color=\"Red\" XValue=\"0\" YValue=\"0\" LegendText=\"Critical Efficiency\"/>";
			//Text Table Contents: LegendText   ~~~   chartXml += "<vc:DataPoint Color=\"Transparent\" XValue=\"0\" YValue=\"0\" LegendText=\"GA: Good Appointment\"/>";
			chartXml += "<vc:DataPoint Color=\"Transparent\" XValue=\"0\" YValue=\"0\" LegendText=\"GA: Good Appointment\"/>";
			chartXml += "<vc:DataPoint Color=\"Transparent\" XValue=\"0\" YValue=\"0\" LegendText=\"SA: Set Appointment\"/>";
			chartXml += "<vc:DataPoint Color=\"Transparent\" XValue=\"0\" YValue=\"0\" LegendText=\"C: Closes\"/>";
			chartXml += "</vc:DataSeries.DataPoints>";
			chartXml += "</vc:DataSeries>";
			chartXml += "</vc:Chart.Series>";
			chartXml += "</vc:Chart>";

			// Write object to an HTTP response stream
			return chartXml;
		}
	}
}
