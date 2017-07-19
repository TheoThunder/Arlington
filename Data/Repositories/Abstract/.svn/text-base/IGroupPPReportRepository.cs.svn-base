using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IGroupPPReportRepository
    {

        Report GetMonthlyReport(DateTime startdate, DateTime enddate);
        IList<GroupReport> GetMonthlyReportForSA(DateTime startdate, DateTime enddate, IEnumerable<User> sa_list);
    }
}
