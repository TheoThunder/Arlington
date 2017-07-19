using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ISAPSReportRepository
    {
        IQueryable<Report> Reports { get; }
        Report CheckExistingRecord(int said);
        void SaveReports(Report data);

        IEnumerable<Report> GetMonthlyReport(DateTime startdate, DateTime enddate, int userid);
        int[] GetTotalValue(DateTime startdate, DateTime enddate, int userid);
    }
}
