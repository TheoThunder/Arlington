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
    public class StaticUserRepositoryTest
    {
        StaticUserRepository staticUserRepository;

        public StaticUserRepositoryTest()
        {
            staticUserRepository = new StaticUserRepository();
        }

        [SetUp]
        public void SetUp()
        {
            staticUserRepository.ClearRepo();
        }

        [Test]
        public void SaveNewUser()
        {
            var user = new User
            {
                UserName = "User1",
                newPassword = "Trinity123",
                FirstName = "Trinity",
                LastName = "User",
                Address1 = "222 Cooper st",
                Address2 = "suite#321",
                State = "TX",
                City = "Arlington",
                ZipCode = "75643",
                OfficeNumber = 817214567,
                SalesRepNumber = 2124,
                CalendarColor = "yellow"
            };
            staticUserRepository.SaveUser(user);
            var result = staticUserRepository.GetUserByUsername("User1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.UserId!= 0);

        }

        [Test]
        public void SaveUpdatedUser()
        {
            var user = new User
            {
                UserName = "User1",
                newPassword = "Trinity123",
                FirstName = "Trinity",
                LastName = "User",
                Address1 = "222 Cooper st",
                Address2 = "suite#321",
                State = "TX",
                City = "Arlington",
                ZipCode = "75643",
                OfficeNumber = 817214567,
                SalesRepNumber = 2124,
                CalendarColor = "yellow"
            };
            staticUserRepository.SaveUser(user);
            var result = staticUserRepository.GetUserByUsername("User1");
            result.SalesRepNumber = 2125;
            int id = result.UserId;
            staticUserRepository.SaveUser(result);
            var result2 = staticUserRepository.GetUserById(id);
            Assert.IsNotNull(result2);
            Assert.IsTrue(result2.UserId != 0);
            Assert.IsFalse(result2.SalesRepNumber == 2124);            
        }

        [Test]
        public void DeleteUser()
        {
            var user = new User
            {
                UserName = "User1",
                newPassword = "Trinity123",
                FirstName = "Trinity",
                LastName = "User",
                Address1 = "222 Cooper st",
                Address2 = "suite#321",
                State = "TX",
                City = "Arlington",
                ZipCode = "75643",
                OfficeNumber = 817214567,
                SalesRepNumber = 2124,
                CalendarColor = "yellow"
            };
            staticUserRepository.SaveUser(user);
            staticUserRepository.DeleteUser(user.UserId);
            var result = staticUserRepository.GetUserByUsername("User1");
            Assert.IsNull(result);
        }

    }
}
