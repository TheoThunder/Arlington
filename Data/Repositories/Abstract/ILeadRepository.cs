using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ILeadRepository
    {
        IQueryable<Lead> Leads { get; }
        void SaveLead(Lead lead);
        void DeleteLead(Lead lead);
        Lead LeadByLeadID(int leadid);
        IQueryable<Domain.Lead> LeadByStatus(string status, int userId);
    }
}
