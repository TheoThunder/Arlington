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


namespace Web.Controllers
{
    public class AppointmentQueueController : Controller
    {

        public ILeadAccessRepository _LeadRepository;
        public ILeadRepository _LeadRepos;
        private IUserRepository _UserRepository;
        public IGenericUsageRepositoryInterface _UsageRepository;
        public IAppointmentSheet _AppointmentRepos;
        ICalendarEventRepository _EventsRepository;

        public AppointmentQueueController(ILeadAccessRepository LeadRepository, ILeadRepository LRepository, IUserRepository UserRepository, IGenericUsageRepositoryInterface ToGetLeadByCardType, IAppointmentSheet GetAppointments, ICalendarEventRepository EventsRepository)
        {
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepository;
            _LeadRepos = LRepository;
            _UsageRepository = ToGetLeadByCardType;
            _AppointmentRepos = GetAppointments;
            _EventsRepository = EventsRepository;
        }


        //
        // GET: /ImportLead/

        public ActionResult Index()
        {
            Lead lead = new Lead();
            Web.ViewModel.AppointmentSheetViewModel asvm = new Web.ViewModel.AppointmentSheetViewModel();

            IList<AppointmentSheet> apmtq = new List<AppointmentSheet>();
            var username = HttpContext.User.Identity.Name;

            var user = _UserRepository.GetUserByUsername(username);

            asvm.user = user;
            //Get all unassigned appointments
            var appointments = _UsageRepository.AppointmentQueue(0, false);
            

            foreach (var appointmentq in appointments)
            {
				if (appointmentq.Score == "Good")
				{
					//do nothing
				}
				else if (appointmentq.Score == "Bad")
				{
					//do nothing
				}
				else
				{
					// To get the assigned username 
					Lead parentLead = _LeadRepos.LeadByLeadID(appointmentq.ParentLeadId);
					if (lead.LeadId != parentLead.LeadId)
					{
						lead = parentLead;
						User newUser = _UserRepository.GetUserById(parentLead.AssignedAAUserId);
						if (newUser == null)
						{
							// Should not this condition
							appointmentq.AssignedUser.UserName = "Not Assigned";
							appointmentq.companyname = parentLead.CompanyName;
						}
						else
						{
							appointmentq.AssignedUser = newUser;
							appointmentq.companyname = parentLead.CompanyName;
						}
						apmtq.Add(appointmentq);
					}
				}
            }
            asvm.AppointmentQueue = apmtq;

            // Drop down showing list of SA's
            var saUsersResult = _UserRepository.GetAllUsersByRole(3);
          
            asvm.AAUsersDropdown = saUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.UserId.ToString()
            });
            return View(asvm);
        }


        public IEnumerable<Data.Domain.AppointmentSheet> GetAppointments()
        {
            var results = _AppointmentRepos.AppointmentSheets;
           
            return results;
        }

        //
        // GET: /ImportLead/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ImportLead/Create
        public ActionResult Profile(int LeadId)
        {
            return View(GetLeadByLeadId(LeadId));
        }

        public ActionResult Reschedule(System.Int32 AppointmentSheetId)
        {
            var appointmentReschedule = _AppointmentRepos.GetAppointmentByAppointmentId(AppointmentSheetId);
            appointmentReschedule.Reschedule = true;
            _AppointmentRepos.SaveAppointmentSheet(appointmentReschedule);
            return Redirect("/AppointmentQueue/Index");
        }

        [HttpPost]
        public ActionResult RescheduleMany(string text, string id)
        {

            string[] ids = id.Split(',');
            // IList<Lead> leads;
            foreach (string appointmentid in ids)
            {
                int aid = int.Parse(appointmentid);
                var appointment = _AppointmentRepos.GetAppointmentByAppointmentId(aid);
                appointment.Reschedule = true;
                _AppointmentRepos.SaveAppointmentSheet(appointment);

            }

            return Redirect("/AppointmentQueue/Index");
        }

        [HttpPost]
        public ActionResult AssignAppointment(string text, string id)
        {
            string[] ids = id.Split(',');
            string[] names = text.Split(',');

            AppointmentSheet appointment;
            
            User user = _UsageRepository.GetUserIDByName(names[0], names[1]);

            foreach (string appointmentid in ids)
            {
                int aid = int.Parse(appointmentid);
                
                appointment = _AppointmentRepos.GetAppointmentByAppointmentId(aid);
                Lead lead = _LeadRepos.LeadByLeadID(appointment.ParentLeadId);
                _UsageRepository.SaveAssignedSALeads(lead, user.UserId);

                _UsageRepository.SaveAssignedAppointments(appointment, user.UserId);
                
                // need ref id to appoint parent_user_integer
             
                string EventReferencingId = appointment.Event_Reference;
                Web.ViewModel.CalendarEventViewModel cevm = new Web.ViewModel.CalendarEventViewModel();
                CalendarEvent events = new CalendarEvent();
                events = _EventsRepository.GetEventByAppointmentID(EventReferencingId);
                events.Parent_Appointment_Id = appointment.AppointmentSheetId;
                events.Parent_User_Id = user.UserId;
                events.appointment = true;
                events.street = appointment.Street;
                events.city = appointment.City;
                events.state = appointment.State;
                events.zipcode = appointment.ZipCode;
                events.description = "";
                events.creator = appointment.CreatorId;
                events.assigned = appointment.AssignedSalesAgent;
                cevm.calendarEvent = events;
                _EventsRepository.SaveCalendarEvent(cevm.calendarEvent);
            }


            //return RedirectToAction("Index", "ImportLead");
            return Redirect("/AppointmentQueue/Index");
        }
        public Data.Domain.Lead GetLeadByLeadId(int leadId)
        {
            return _LeadRepos.LeadByLeadID(leadId);
        }

        public ActionResult Suppress(int LeadId)
        {
            var leadSuppress = GetLeadByLeadId(LeadId);
            leadSuppress.Suppressed = true;
            SaveBusinessInformation(leadSuppress);
            return RedirectToAction("Index");
        }

        private void SaveBusinessInformation(Data.Domain.Lead leadSuppress)
        {
            _LeadRepos.SaveLead(leadSuppress);
        }


        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ImportLead/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ImportLead/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ImportLead/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ImportLead/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ImportLead/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
