
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;
namespace Data.Repositories.Static
{
    public class StaticZoneRepository : IZoneRepository
    {
        private static IList<Zone> fakeZone = new List<Zone>();
        private static int zoneCount = 1;

        public IQueryable<Domain.Zone> Zones
        {
            get { return fakeZone.AsQueryable(); }
        }

        public void SaveZone(Zone zone)
        {
            //If new zone, add to the list
            if (zone.ZoneId == 0)
            {
                zone.ZoneId = zoneCount;
                zoneCount++;
                fakeZone.Add(zone);
            }
            else if (fakeZone.Count(row => row.ZoneId == zone.ZoneId) == 1)
            {
                //updating deleting old and adding new
                DeleteZone(zone);
                fakeZone.Add(zone);
            }

        }

        public void DeleteZone(Zone zone)
        {

            var temp = fakeZone.ToList();
            temp.RemoveAll(row => row.ZoneId == zone.ZoneId);
            fakeZone = temp;
        }
     
        public int GetZoneByZipcode(string zipcode)
        {

            for (int i = 0; i < fakeZone.Count; i++)
                for (int j = 0; j < fakeZone.ToList()[i].ZipCodesCovered.Count; j++)
                    if (fakeZone.ToList()[i].ZipCodesCovered.ToList()[j] == zipcode)
                    {
                        return fakeZone.ToList()[i].ZoneNumber;
                    }
                    
            return -1;// if the zone is not in the list
                
        }
        public IEnumerable<ZipCodes> GetZipcodesByZone(int zone_id)
        {
            IList<ZipCodes> allZips = new List<ZipCodes>();
           
            return allZips;
        }
        public void SaveZipCode(int zoneid, int zipcode)
        {
            throw new NotImplementedException();
        }


        public void DeleteZipCode(int zipcode)
        {
            throw new NotImplementedException();
        }
        public ZipCodes GetZipCode(int zipcodeid)
        {
            throw new NotImplementedException();
        }
        //created for Unit Testing
        public void ClearRepo()
        {
            fakeZone.Clear();
        }
    }
}
