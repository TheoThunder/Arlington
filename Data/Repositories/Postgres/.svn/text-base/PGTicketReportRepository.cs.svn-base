using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGTicketReportRepository : ITicketReportRepository
    {
        #region Query Strings
       
        private string GetTotalOpenedTicketsQuery = @"select count(*) from ticket where tstatus != 'Closed' AND creator = :userid AND date_opened BETWEEN :startdate AND :enddate";
        private string GetTotalClosedTicketsQuery = @"select count(*) from ticket where tstatus = 'Closed' AND closedby = :userid AND date_closed BETWEEN :startdate AND :enddate";
        
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Report> Records = new List<Domain.Report>();
        Report newReport = new Report();
       

        public int[] GetTotalValue(DateTime startdate, DateTime enddate, int userid)
        {
            int[] TotalList = new int[2];
            int totalTicketOpened = 0;
            int totalTicketClosed = 0;
           

            totalTicketOpened = GetTotalOpened(startdate, enddate, userid);
            totalTicketClosed = GetTotalClosed(startdate, enddate, userid);
           

            TotalList[0] = totalTicketOpened;
            TotalList[1] = totalTicketClosed;
           

            return TotalList;

        }

        private int GetTotalOpened(DateTime startdate, DateTime enddate, int userid)
        {
            int count;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalOpenedTicketsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    command.Parameters[1].Value = startdate;
                    command.Parameters[2].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    count = dr;
                }
            }
            return count;
        }

        private int GetTotalClosed(DateTime startdate, DateTime enddate, int userid)
        {
            int closescount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalClosedTicketsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    command.Parameters[1].Value = startdate;
                    command.Parameters[2].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    closescount = dr;
                }
            }
            return closescount;

        }


        public IEnumerable<Report> GetMonthlyReport(DateTime startdate, DateTime enddate, int userid)
        {
            IList<Report> Records = new List<Report>();
            DateTime tempDate = enddate;
            // startdate = startdate.AddDays(-(startdate.Day) + 1);
            enddate = startdate.AddMonths(1).AddDays(-(startdate.Day));
            while (tempDate >= enddate)
            {

                Report Record = new Report();
                Boolean stop = false;

                int totalTicketsOpened = 0;
                int totalTicketsClosed = 0;


                totalTicketsOpened = GetTotalOpened(startdate, enddate, userid);
                totalTicketsClosed = GetTotalClosed(startdate, enddate, userid);
               

                Record.Month = Helper.ConverttoStringDate(startdate);
                Record.Year = startdate.Year;
                Record.TicketsOpened = totalTicketsOpened;
                Record.TicketsClosed = totalTicketsClosed;
              

                if (startdate.Day != 1 || startdate.Day != 31 || startdate.Day != 30 || startdate.Day != 28)
                {
                    startdate = startdate.AddMonths(1).AddDays(-(startdate.Day) + 1);
                }
                else
                {
                    startdate = startdate.AddMonths(1);
                }
                
                Records.Add(Record);
                if (tempDate < enddate && stop == false)
                {
                    enddate = tempDate.AddSeconds(enddate.Second - 1);
                    stop = true;
                }
                enddate = enddate.AddMonths(1);
            }

            return Records;
        }
  
    }
}
