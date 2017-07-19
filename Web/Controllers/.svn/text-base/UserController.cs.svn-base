using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Text;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _UserRepository;
        private IZoneRepository _ZoneRepository;
        private IUserZoneRepository _UserZoneRepository;
        IPhoneUserRepository _PhoneUserRepos;
        private ITimeSlotRepository _timeSlotRepository;
        private IThresholdRepository _ThresholdRepository;
        private ICalendarEventRepository _eventsRepository;

        public UserController(IUserRepository UserRepository, IZoneRepository ZoneRepository, IUserZoneRepository UserZoneRepository, IPhoneUserRepository PhoneUserRepository, ITimeSlotRepository timeSlotRepository, IThresholdRepository Threshold, ICalendarEventRepository eventsRepository)
        {
            _UserRepository = UserRepository;
            _ZoneRepository = ZoneRepository;
            _UserZoneRepository = UserZoneRepository;
            _PhoneUserRepos = PhoneUserRepository;
            _timeSlotRepository = timeSlotRepository;
            _ThresholdRepository = Threshold;
            _eventsRepository = eventsRepository;
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            Web.ViewModel.UserCreateViewModel uc = new ViewModel.UserCreateViewModel();
            var username = HttpContext.User.Identity.Name;

            uc.user = _UserRepository.GetUserByUsername(username);
            if (uc.user.AssignedRoleId == 3 || uc.user.AssignedRoleId == 4)
                RedirectToAction("Index2");
            var results = _UserRepository.GetAllUsers();
            uc.users = results;
            return View(uc);            
        }

        public ActionResult Index2()
        {
            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();

            var username = HttpContext.User.Identity.Name;

            uc.user = _UserRepository.GetUserByUsername(username);

            return RedirectToActionPermanent("Edit", uc.user.UserId);
            //return View("Edit", uc);
       }

        public ActionResult Create()
        {
            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
            var username = HttpContext.User.Identity.Name;

            uc.displayuser = _UserRepository.GetUserByUsername(username);
            uc.ZoneList = _ZoneRepository.Zones.ToList();
                       
            return View(uc);
        }

        [AcceptVerbs(HttpVerbs.Post)] 
        public ActionResult Create(Web.ViewModel.UserCreateViewModel uc, string values)
        {
            // In addition to updating the User table , also update the timeslot table if
            // the assigned roleid is 3 (for SA) . If the assigned roleid is 3 , then the number 
            // of available SA's should go up by one for all the timeslots .
            // You may right it in a static method and call the update code , so that if a user is 
            // deleted , then the number of available SA's go down by 1 .
            int a;
            var newUser = uc.user;
            PhoneUser newPhoneUser = new PhoneUser();

            IEnumerable<User> users = new List<User>();
            users = _UserRepository.GetAllUsersByRole(3);
             a = users.Count();

             

            if (newUser.selectedRole == "Administrator")
                newUser.AssignedRoleId = 1;
            if (newUser.selectedRole == "Manager")
                newUser.AssignedRoleId = 2;
            if (newUser.selectedRole == "SA")
                newUser.AssignedRoleId = 3;
            if (newUser.selectedRole == "AA")
                newUser.AssignedRoleId = 4;
            if (newUser.selectedRole == "Customer Service Agent")
                newUser.AssignedRoleId = 5;

            var temp = _UserRepository.GetUserByUsername(newUser.UserName);

            if (temp != null)
                return Json(new { msg = "Not Null. User exists." });

            newUser.Address2 = "";
            newUser.newPassword = "trinity123";                 // Default first time password
            newUser.IsActive = true;
            _UserRepository.SaveUser(newUser);

            if (newUser.AssignedRoleId == 3)
            {
                // add one available SA in the timeslot table .
                var timeslots = _timeSlotRepository.TimeSlots;    // get the timeslots 

                Threshold threshold = new Threshold();
                threshold = _ThresholdRepository.Thresholds.First();

                foreach (var timeslot in timeslots)
                {
                    timeslot.Num_Available_SA = timeslot.Num_Available_SA + 1;
                    timeslot.Title = timeslot.Num_Available_SA;
                    if (timeslot.Num_Available_SA > threshold.Upper_Calendar)
                    {
                        timeslot.Color = "green";
                    }
                    else if (timeslot.Num_Available_SA >= threshold.Lower_Calendar && timeslot.Num_Available_SA <= threshold.Upper_Calendar)
                    {
                        timeslot.Color = "yellow";
                    }
                    else
                    {
                        timeslot.Color = "red";
                    }

                    _timeSlotRepository.SaveTimeSlot(timeslot);
                }

            }

            var tempUser = _UserRepository.GetUserByUsername(uc.user.UserName);
            newPhoneUser.CRMUserId = tempUser.UserId;
            newPhoneUser.Email = uc.user.EmailOne;
            newPhoneUser.Extension = uc.extensionNum;
            newPhoneUser.FirstName = uc.user.FirstName;
            newPhoneUser.MiddleName = uc.user.MiddleName;
            newPhoneUser.LastName = uc.user.LastName;
            newPhoneUser.AccountId = 1155;

            _PhoneUserRepos.SavePhoneUser(newPhoneUser);

            if (values != "")
            {
                var ids = values.Split(',');

                UserZone uz = new UserZone();

                var usernew = _UserRepository.GetUserByUsername(uc.user.UserName);

                uz.UserID = usernew.UserId;

                for (int i = 0; i < ids.Length; i++)
                {
                    uz.ZoneId = Convert.ToInt32(ids[i]);
                    _UserZoneRepository.SaveUserZone(uz);
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index") });

        }

        public ActionResult Edit(Int32 id)
        {
            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();

            var username = HttpContext.User.Identity.Name;
            var phoneuser = _PhoneUserRepos.GetPhoneUser(id);
            uc.displayuser = _UserRepository.GetUserByUsername(username);

            uc.extensionNum = phoneuser.Extension;
                uc.user = _UserRepository.GetUserById(id);

                if (uc.user.AssignedRoleId == 1)
                    uc.user.selectedRole = "Administrator";
                if (uc.user.AssignedRoleId == 2)
                    uc.user.selectedRole = "Manager";
                if (uc.user.AssignedRoleId == 3)
                    uc.user.selectedRole = "SA";
                if (uc.user.AssignedRoleId == 4)
                    uc.user.selectedRole = "AA";
                if (uc.user.AssignedRoleId == 5)
                    uc.user.selectedRole = "Customer Service Agent";
            
            uc.ZoneList = _ZoneRepository.Zones.ToList();
            var uzones = _UserZoneRepository.GetAllZonesByUser(id).ToList();

            IList<int> uzoneslist = new List<int>();

            foreach( var singleZone in uzones)
            {
                uzoneslist.Add(singleZone.ZoneId);
            }

            uc.AssignedZoneList = uzoneslist;
            uc.eId = uc.user.FirstName.Substring(0,1) + uc.user.LastName.Substring(0,1) + "1000" + uc.user.UserId.ToString();

            return View(uc);
        }

        [AcceptVerbs(HttpVerbs.Post)] 
        public ActionResult Edit(Web.ViewModel.UserCreateViewModel uc, string values, string vals)
        {
            var newUser = uc.user;
            //newUser.oldPassword = EncryptPassword(newUser.oldPassword);
            string newPswd;
            bool check = false;

            if (newUser.oldPassword != null && newUser.changePassword1 != null && newUser.changePassword2 != null)
            {
                newPswd = SavePassword(vals, uc.user.UserId);
                if (newPswd == "")
                    check = false;
                else
                {
                    check = true;
                    newUser.newPassword = newPswd;
                }
            }

            PhoneUser newPhoneUser = new PhoneUser();

            newPhoneUser.CRMUserId = uc.user.UserId;
            newPhoneUser.Email = uc.user.EmailOne;
            newPhoneUser.Extension = uc.extensionNum;
            newPhoneUser.FirstName = uc.user.FirstName;
            newPhoneUser.MiddleName = uc.user.MiddleName;
            newPhoneUser.LastName = uc.user.LastName;
            newPhoneUser.AccountId = 1155;
            newPhoneUser.PhoneUserId = 1;
            _PhoneUserRepos.SavePhoneUser(newPhoneUser);

            newUser.firstTime = false;
            _UserRepository.SaveUser(newUser);

            if (values != "")
            {
                var ids = values.Split(',');

                UserZone uz = new UserZone();

                uz.UserID = uc.user.UserId;

                _UserZoneRepository.DeleteUserZoneByUserID(uc.user.UserId);


                for (int i = 0; i < ids.Length; i++)
                {
                    uz.ZoneId = Convert.ToInt32(ids[i]);
                    _UserZoneRepository.SaveUserZone(uz);
                }
            }
            if (newUser.AssignedRoleId == 1 || newUser.AssignedRoleId == 2)
            {
                return Json(new { redirectToUrl = Url.Action("Index"), data = check });
            }
            else
            {
                return Json(new { redirectToUrl = Url.Action("Edit", new { id = newUser.UserId }), data = check });
               // return RedirectToAction("Edit", "User", new { id = newUser.UserId });
            }

        }

        public ActionResult Details(int id)
        {
            //var result = _UserRepository.Users.ToList()[id - 1];

            var result = _UserRepository.GetUserById(id);

            id = 0;
            return View(result);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserNameCheck(string values)
        {

            var uName = values;

            var currentUser = _UserRepository.GetUserByUsername(uName);
            
            if (currentUser != null)
            {
                return Json(new { data = true });
            }

            return Json(new { data = false });
        }

        public string SavePassword(string values, int id)
        {

            var passwords = values.Split(',');

            var currentUser = _UserRepository.GetUserById(id);

            var encryptedPassword = EncryptPassword(passwords[0]);
            
            if (currentUser.Password.Equals(encryptedPassword))
            {
                if (passwords[1].Equals(passwords[2]))
                {
                    return EncryptPassword(passwords[2]);
                }
            }

            return "";
        }

        public ActionResult Delete(int id)
        {
            var result = _UserRepository.GetUserById(id);
            //var result = _UserRepository.Users.ToList()[id - 1];

            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(int id, User u)
        {
            Web.ViewModel.UserCreateViewModel uc = new Web.ViewModel.UserCreateViewModel();
            uc.user = _UserRepository.GetUserById(id);

            if (uc.user.AssignedRoleId == 3)
            {
                // if its a SA , then delete the number of available SA from the timeslot table .
                 
                    var timeslots = _timeSlotRepository.TimeSlots;    // get the timeslots 

                    Threshold threshold = new Threshold();
                    threshold = _ThresholdRepository.Thresholds.First();

                    foreach (var timeslot in timeslots)
                    {
                        timeslot.Num_Available_SA = timeslot.Num_Available_SA - 1;
                        timeslot.Title = timeslot.Num_Available_SA;
                        if (timeslot.Num_Available_SA > threshold.Upper_Calendar)
                        {
                            timeslot.Color = "green";
                        }
                        else if (timeslot.Num_Available_SA >= threshold.Lower_Calendar && timeslot.Num_Available_SA <= threshold.Upper_Calendar)
                        {
                            timeslot.Color = "yellow";
                        }
                        else
                        {
                            timeslot.Color = "red";
                        }
                        _timeSlotRepository.SaveTimeSlot(timeslot);
                    }

                // whenever a SA is deleted , the calendar events of the SA should also be deleted .

                 IEnumerable<CalendarEvent> events = new List<CalendarEvent>();
                 events = _eventsRepository.GetEventsByUserId(id);

                 foreach (var e in events)
                 {
                     _eventsRepository.DeleteCalendarEvent(e);
                 }

                }

            _UserRepository.DeleteUser(id);
            
            return View("Deleted");
        }

        public static string EncryptPassword(string password)
        {
            if (!String.IsNullOrEmpty(password))
            {
                byte[] pwdBytes = Encoding.GetEncoding(1252).GetBytes(password);
                byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(pwdBytes);
                return Encoding.GetEncoding(1252).GetString(hashBytes);
            }
            else
            {
                return "";
            }
        }
    }

}
