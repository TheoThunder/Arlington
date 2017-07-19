using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ITicketReportRepository
    {
        IEnumerable<Report> GetMonthlyReport(DateTime startdate, DateTime enddate, int userid);
        int[] GetTotalValue(DateTime startdate, DateTime enddate, int userid);
    }
}