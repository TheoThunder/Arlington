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
    public class StaticRoleRepositoryUnitTest
    {
        StaticRoleRepository staticRoleRepository;
       

        public StaticRoleRepositoryUnitTest()
        {
            staticRoleRepository = new StaticRoleRepository();
        }

        [SetUp]
        public void Setup()
        {
            staticRoleRepository.ClearRepo();
        }
        [Test]
        public void SaveNewRole()
        {
            var fakePermissionForTest  = new List<Permission>
            {
            new Permission{ Name = "Manager", Action = "No Action" },
            new Permission{ Name = "Agent", Action = "On" },
            new Permission{ Name = "Salesman", Action = "OffAction" },
            new Permission{ Name = "Salesperson", Action = "Call" }

            };
            var role = new Role {Name = "Representative", Permissions = fakePermissionForTest };
            staticRoleRepository.SaveRole(role);
            var result = staticRoleRepository.GetRoleByRoleName("Representative");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.RoleId != 0);

        }

        [Test]
        public void SaveUpdateRole()
        {
            var fakePermissionToTest = new List<Permission>
            {
            new Permission{ Name = "Manager", Action = "No Action" },
            new Permission{ Name = "Agent", Action = "On" },
            new Permission{ Name = "Salesman", Action = "OffAction" },
            new Permission{ Name = "Salesperson", Action = "Call" }

            };
            var role = new Role { Name = "RoleName", Permissions = fakePermissionToTest};
            staticRoleRepository.SaveRole(role);
            var result = staticRoleRepository.GetRoleByRoleName("RoleName");
            result.Name = "RoleName2";
            staticRoleRepository.SaveRole(result);
            var result2 = staticRoleRepository.GetRoleByRoleName("RoleName2");
            Assert.IsNotNull(result2);
            Assert.IsTrue(result2.Name == "RoleName2");
        }

        [Test]
        public void DeleteRole()
        {
            var fakePermissionToTest = new List<Permission>
            {
            new Permission{ Name = "Manager", Action = "No Action" },
            new Permission{ Name = "Agent", Action = "On" },
            new Permission{ Name = "Salesman", Action = "OffAction" },
            new Permission{ Name = "Salesperson", Action = "Call" }

            };
            var role = new Role { Name = "NameOfRole", Permissions = fakePermissionToTest };
            staticRoleRepository.SaveRole(role);
            staticRoleRepository.DeleteRole(role);
            var result = staticRoleRepository.GetRoleByRoleName("NameOfRole");
            Assert.IsNull(result);
        }
    }
}
