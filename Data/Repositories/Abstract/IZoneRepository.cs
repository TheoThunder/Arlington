using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IZoneRepository
    {
        IQueryable<Zone> Zones { get; }
        
        void SaveZone(Zone zone);
        void DeleteZone(Zone zone);
        int GetZoneByZipcode(string zipcode);
        IEnumerable<ZipCodes> GetZipcodesByZone(int zone_id);
        void SaveZipCode(int zoneid, int zipcode);
        void DeleteZipCode(int zipcode);
        ZipCodes GetZipCode(int zipcodeid);
    }
}
