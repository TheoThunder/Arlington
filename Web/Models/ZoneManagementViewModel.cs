using System.ComponentModel;
using System.Collections.Generic;
using Data.Domain;
namespace Web.ViewModel
{
    public class ZoneManagementViewModel
    {
        [DisplayName("Zone")]
        public Zone zone { get; set; }
        public User user { get; set; }
        public IEnumerable<Zone> ZoneList { get; set; }
        public string SelectedZoneNumber { get; set; }
        public IEnumerable<int> ZipcodeList { get; set; }
        public ZipCodes SelectedZipCodes { get; set; }
        public IEnumerable<Equipment> Equipment { get; set; }
        public string SelectedEquipment { get; set; }
        //public int ZoneNumber { get; set; }

        public Threshold threshold { get; set; }
    }
  
    
}