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
    public class StaticAccountRepositoryTest
    {
        StaticAccountRepository staticAccountRepository;

        public StaticAccountRepositoryTest()
        {
            staticAccountRepository = new StaticAccountRepository();
        }
        [SetUp]
        public void SetUp()
        {
             staticAccountRepository.ClearRepo();
        }
        [Test]
        public void SaveNewAccounts()
        {
            var account = new Account
            {
                MerchantId = "a1",
                AccountName = "acca1",
                SalesRepNumber = "srn1",
                OfficeNumber = "ofn1",
                Status = "sts1",
                AccountApprovalDate = DateTime.Now.AddHours(-5.0),
                AnnualFee = true,
                EstimatedMonthlyVolume = "estvol",
                HT = "ht",
                HMV = "hmv",
                Platform = "pltform"
            };
            staticAccountRepository.SaveAccounts(account);
            var result = staticAccountRepository.Accounts.Where(row => row.MerchantId == "a1").ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].AccountId != 0);

        }

        [Test]
        public void SaveUpdatedAccounts()
        {
            var account = new Account
            {
                MerchantId = "a1",
                AccountName = "acca1",
                SalesRepNumber = "srn1",
                OfficeNumber = "ofn1",
                Status = "sts1",
                AccountApprovalDate = DateTime.Now.AddHours(-5.0),
                AnnualFee = true,
                EstimatedMonthlyVolume = "estvol",
                HT = "ht",
                HMV = "hmv",
                Platform = "pltform"
            };
            staticAccountRepository.SaveAccounts(account);
            var result = staticAccountRepository.Accounts.Where(row => row.MerchantId == "a1").ToList()[0];
            result.MerchantId = "b1";
            staticAccountRepository.SaveAccounts(result);
            var result2 = staticAccountRepository.Accounts.Where(row => row.MerchantId == "b1").ToList();
            Assert.IsTrue(result2.Count == 1, String.Format("result2 count was {0}", result2.Count));
            Assert.IsTrue(result2[0].AccountId != 0);
            Assert.IsFalse(result2[0].MerchantId=="a1");
        }
        [Test]
        public void DeleteAccounts()
        {

            var account = new Account
            {
                MerchantId = "a1",
                AccountName = "acca1",
                SalesRepNumber = "srn1",
                OfficeNumber = "ofn1",
                Status = "sts1",
                AccountApprovalDate = DateTime.Now.AddHours(-5.0),
                AnnualFee = true,
                EstimatedMonthlyVolume = "estvol",
                HT = "ht",
                HMV = "hmv",
                Platform = "pltform"
            };
            staticAccountRepository.SaveAccounts(account);
            staticAccountRepository.DeleteAccounts(account);
            var result = staticAccountRepository.Accounts.Where(row => row.MerchantId == "a1").ToList();
            Assert.IsTrue(result.Count == 0);

        }

    }
}
