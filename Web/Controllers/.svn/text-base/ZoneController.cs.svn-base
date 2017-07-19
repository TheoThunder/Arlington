using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using Data.Domain;
using Web.ViewModel;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Web.Controllers
{
	public class ZoneController : Controller
	{
		public IZoneRepository _ZoneRepository;
		public IEquipmentRepository _EquipmentRepository;
		public IUserRepository _UserRepository;
		public ILeadRepository _LeadRepos;
		public IGenericUsageRepositoryInterface _ChangeZip;
		public IThresholdRepository _Threshold;
		public ZoneController(IThresholdRepository ThresholdRepos, IZoneRepository ZoneRepository, IEquipmentRepository EquipmentRepository, IUserRepository UserRepos, ILeadRepository LeadRepos, IGenericUsageRepositoryInterface GenericRepos)
		{
			_ZoneRepository = ZoneRepository;
			_EquipmentRepository = EquipmentRepository;
			_UserRepository = UserRepos;
			_LeadRepos = LeadRepos;
			_ChangeZip = GenericRepos;
			_Threshold = ThresholdRepos;
		}

		//
		// GET: /Zone/
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			ZoneManagementViewModel vm = new ZoneManagementViewModel();

			// For Username 
			var username = HttpContext.User.Identity.Name;
			vm.user = _UserRepository.GetUserByUsername(username);

			/****FOR THE ZONE NUMBERS DROP DOWN****/
			vm.ZoneList = _ZoneRepository.Zones.ToList();

			/****FOR THE EQUIPMENT TYPE TABLE*****/
			vm.Equipment = _EquipmentRepository.Equipments.ToList();

			/*****FOR THE ZIP CODES DROP DOWN*****/
			var zipCode = new List<int>();

			/******* For Threshold *****************/
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;

			Threshold threshold = new Threshold();
			if (tlist.Count() != 0)
			{
				threshold = tlist.First();
			}
			vm.threshold = threshold;

			foreach (var results in _ZoneRepository.Zones)
			{
				var zipcodeslist = _ZoneRepository.GetZipcodesByZone(results.ZoneNumber);
				foreach (var zip in zipcodeslist)
				{
					zipCode.Add(zip.ZipCode);

				}
				break;
			}
			vm.ZipcodeList = zipCode;
			return View(vm);


		}

		[AcceptVerbs(HttpVerbs.Post)]
		public string Index(Zone zone)
		{
			var zoneList = new List<int>();
			_ZoneRepository.SaveZone(zone);

			foreach (var result in _ZoneRepository.Zones)
			{
				if ((result.ZoneNumber == 4))
					return "got it";
			}
			return "fad";

		}

		/// <summary>
		/// Takes in a new Zone Number, saves new Zone to Repository, returns in JSON Format the Zone Number and it's ZoneId
		/// </summary>
		/// <param name="id">new Zone Number</param>
		/// <returns></returns>
		public ActionResult addZone(string text)
		{
			Zone zone = new Zone();
			var zoneInt = Convert.ToInt32(text);
			try
			{
				var checkExistingZone = _ZoneRepository.Zones.Single(row => row.ZoneNumber == zoneInt);
			}
			catch
			{
				zone.ZoneNumber = zoneInt;

				_ZoneRepository.SaveZone(zone);
				var newZone = _ZoneRepository.Zones.Single(row => row.ZoneNumber == zoneInt);

				StringBuilder sb = new StringBuilder();
				StringWriter sw = new StringWriter(sb);

				using (JsonWriter w = new JsonTextWriter(sw))
				{
					w.WriteStartObject();
					w.WritePropertyName("ZoneId");
					w.WriteValue(newZone.ZoneId);
					w.WritePropertyName("ZoneNumber");
					w.WriteValue(newZone.ZoneNumber);
					w.WriteEndObject();
				}

				return Content(sb.ToString(), "application/json");
			}

			return Content("false");



		}



		/// <summary>
		/// Takes in a ZoneId, removes related Zone from Repository, returns success or failure
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult removeZone(string id)
		{
			var deleteZone = _ZoneRepository.Zones.Single(row => row.ZoneId == Convert.ToInt32(id));
			var ZipCodesCovered = _ZoneRepository.GetZipcodesByZone(deleteZone.ZoneNumber);
			foreach (var zipcode in ZipCodesCovered)
			{
				_ZoneRepository.DeleteZipCode(zipcode.ZipCode);
			}
			_ZoneRepository.DeleteZone(deleteZone);
			return RedirectPermanent("Index");

		}
		/// <summary>
		/// Takes in a ZoneId, gather the zipcode list of the related Zone from Repository, returns success or failure
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult gatherZipcodes(string id)
		{
			var findZone = _ZoneRepository.Zones.Single(row => row.ZoneId == Convert.ToInt32(id));
			var zipCode = new List<int>();
			var ZipCodesCovered = _ZoneRepository.GetZipcodesByZone(findZone.ZoneNumber);
			//foreach (var zip in ZipCodesCovered)
			//// foreach (var zip in results.ZipCodesCovered)
			//{
			//    zipCode.Add(zip.ZipCode);

			//}

			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter w = new JsonTextWriter(sw))
			{
				w.WriteStartObject();
				w.WritePropertyName("ZipCodesCovered");

				w.WriteStartArray();
				foreach (var zip in ZipCodesCovered)
				{
					w.WriteValue(zip.ZipCode);
				}
				w.WriteEnd();
				w.WritePropertyName("ZoneId");
				w.WriteValue(findZone.ZoneId);

				w.WriteEndObject();
			}

			return Content(sb.ToString(), "application/json");

		}
		/// <summary>
		/// Takes in a new Zone Number, saves new Zone to Repository, returns in JSON Format the Zone Number and it's ZoneId
		/// </summary>
		/// <param name="id">new Zone Number</param>
		/// <returns></returns>
		public ActionResult addZipcode(string text, string id)
		{
			var checkExixtingZip = _ZoneRepository.GetZoneByZipcode(text);

			if (checkExixtingZip == 0)
			{

				var findZone = _ZoneRepository.Zones.Single(row => row.ZoneId.ToString() == id);
				findZone.ZipCodesCovered.Add(text);
				int zipcode = Convert.ToInt32(text);
				int zonenumber = Convert.ToInt32(id);

				_ZoneRepository.SaveZipCode(findZone.ZoneNumber, zipcode);
				var ZipCodesCovered = _ZoneRepository.GetZipcodesByZone(findZone.ZoneNumber);
				StringBuilder sb = new StringBuilder();
				StringWriter sw = new StringWriter(sb);

				using (JsonWriter w = new JsonTextWriter(sw))
				{
					w.WriteStartObject();
					w.WritePropertyName("ZipCodesCovered");

					w.WriteStartArray();
					foreach (var zip in ZipCodesCovered)
					{
						w.WriteValue(zip.ZipCode);
					}
					w.WriteEnd();
					w.WritePropertyName("ZoneId");
					w.WriteValue(findZone.ZoneId);

					w.WriteEndObject();
				}
				//ChangeExistingLeadsZoneNumber(zipcode, findZone.ZoneNumber);
				return Content(sb.ToString(), "application/json");
			}
			else
			{
				return Content("false");           //zipcode already exist
			}
		}

		private void ChangeExistingLeadsZoneNumber(int zip, int zone)
		{
			IEnumerable<Lead> Leads = new List<Lead>();
			Leads = _LeadRepos.Leads;
			ChangeExistingLeadsZoneNumber(zip, zone, Leads);

		}

		public void ChangeExistingLeadsZoneNumber(int zip, int zone, IEnumerable<Lead> Leads)
		{


			foreach (var lead in Leads)
			{

				//var tranformedzip = lead.ZipCode.Remove(6);
				string tranformedzip = lead.ZipCode;

				//int zipcode = Convert.ToInt32(tranformedzip[0].ToString());
				int flag = tranformedzip.IndexOf(zip.ToString(), 0, 5);
				if (lead.ZoneNumber == 0 && flag != -1)
				{
					lead.ZoneNumber = zone;
					_LeadRepos.SaveLead(lead);
				}
				tranformedzip = null;

			}

		}
		public ActionResult removeZipcode(string id)
		{
			// var zipcodeid = Convert.ToInt32(text);
			var zoneid = Convert.ToInt32(id);

			_ZoneRepository.DeleteZipCode(zoneid);

			return Redirect("Index");

		}
		public ActionResult equipmentAdd(string text, string id, string value)
		{
			try
			{
				var checkEquipment = _EquipmentRepository.Equipments.Single(row => row.Name == text);
			}
			catch
			{
				Equipment equipment = new Equipment();
				equipment.Name = text;
				if (value == "1")
					equipment.Type = "Terminal";
				if (value == "2")
					equipment.Type = "Pinpad";
				if (value == "3")
					equipment.Type = "Check";

				if (id == "true")
				{
					equipment.Active = true;
				}
				else
				{
					equipment.Active = false;
				}

				_EquipmentRepository.SaveEquipment(equipment);

				//return Redirect("Index");
				return Content("true");
			}
			return Content("false");

		}
		public ActionResult Edit(string id, string value)
		{
			var findEquipmentType = _EquipmentRepository.Equipments.Single(row => row.EquipmentId == Convert.ToInt32(id));
			findEquipmentType.Type = value;
			_EquipmentRepository.SaveEquipment(findEquipmentType);
			return Content(value);
		}
		public ActionResult ChkEdit(int[] id)
		{
			foreach (var temp in _EquipmentRepository.Equipments)
			{
				temp.Active = false;
				_EquipmentRepository.SaveEquipment(temp);
			}
			if (id == null)
			{
				return Redirect("Index");
			}
			else
			{
				for (int i = 0; i < id.Length; i++)
				{
					var findEquipmentType = _EquipmentRepository.Equipments.Single(row => row.EquipmentId == id[i]);
					findEquipmentType.Active = true;
					_EquipmentRepository.SaveEquipment(findEquipmentType);

				}
				return Redirect("Index");
			}
		}
		public ActionResult Delete(int equipmentId)
		{

			var findEquipmentType = _EquipmentRepository.Equipments.Single(row => row.EquipmentId == equipmentId);
			_EquipmentRepository.DeleteEquipment(findEquipmentType);
			return Redirect("Index");

		}


		public ActionResult ChangeUpperCalendar(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.Lower_Calendar < value)
			{
				thresholdvalue.Upper_Calendar = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}

		public ActionResult ChangeLowerCalendar(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.Upper_Calendar > value)
			{
				thresholdvalue.Lower_Calendar = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}

		// Work efficiency change upper limit
		public ActionResult ChangeUpperDashboardSA(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.WE_SA_Lower_Dashboard < value)
			{
				thresholdvalue.WE_SA_Upper_Dashboard = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}
		// Work efficiency change lower limit
		public ActionResult ChangeLowerDashboardSA(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.WE_SA_Upper_Dashboard > value)
			{
				thresholdvalue.WE_SA_Lower_Dashboard = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}

		// Work efficiency change upper limit
		public ActionResult ChangeUpperDashboardGA(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.WE_GA_Lower_Dashboard < value)
			{
				thresholdvalue.WE_GA_Upper_Dashboard = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}
		// Work efficiency change lower limit
		public ActionResult ChangeLowerDashboardGA(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.WE_GA_Upper_Dashboard > value)
			{
				thresholdvalue.WE_GA_Lower_Dashboard = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}

		// Change number of calls threshold upper
		public ActionResult ChangeUpperDashboardNC(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.NC_Lower_Dashboard < value)
			{
				thresholdvalue.NC_Upper_Dashboard = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}
		// Change number of calls threshold lower
		public ActionResult ChangeLowerDashboardNC(int value)
		{
			Data.Domain.Threshold thresholdvalue = new Threshold();
			IEnumerable<Threshold> tlist = new List<Threshold>();
			tlist = _Threshold.Thresholds;
			if (tlist.Count() != 0)
			{
				thresholdvalue = tlist.First();
			}
			if (thresholdvalue.NC_Upper_Dashboard > value)
			{
				thresholdvalue.NC_Lower_Dashboard = value;
				_Threshold.SaveThreshold(thresholdvalue);
				string val = value.ToString();
				return Content(val);
			}
			else
			{
				return Content("false");
			}
		}
	}
}