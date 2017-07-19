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
    public class RescheduleController : Controller
    {
        
         public ILeadAccessRepository _LeadRepository;
        public ILeadRepository _LeadRepos;
        private IUserRepository _UserRepository;
        public IGenericUsageRepositoryInterface _UsageRepository;
        public IAppointmentSheet _AppointmentRepos;

        public RescheduleController(ILeadAccessRepository LeadRepository, ILeadRepository LRepository, IUserRepository UserRepository, IGenericUsageRepositoryInterface ToGetLeadByCardType, IAppointmentSheet GetAppointments)
        {
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepository;
            _LeadRepos = LRepository;
            _UsageRepository = ToGetLeadByCardType;
            _AppointmentRepos = GetAppointments;
        }


      
        public ActionResult Index(string text, string id)
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

    }
}
