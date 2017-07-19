using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data.Repositories.Static;
using Data.Domain;

namespace UnitTesting.DataTesting.StaticRepositories
{
    [TestFixture]
    public class StaticZoneRepositoryUnitTest 
    {
          StaticZoneRepository staticZoneRepository;

        public StaticZoneRepositoryUnitTest()
        {
            staticZoneRepository = new StaticZoneRepository();
        }

        [SetUp]
        public void Setup()
        {
            staticZoneRepository.ClearRepo();
        }

        [Test]
        public void SaveNewZone()
        {
            var zone = new Zone{ ZoneNumber = 1, ZipCodesCovered = new List<string> {"91201"}};
            staticZoneRepository.SaveZone(zone);
            var result = staticZoneRepository.Zones.Where(row => row.ZoneNumber == 1).ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].ZoneNumber != 0);
        }

        [Test]
        public void SaveUpdateZone()
        {
            var zone = new Zone { ZoneNumber = 2, ZipCodesCovered = new List<string> { "61110", "62003" } };
            staticZoneRepository.SaveZone(zone);
            var result = staticZoneRepository.Zones.Where(row => row.ZoneNumber == 2).ToList();
            staticZoneRepository.SaveZone(result[0]);
            Assert.IsTrue(result.Count == 1, String.Format("result count was 0 ", result.Count));
            Assert.IsTrue(result[0].ZoneId != 0);

        }

        [Test]
        public void DeleteZone()
        {
            var zone = new Zone { ZoneNumber = 1, ZipCodesCovered = new List<string> { "91201" } };
            staticZoneRepository.SaveZone(zone);
            staticZoneRepository.DeleteZone(zone);
            var result = staticZoneRepository.Zones.Where(row => row.ZoneNumber == 1).ToList();
            Assert.IsTrue(result.Count == 0);
        }
        [Test]
        public void GetZoneByZipCode()
        {
            var zone = new Zone { ZoneNumber = 2, ZipCodesCovered = new List<string> { "61110", "62003" } };
            staticZoneRepository.SaveZone(zone);
            var result = staticZoneRepository.GetZoneByZipcode("61110");
            Assert.IsTrue(result == zone.ZoneNumber);
            //maybe another option
            //var reslt = staticZoneRepository.Zones.Where(row => row.ZipCodesCovered.Contains("61110")).ToList();
            //Assert.IsTrue(reslt[0].ZoneNumber == 2);
        }
    }
}
