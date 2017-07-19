using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ILeadAccessRepository
    {
        IQueryable<Lead> LeadAccessRecords { get; }
        void SaveLeadAccessRecord(string path);
        void DeleteLeadAccessRecord(Lead record);
    }
}
