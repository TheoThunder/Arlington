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
    public class ImportLeadController : Controller
    {

        public ILeadAccessRepository _LeadRepository;
        public ILeadRepository _LeadRepos;
        private IUserRepository _UserRepository;
        public IGenericUsageRepositoryInterface _IncodeQueryRepos;

        public ImportLeadController(IGenericUsageRepositoryInterface IncodeQueryRepos, ILeadAccessRepository LeadRepository, ILeadRepository LRepository, IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
            _LeadRepository = LeadRepository;
            _LeadRepos = LRepository;
            _IncodeQueryRepos = IncodeQueryRepos;
        }


        //
        // GET: /ImportLead/

     

        public ActionResult Index()
        {
            var username = HttpContext.User.Identity.Name;
            
                Web.ViewModel.ImportLeadViewModel il = new Web.ViewModel.ImportLeadViewModel();
            IList<User> dropdownUsers = new List<User>();

                var users = _UserRepository.GetUserByUsername(username);
                il.user = users;
                    var unassignedresults = _IncodeQueryRepos.UnAssignedleads();
                    var ignoredresults = _IncodeQueryRepos.Ignoredleads(); 
                    il.Unassignedleads = unassignedresults;
                    il.Ignoredleads = ignoredresults;

                    dropdownUsers = _IncodeQueryRepos.GetAllSalesAndAppointmentAgents();

            il.AAUsersDropdown = dropdownUsers.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.UserId.ToString()
            });
            return View(il);
            
        }

        //Post the file.

              [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                // TODO: Decide exactly what to do with file.
                // extract only the filename
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                //append timestamp to filename
                string ftime = string.Format("{0:yyyyMMddHHmmss}", System.DateTime.Now);
                fileName = fileName + "_" + ftime + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(INF.ConfigReader.UploadPath), fileName);
                file.SaveAs(path);


                _LeadRepository.SaveLeadAccessRecord(path);
            }
            else
            {
                string errordetails = "Please select a file to upload";
                return RedirectToAction("Error", "Home", new { id = errordetails });
                
            }

           
           
        // redirect back to the index action to show the form once again
        return RedirectToAction("Index");        
        }

       
    
      
        //
        // GET: /ImportLead/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

   
        

        //
        // GET: /ImportLead/Create

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
