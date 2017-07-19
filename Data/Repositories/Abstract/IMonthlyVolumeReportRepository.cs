using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IMonthlyVolumeReportRepository
    {
        IEnumerable<Report> GetMonthlyReport(DateTime startdate, DateTime enddate, int userid, string volume);
        IEnumerable<Report> GetMonthlySAReport(DateTime startdate, DateTime enddate, int userid, string volume);
        IEnumerable<Report> GetTotalReport(DateTime startdate, DateTime enddate, int aauserid, int sauserid, string volume);
    }
}
