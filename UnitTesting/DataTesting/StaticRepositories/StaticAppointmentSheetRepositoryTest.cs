using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data.Domain;
using Data.Repositories.Static;

//namespace UnitTesting.DataTesting.StaticRepositories
//{
//    [TestFixture]
//    public class StaticAppointmentSheetRepositoryTest
//    {
//        StaticAppointmentSheetRepository staticAppointmentSheetRepository;
//        public StaticAppointmentSheetRepositoryTest()
//        {
//            staticAppointmentSheetRepository = new StaticAppointmentSheetRepository();
//        }

//        [SetUp]
//        public void SetUp()
//        {
//            staticAppointmentSheetRepository.ClearRepo();
//        }
//        [Test]
//        public void SaveNewAppSheet()
//        {
//            var appSheet = new AppointmentSheet
//            {
//                CreatedAt = DateTime.Now.AddHours(-10.0),
//                LastUpdated = DateTime.Now,
//                DayOfAppointment = DateTime.Now.AddHours(-10.0),
//                AppointmentLocation = "abc",
//                Street = "cooper",
//                City = "Arlington",
//                State = "TX",
//                ZipCode = 76010,
//                CurrentlyAcceptingCards = true,
//                NewSetUp = true,
//                Price = true,
//                NewEquipment = true,
//                AddingServices = true,
//                Unhappy = true,
//                SingleLocation = true,
//                MultiLocation = true,
//                CurrentProcessor = "Intel",
//                HowManyLocations = 2,
//                Volume = "third",
//                Swipe = true,
//                Moto = true,
//                Internet = true,
//                Comment = "hohoho",
//                Score = "this is score"
//            };
//            staticAppointmentSheetRepository.SaveAppointmentSheet(appSheet);
//            var result = staticAppointmentSheetRepository.AppointmentSheets.Where(row => row.AppointmentLocation == "abc").ToList();
//            Assert.IsTrue(result.Count == 1);
//            Assert.IsTrue(result[0].AppointmentSheetId != 0);

//        }

//        [Test]
//        public void SaveUpdatedAppSheet()
//        {
//            var appSheet = new AppointmentSheet
//            {
//                CreatedAt = DateTime.Now.AddHours(-10.0),
//                LastUpdated = DateTime.Now,
//                DayOfAppointment = DateTime.Now.AddHours(-10.0),
//                AppointmentLocation = "abc",
//                Street = "cooper",
//                City = "Arlington",
//                State = "TX",
//                ZipCode = 76010,
//                CurrentlyAcceptingCards = true,
//                NewSetUp = true,
//                Price = true,
//                NewEquipment = true,
//                AddingServices = true,
//                Unhappy = true,
//                SingleLocation = true,
//                MultiLocation = true,
//                CurrentProcessor = "Intel",
//                HowManyLocations = 2,
//                Volume = "third",
//                Swipe = true,
//                Moto = true,
//                Internet = true,
//                Comment = "hohoho",
//                Score = "this is score"
//            };
//            staticAppointmentSheetRepository.SaveAppointmentSheet(appSheet);
//            var result = staticAppointmentSheetRepository.AppointmentSheets.Where(row => row.AppointmentLocation == "abc").ToList()[0];
//            result.HowManyLocations = 5;
//            staticAppointmentSheetRepository.SaveAppointmentSheet(result);
//            var result2 = staticAppointmentSheetRepository.AppointmentSheets.Where(row => row.AppointmentLocation=="abc").ToList();
//            Assert.IsTrue(result2.Count == 1, String.Format("result2 count was {0}", result2.Count));
//            Assert.IsTrue(result2[0].AppointmentSheetId!= 0);
//            Assert.IsFalse(result2[0].HowManyLocations==2);
//        }
//        [Test]
//        public void DeleteEquipment()
//        {
//            var appSheet = new AppointmentSheet
//            {
//                CreatedAt = DateTime.Now.AddHours(-10.0),
//                LastUpdated = DateTime.Now,
//                DayOfAppointment = DateTime.Now.AddHours(-10.0),
//                AppointmentLocation = "abc",
//                Street = "cooper",
//                City = "Arlington",
//                State = "TX",
//                ZipCode = 76010,
//                CurrentlyAcceptingCards = true,
//                NewSetUp = true,
//                Price = true,
//                NewEquipment = true,
//                AddingServices = true,
//                Unhappy = true,
//                SingleLocation = true,
//                MultiLocation = true,
//                CurrentProcessor = "Intel",
//                HowManyLocations = 2,
//                Volume = "third",
//                Swipe = true,
//                Moto = true,
//                Internet = true,
//                Comment = "hohoho",
//                Score = "this is score"
//            };
//            staticAppointmentSheetRepository.SaveAppointmentSheet(appSheet);
//            staticAppointmentSheetRepository.DeleteAppointmentSheet(appSheet);
//            var result = staticAppointmentSheetRepository.AppointmentSheets.Where(row => row.AppointmentLocation == "abc").ToList();
//            Assert.IsTrue(result.Count == 0);

//        }
//    }
//}
