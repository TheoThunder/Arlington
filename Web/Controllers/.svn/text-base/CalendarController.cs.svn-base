using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Data.Domain;
using Data.Repositories.Abstract;
using Data.Repositories.Static;
using Web.ViewModel;
using Web.Service;

namespace Web.Controllers
{
    public class CalendarController : Controller
    {
        private LeadProfileService _service;
        private ICalendarEventRepository _eventsRepository;
        private ITimeSlotRepository _timeSlotRepository;
        private IUserRepository _UserRepository;
        private IThresholdRepository _ThresholdRepository;
        private IAppointmentSheet _appointmentsRepository;


        int LeadIdforcal=0;
        

        public CalendarController(IThresholdRepository Threshold, IUserRepository UserRepository, ICalendarEventRepository eventsRepository, ITimeSlotRepository timeSlotRepository, IAppointmentSheet appointmentsRepository)//, LeadProfileService leadRepos)
        {
            //eventsRepository = new StaticCalendarEventRepository();
            this._eventsRepository = eventsRepository;
            _timeSlotRepository = timeSlotRepository;
            _UserRepository = UserRepository;
            _ThresholdRepository = Threshold;
            _appointmentsRepository = appointmentsRepository;
 
        }

        public ActionResult ManCalendar()
        {
         
            CalendarEventViewModel cvm = new CalendarEventViewModel();
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            cvm.User = user;
            return View(cvm);
        }

        public ActionResult Index()
        {
            CalendarEventViewModel cevm = new CalendarEventViewModel();

            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            cevm.User = user;
            return View(cevm);
        }

        public ActionResult Calendar()
        {
            //string test = Membership.GetUser().UserName;
            return View();
        }

        private string zeroPad(int num) 
        {
            string numZeropad = num + "";
            while (numZeropad.Length < 2) 
            {
            numZeropad = "0" + numZeropad;
                
            }
            return numZeropad;
        }
     
        
        
       public JsonResult GetEventsPopup(double start, double end)
        {
            string z1;
            int a;

            Threshold threshold = new Threshold();
            threshold = _ThresholdRepository.Thresholds.First();

            var taskList = new List<CalendarDTO>();
            IEnumerable<TimeSlot> timeslots = new List<TimeSlot>();
            timeslots = _timeSlotRepository.TimeSlots;
            // To get the calander events

            IEnumerable<CalendarEvent> events = new List<CalendarEvent>();
            events = _eventsRepository.CalendarEvents;

            //IEnumerable<User> users = new List<User>();
            //users = _UserRepository.GetAllUsersByRole(3);
            // a = users.Count();
            
            //The following creates all time slots form 2011 to 2020
            //make sure you update the time slots based on the number of users, this should just be used as a temporary thing
           /*
            #region Create all time slots 
            TimeSlot newTimeSlot = new TimeSlot();
            int nextHour;
            for (int year = 2011; year <= 2011; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= 31; day++)
                    {
                        for (int hour = 9; hour <= 17; hour++)
                       {
                            nextHour = hour + 1;
                            newTimeSlot.Title = a;
                            newTimeSlot.StartTime = year + "-" + zeroPad(month) + "-" + zeroPad(day) + "T" + zeroPad(hour) + ":00:00";
                            newTimeSlot.EndTime = year + "-" + zeroPad(month) + "-" + zeroPad(day) + "T" + zeroPad(nextHour) + ":00:00";
                            newTimeSlot.Color = "green";
                            newTimeSlot.Num_Available_SA = a;
                            _timeSlotRepository.SaveTimeSlot(newTimeSlot);
                            
                        }
                    }
                }
            }
            #endregion 
            */

            
            foreach (var timeslot in timeslots)
            {
                CalendarDTO taList = new CalendarDTO();

                taList.id = timeslot.TimeSlotId;
                taList.start = timeslot.StartTime;
                taList.end = timeslot.EndTime;
                taList.backgroundColor = timeslot.Color;
                taList.title = timeslot.Title.ToString();
                taList.availableSAs = timeslot.Num_Available_SA;
                taList.allDay = false;
                taskList.Add(taList);
            }
            //return Json(cal.ToArray(), JsonRequestBehavior.AllowGet);
            return Json(taskList.ToArray(), JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult sendLeadId(string values)
        {
            LeadIdforcal = int.Parse(values) ;
            Session["leadid"] = LeadIdforcal ;
            var appts = _appointmentsRepository.GetAppointmentByLeadId(LeadIdforcal);
            var t = appts.Last();

            string tempDayofApt = t.DayOfAppointment.ToString();
            tempDayofApt = tempDayofApt.Remove(9);
            string starttemp = t.AppointmentDateFrom.ToString();
            starttemp = starttemp.Remove(0, 9);
            starttemp = tempDayofApt + " " + starttemp;
            string endtemp = t.AppointmentDateTo.ToString();
            endtemp = endtemp.Remove(0, 9);
            endtemp = tempDayofApt + " " + endtemp;
            DateTime tempstart = DateTime.Parse(starttemp);
            string z1 = tempstart.ToString("yyyy-MM-ddTHH':'mm':'ss");
            DateTime tempend = DateTime.Parse(endtemp);
            string z2 = tempend.ToString("yyyy-MM-ddTHH':'mm':'ss");

            Session["starttime"] = z1;
            Session["endtime"] = z2;


            return View() ;
        }
       
        public JsonResult updateTimeSlot(FormCollection recentTimeSlot )
        {
            // the whole code is dummy !!! will be deleted in future .
           // int leadid = (int)Session["leadid"];
            var appts = _appointmentsRepository.GetAppointmentByLeadId(11015);  // get the appointment by id
            var timeslots = _timeSlotRepository.TimeSlots;   // get the timeslots 
            int lasttimeslotid = 0 ;
            int lastavailableSA = 0;
            Boolean update = false;

            var t = appts.Last();

          //  DateTime a = t.AppointmentDateTo;
           // DateTime b = t.AppointmentDateFrom;
            DateTime c = t.DayOfAppointment;
            string test = c.ToString();

           
            string tempDayofApt = t.DayOfAppointment.ToString();
            tempDayofApt = tempDayofApt.Remove(9);
            string starttemp = t.AppointmentDateFrom.ToString();
            starttemp = starttemp.Remove(0, 9);
            starttemp = tempDayofApt + " " + starttemp;
            string endtemp = t.AppointmentDateTo.ToString();
            endtemp = endtemp.Remove(0, 9);
            endtemp = tempDayofApt + " "+ endtemp;
            DateTime tempstart = DateTime.Parse(starttemp); 
            string z1 = tempstart.ToString("yyyy-MM-ddTHH':'mm':'ss"); 
            DateTime tempend = DateTime.Parse(endtemp);
            string z2 = tempend.ToString("yyyy-MM-ddTHH':'mm':'ss");
           

            //get the timeslot id for the last appointment .
            foreach (var timeslot in timeslots)
            {
             if (string.Compare(timeslot.StartTime, z1) == 0)
                {
                     lasttimeslotid = timeslot.TimeSlotId;
                     lastavailableSA = timeslot.Num_Available_SA;
                     update = true;
                }
            }

            Threshold threshold = new Threshold();
            
            threshold = _ThresholdRepository.Thresholds.First();
         
            //IList<CalendarDTO> tasksList = new List<CalendarDTO>();
            TimeSlot updateTimeSlot = new TimeSlot();
            updateTimeSlot.TimeSlotId = int.Parse(recentTimeSlot[0]);
            //Title will be the number of available SA since it will be displayed

            //This is for test 

            //updateTimeSlot.Title = int.Parse(recentTimeSlot[8]);
            updateTimeSlot.Title = int.Parse(recentTimeSlot[6]);

            //...........

            //this is for test .. 
            //updateTimeSlot.StartTime = recentTimeSlot[4];
            //updateTimeSlot.EndTime = recentTimeSlot[5];
            // ...

            updateTimeSlot.StartTime = recentTimeSlot[2];
            updateTimeSlot.EndTime = recentTimeSlot[3];


            //updateTimeSlot.Color = recentTimeSlot[6];

            //this is for test .. 
            //updateTimeSlot.Num_Available_SA = int.Parse(recentTimeSlot[8]);
            //... 

            updateTimeSlot.Num_Available_SA = int.Parse(recentTimeSlot[6]);


            if (string.Compare(z1, updateTimeSlot.StartTime) == 0)
            {
                updateTimeSlot.Num_Available_SA = updateTimeSlot.Num_Available_SA + 1;
            //     _timeSlotRepository.SaveTimeSlot(updateTimeSlot);
            }
            else
            {
                updateTimeSlot.Num_Available_SA = updateTimeSlot.Num_Available_SA;

                // if its a different timeslot , then, change the previous timeslot .
                if (update)
                {
                    TimeSlot updateTimeSlotprev = new TimeSlot();

                    updateTimeSlotprev.TimeSlotId = lasttimeslotid;
                    updateTimeSlotprev.Num_Available_SA = lastavailableSA + 1;
                    updateTimeSlotprev.StartTime = z1;
                    updateTimeSlotprev.EndTime = z2;
                    updateTimeSlotprev.Title = updateTimeSlotprev.Num_Available_SA;
                    updateTimeSlotprev.All_Day = false;

                    if (updateTimeSlotprev.Num_Available_SA > threshold.Upper_Calendar)
                    {
                        updateTimeSlotprev.Color = "green";
                    }
                    else if (updateTimeSlotprev.Num_Available_SA >= threshold.Lower_Calendar && updateTimeSlotprev.Num_Available_SA <= threshold.Upper_Calendar)
                    {
                        updateTimeSlotprev.Color = "yellow";
                    }
                    else
                    {
                        updateTimeSlotprev.Color = "red";
                    }

                 //   _timeSlotRepository.SaveTimeSlot(updateTimeSlotprev);
                }
            }

            if (updateTimeSlot.Num_Available_SA > threshold.Upper_Calendar)
            {
                updateTimeSlot.Color = "green";
            }
            else if (updateTimeSlot.Num_Available_SA >= threshold.Lower_Calendar && updateTimeSlot.Num_Available_SA <= threshold.Upper_Calendar)
            {
                updateTimeSlot.Color = "yellow";
            }
            else
            {
                updateTimeSlot.Color = "red";
            }
            updateTimeSlot.All_Day = false;
          //  _timeSlotRepository.SaveTimeSlot(updateTimeSlot);

            return null;
            //return Json(updateTimeSlot, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCalendarEvent(CalendarEventViewModel calendar)
        {
            String calendarsting = calendar.ToString();
            return View();
        }
        public JsonResult GetEvents()
        {
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            int userId = user.UserId;
            var outList = new List<object>();
            IEnumerable<CalendarEvent> events = new List<CalendarEvent>();

            // code to store SA agent calender . Maya
            events = _eventsRepository.CalendarEvents.Where(row => row.Parent_User_Id == userId);            
           // events = _eventsRepository.GetEventsByUserId(userId);
            //....
            IList<CalendarEvent> cal = new List<CalendarEvent>();
            foreach (var calevent in events)
            {
                cal.Add(calevent);
            }
            return Json(cal.ToArray(), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetEventsPost()
        {
            var outList = new List<object>();
            IEnumerable<CalendarEvent> events = new List<CalendarEvent>();
            events = _eventsRepository.CalendarEvents;
            IList<CalendarEvent> cal = new List<CalendarEvent>();
            foreach (var calevent in events)
            {
                cal.Add(calevent);
            }
            return Json(cal.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateCalendar(FormCollection newCalendarEvent)
        {
            CalendarEventViewModel cevm = new CalendarEventViewModel();
            CalendarEvent events  =  new CalendarEvent();
            string starttemp = newCalendarEvent[0].Remove(0, 4);
            starttemp = starttemp.Remove(20);
            string endtemp = newCalendarEvent[1].Remove(0, 4);
            endtemp = endtemp.Remove(20);
            DateTime tempstart = DateTime.Parse(starttemp);
            events.start = tempstart.ToString("yyyy-MM-ddTHH':'mm':'ss'.'fff'-'05':'00");
            DateTime tempend = DateTime.Parse(endtemp);
            events.end = tempend.ToString("yyyy-MM-ddTHH':'mm':'ss'.'fff'-'05':'00");
            events.title = newCalendarEvent[3].ToString();
            events.street = newCalendarEvent[4].ToString();
            events.city = newCalendarEvent[5].ToString();
            events.state = newCalendarEvent[6].ToString();
            events.description = newCalendarEvent[9].ToString();
            
            if (newCalendarEvent[7] == "")
                events.zipcode = 12345;
            else
                events.zipcode = int.Parse(newCalendarEvent[7].ToString());
            //This guy will not take really big integers
            
            // code to store SA agent calender . Maya AA
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            events.Parent_User_Id = user.UserId;

            //..............//
            
            cevm.calendarEvent = events;
            _eventsRepository.SaveCalendarEvent(cevm.calendarEvent);

            // you have to save the calendar events in the timeslot repository as well
            // so that it shows up in the AA table .

            string z1;

            Threshold threshold = new Threshold();
            threshold = _ThresholdRepository.Thresholds.First();

            var taskList = new List<CalendarDTO>();
            IEnumerable<TimeSlot> timeslots = new List<TimeSlot>();
            timeslots = _timeSlotRepository.TimeSlots;
           
                    z1 = events.start;
                    z1 = z1.Remove(19);
                    foreach (var timeslot in timeslots)
                    {
                        if (string.Compare(z1, timeslot.StartTime) == 0)
                        {
                            timeslot.Num_Available_SA = timeslot.Num_Available_SA - 1;
                            timeslot.Title = timeslot.Num_Available_SA;

                            if (timeslot.Num_Available_SA > threshold.Upper_Calendar)
                            {
                                timeslot.Color = "green";
                            }
                            if (timeslot.Num_Available_SA >= threshold.Lower_Calendar && timeslot.Num_Available_SA <= threshold.Upper_Calendar)
                            {
                                timeslot.Color = "yellow";
                            }
                            if (timeslot.Num_Available_SA == 0)
                            {
                                timeslot.Color = "red";
                            }
                            _timeSlotRepository.SaveTimeSlot(timeslot);
                        }
                    }
                
            return null;
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateEventId(FormCollection updatedCalendarEvent)
        {
            CalendarEventViewModel cevm = new CalendarEventViewModel();
            CalendarEvent events = new CalendarEvent();
            string lead = updatedCalendarEvent[19].Substring(0, 5);
            int LeadId = Int32.Parse(lead);
            LeadId = Int32.Parse(lead);
            AppointmentSheetViewModel avm = new AppointmentSheetViewModel();
            AppointmentSheet asvm = new AppointmentSheet() { ParentLeadId = LeadId };
            string starttemp = updatedCalendarEvent[14].Remove(0, 4);
            starttemp = starttemp.Remove(20);
            string endtemp = updatedCalendarEvent[15].Remove(0, 4);
            endtemp = endtemp.Remove(20);
            DateTime tempstart = DateTime.Parse(starttemp);
            events.start = tempstart.ToString("yyyy-MM-ddTHH':'mm':'ss'.'fff'-'05':'00");
            DateTime tempend = DateTime.Parse(endtemp);
            events.end = tempend.ToString("yyyy-MM-ddTHH':'mm':'ss'.'fff'-'05':'00");
            events.id = Int32.Parse(updatedCalendarEvent[0]);
            events.title = updatedCalendarEvent[1].ToString();
            events.street = updatedCalendarEvent[6].ToString();
            events.city = updatedCalendarEvent[7].ToString();
            events.state = updatedCalendarEvent[8].ToString();
            events.Parent_User_Id = Int32.Parse(updatedCalendarEvent[17]);
            events.Parent_Appointment_Id = Int32.Parse(updatedCalendarEvent[18]);
            events.Appointment_Reference = updatedCalendarEvent[19].ToString();
            events.zipcode = Int32.Parse(updatedCalendarEvent[10]);
            events.appointment = Boolean.Parse(updatedCalendarEvent[4]);
            events.creator = Int32.Parse(updatedCalendarEvent[12]);
            asvm.Street = updatedCalendarEvent[6].ToString(); ;
            asvm.City = updatedCalendarEvent[7].ToString();
            asvm.State = updatedCalendarEvent[8].ToString();
            asvm.Event_Reference = updatedCalendarEvent[19].ToString();
            asvm.ZipCode = Int32.Parse(updatedCalendarEvent[10]);
            asvm.AssignedSalesAgent = Int32.Parse(updatedCalendarEvent[17]);
            asvm.AppointmentDateFrom = tempstart;
            asvm.AppointmentDateTo = tempend;
            asvm.AppointmentSheetId = events.Parent_Appointment_Id;
            string parentlead = events.Appointment_Reference.Substring(0, 5);
            asvm.ParentLeadId = Int32.Parse(parentlead);
            asvm.DayOfAppointment = tempstart;
            asvm.LastUpdated = Convert.ToDateTime(DateTime.Now);
            asvm.CreatorId = events.creator;
            avm.appointmentSheet = asvm;
            cevm.calendarEvent = events;
            _eventsRepository.SaveCalendarEvent(cevm.calendarEvent);
            _appointmentsRepository.SaveAppointmentSheetFromCalendar(avm.appointmentSheet);
            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult deleteEvent(FormCollection eventToBeDeleted)
        {
            CalendarEvent events = new CalendarEvent();
            events.id = Int32.Parse(eventToBeDeleted[0]);
            _eventsRepository.DeleteCalendarEvent(events);

            

            string z1;

            string starttemp = eventToBeDeleted[14].Remove(0, 4);
            starttemp = starttemp.Remove(20);
            string endtemp = eventToBeDeleted[15].Remove(0, 4);
            endtemp = endtemp.Remove(20);
            DateTime tempstart = DateTime.Parse(starttemp);
            events.start = tempstart.ToString("yyyy-MM-ddTHH':'mm':'ss");
            DateTime tempend = DateTime.Parse(endtemp);
            events.end = tempend.ToString("yyyy-MM-ddTHH':'mm':'ss");


            Threshold threshold = new Threshold();
            threshold = _ThresholdRepository.Thresholds.First();

            var taskList = new List<CalendarDTO>();
            IEnumerable<TimeSlot> timeslots = new List<TimeSlot>();
            timeslots = _timeSlotRepository.TimeSlots;

            

            z1 = events.start;
            foreach (var timeslot in timeslots)
            {
                if (string.Compare(z1, timeslot.StartTime) == 0)
                {
                    timeslot.Num_Available_SA = timeslot.Num_Available_SA + 1;
                    timeslot.Title = timeslot.Num_Available_SA;

                    if (timeslot.Num_Available_SA > threshold.Upper_Calendar)
                    {
                        timeslot.Color = "green";
                    }
                    if (timeslot.Num_Available_SA >= threshold.Lower_Calendar && timeslot.Num_Available_SA <= threshold.Upper_Calendar)
                    {
                        timeslot.Color = "yellow";
                    }
                    if (timeslot.Num_Available_SA == 0)
                    {
                        timeslot.Color = "red";
                    }
                    _timeSlotRepository.SaveTimeSlot(timeslot);
                }
            }
            return null;
        }


        public JsonResult GetManagerEvents()
        {
            var outList = new List<object>();
            IEnumerable<CalendarEvent> events = new List<CalendarEvent>();
            events = _eventsRepository.CalendarEvents;
            IList<CalendarEvent> cal = new List<CalendarEvent>();
            foreach (var calevent in events)
            {
                calevent.CalendarColor = _UserRepository.GetCalendarColorForUser(calevent.Parent_User_Id);
                calevent.EndTime = DateTime.Parse(calevent.end);
                calevent.StartTime = DateTime.Parse(calevent.start);
                if (calevent.Parent_User_Id != 0) 
                {
                    calevent.salesAgent = _UserRepository.GetUserById(calevent.Parent_User_Id).UserName;
                }
                else { calevent.salesAgent = ""; }
                    cal.Add(calevent);
            }
            
            return Json(cal.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost] 
        public ActionResult ts()
        {
            return null;
        }




        //public ActionResult Profile(int LeadId)
        //{
        //    ProfileViewModel pvm = new ProfileViewModel();
        //    // Please remember to include this part of the code whenever Click to Dial is implemented.
        //    Lead lead = new Lead();
        //    lead = _service.GetLeadByLeadId(LeadId);
        //    pvm.lead = lead;
            
        //    return View(pvm);
        //}
        private static DateTime convertfromunixtimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        private long ToUnixTimespan(DateTime date)
        {
            TimeSpan tspan = date.ToUniversalTime().Subtract(
            new DateTime(1970, 1, 1, 0, 0, 0));

            return (long)Math.Truncate(tspan.TotalSeconds);
        }
    }
}


