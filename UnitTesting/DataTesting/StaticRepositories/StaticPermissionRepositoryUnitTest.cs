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
    public class StaticPermissionRepositoryUnitTest
    {
        StaticPermissionRepository staticPermissionRepository;

        public StaticPermissionRepositoryUnitTest()
        {
            staticPermissionRepository = new StaticPermissionRepository();
        }


        [SetUp]
        public void Setup()
        {
            staticPermissionRepository.ClearRepo();
        }

        [Test]
        public void SaveNewPermission()
        {
            var permission = new Permission { Name = "Admin", Action = "Delete" };
            staticPermissionRepository.SavePermission(permission);
            var result = staticPermissionRepository.Permissions.Where(row => row.Name == "Admin").ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].PermissionId != 0);
        }

        [Test]
        public void SaveUpdatePermission()
        {
            var permission = new Permission { Name = "Lead", Action = "Call" };
            staticPermissionRepository.SavePermission(permission);
            var result = staticPermissionRepository.Permissions.Where(row => row.Name == "Lead").ToList();
            staticPermissionRepository.SavePermission(result[0]);
            Assert.IsTrue(result.Count == 1, String.Format("result count was 0", result.Count));
            Assert.IsTrue(result[0].PermissionId != 0);
        }

        [Test]
        public void DeletePermission()
        {
            var permission = new Permission { Name = "Manager", Action = "No Action" };
            staticPermissionRepository.SavePermission(permission);
            staticPermissionRepository.DeletePermission(permission);
            var result = staticPermissionRepository.Permissions.Where(row => row.Action == "No Action").ToList();
            Assert.IsTrue(result.Count == 0);
        }
    }
}
