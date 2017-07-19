using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data.Domain;

namespace UnitTesting.DataTesting.Domain
{
    [TestFixture]
    public class PermissionTests
    {
        string correctPermString1 = "Cake_Eat";
        string correctPermString2 = "Lead_Edit";
        string incorrectPermString1 = "";
        string incorrectPermString2 = "CakeBurn";

        [Test]
        public void StringConstructorCorrectTest()
        {
            Assert.DoesNotThrow(() => new Permission(correctPermString1));
            Assert.DoesNotThrow(() => new Permission(correctPermString2));
        }

        [Test]
        public void StringConstructorIncorrectTest()
        {
            Assert.Throws<ArgumentException>(() => new Permission(incorrectPermString1));
            Assert.Throws<ArgumentException>(() => new Permission(incorrectPermString2));
        }

        Permission GoodPairItem1 = new Permission() { Name = "Cake", Action = "Eat", PermissionId = 1 };
        Permission GoodPairItem2 = new Permission() { Name = "Cake", Action = "Eat", PermissionId = 1 };
        Permission BadPairItem1 = new Permission() { Name = "Cheese", Action = "Eat", PermissionId = 2 };
        Permission BadPairItem2 = new Permission() { Name = "Cheese", Action = "Burn", PermissionId = 3 };

        [Test]
        public void EqualityTest()
        {
            Assert.AreEqual(GoodPairItem1, GoodPairItem2);
            Assert.AreNotEqual(BadPairItem1, BadPairItem2);
            Assert.IsTrue(GoodPairItem1 == GoodPairItem2);
            Assert.IsFalse(GoodPairItem1 != GoodPairItem2);
            Assert.IsTrue(BadPairItem1 != BadPairItem2);
            Assert.IsFalse(BadPairItem1 == BadPairItem2);
        }

        [Test]
        public void ContainInListWorksTest()
        {
            IList<Permission> testList = new List<Permission>();
            testList.Add(GoodPairItem1);
            testList.Add(BadPairItem1);

            Assert.IsTrue(testList.Contains(GoodPairItem1));
            Assert.IsTrue(testList.Contains(BadPairItem1));
            Assert.IsFalse(testList.Contains(BadPairItem2));

        }
    }
}
