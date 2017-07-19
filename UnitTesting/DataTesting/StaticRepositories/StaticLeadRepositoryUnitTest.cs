using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Static;
using Data.Domain;
using NUnit.Framework;

namespace UnitTesting.DataTesting.StaticRepositories
{
    [TestFixture]
    class StaticLeadRepositoryUnitTest
    {
        private Lead Lead1;
        private StaticLeadRepository staticLeadRepository;

        public StaticLeadRepositoryUnitTest()
        {
            staticLeadRepository = new StaticLeadRepository();
        }


        [SetUp]
        public void Setup()
        {
            //Create a lead for testing
            Lead1 = new Lead() { CompanyName = "Fake Company" };

            //Before each test, purge the repository
            staticLeadRepository.ClearRepo();
            //slr.Leads.ToList().Clear();
        }

        /// <summary>
        /// Test the StaticLeadRepository's ability to save a new Lead
        /// </summary>
        [Test]
        public void TestSaveNewLead()
        {
            Assert.IsTrue(Lead1.LeadId == 0, "New Lead did not start with a LeadId of 0");
            staticLeadRepository.SaveLead(Lead1);
            var queriedLead = from lead in staticLeadRepository.Leads where lead.CompanyName == "Fake Company" select lead;
            Assert.IsTrue(queriedLead.ToList().Count == 1);
            Assert.IsTrue(queriedLead.ToList()[0].LeadId != 0);
        }

        [Test]
        public void TestSaveExistingLead()
        {
            var lead = new Lead { CompanyName = "jjjj" };
            staticLeadRepository.SaveLead(lead);
            var result = staticLeadRepository.Leads.Where(row => row.CompanyName == "jjjj").ToList()[0];
            result.CompanyName = "xyz";
            staticLeadRepository.SaveLead(result);
            var result2 = staticLeadRepository.Leads.Where(row => row.CompanyName == "xyz").ToList();
            Assert.IsTrue(result2.Count == 1);
            Assert.IsTrue(result2[0].LeadId != 0);
            Assert.IsFalse(result2[0].CompanyName=="jjjj");
        }

        [Test]
        public void TestDeleteLead()
        {
            var lead = new Lead { CompanyName = "abc"};
            staticLeadRepository.SaveLead(lead);
            staticLeadRepository.DeleteLead(lead);
            var result = staticLeadRepository.Leads.Where(row => row.CompanyName == "abc").ToList();
            Assert.IsTrue(result.Count == 0);
 
        }
    }
}
