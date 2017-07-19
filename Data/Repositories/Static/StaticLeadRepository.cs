using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Static
{
    public class StaticLeadRepository : ILeadRepository
    {
        //A Hardcoded list of leads.
        private static IList<Lead> fakeLeads = new List<Lead>();
        private static int counter = 1;

        public IQueryable<Domain.Lead> Leads
        {
            get { return fakeLeads.AsQueryable(); }
            
        }

        public Domain.Lead LeadByLeadID(int leadid)
        {
            Domain.Lead lead = new Lead();
            fakeLeads.Clear();
            
            return lead;

        }

        public IQueryable<Domain.Lead> LeadByStatus(string status, int userId)
        {
            //Not implemented
            try
            {

                fakeLeads.Clear();

                return fakeLeads.AsQueryable();
            }
            catch
            {
                throw new NotImplementedException();
            }


        }
        public void SaveLead(Domain.Lead lead)
        {
            // If it's a new lead, just add it to the list
            if (lead.LeadId == 0)
            {
                lead.LeadId = counter;
                counter += 1;
                fakeLeads.Add(lead);
            }
            else if (fakeLeads.Count(row => row.LeadId == lead.LeadId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteLead(lead);
                fakeLeads.Add(lead);
            }
        }

        public void DeleteLead(Domain.Lead lead)
        {
            var temp = fakeLeads.ToList();
            temp.RemoveAll(row => row.LeadId == lead.LeadId);
            fakeLeads = temp;
        }
        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeLeads.Clear();
        }
    }
}
