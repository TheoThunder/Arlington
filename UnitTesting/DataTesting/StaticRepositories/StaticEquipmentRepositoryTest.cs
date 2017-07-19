using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data.Domain;
using Data.Repositories.Static;

namespace UnitTesting.DataTesting.StaticRepositories
{
    [TestFixture]
    public class StaticEquipmentRepositoryTest
    {
        StaticEquipmentRepository staticEquipmentRepository;

        public StaticEquipmentRepositoryTest()
        {
            staticEquipmentRepository = new StaticEquipmentRepository();
        }

        [SetUp]
        public void SetUp()
        {
            staticEquipmentRepository.ClearRepo();
        }

        [Test]
        public void SaveNewEquipment()
        {
            var equipment = new Equipment { Name = "abc", Type = Data.Constants.EquipmentTypes.PRINTER, Active = true };
            staticEquipmentRepository.SaveEquipment(equipment);
            var result = staticEquipmentRepository.Equipments.Where(row => row.Name == "abc").ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].EquipmentId != 0);

        }

        [Test]
        public void SaveUpdatedEquipment()
        {
            var equipment = new Equipment { Name = "pinpadChged", Type = Data.Constants.EquipmentTypes.PINPAD, Active = true };
            staticEquipmentRepository.SaveEquipment(equipment);
            var result = staticEquipmentRepository.Equipments.Where(row => row.Name == "pinpadChged").ToList()[0];
            result.Active = false;
            staticEquipmentRepository.SaveEquipment(result);
            var result2 = staticEquipmentRepository.Equipments.Where(row => row.Name == "pinpadChged").ToList();
            Assert.IsTrue(result2.Count == 1, String.Format("result2 count was {0}", result2.Count));
            Assert.IsTrue(result2[0].EquipmentId != 0);
            Assert.IsFalse(result2[0].Active);
        }

        [Test]
        public void DeleteEquipment()
        {
            var equipment = new Equipment { Name = "abc", Type = Data.Constants.EquipmentTypes.PRINTER, Active = true };
            staticEquipmentRepository.SaveEquipment(equipment);
            staticEquipmentRepository.DeleteEquipment(equipment);
            var result = staticEquipmentRepository.Equipments.Where(row => row.Name == "abc").ToList();
            Assert.IsTrue(result.Count == 0);
 
        }
       
    }
}
