using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGMonthlyVolumeReportRepository : IMonthlyVolumeReportRepository
    {
       
        private string GetTotalAccountsQuery = @"select count(*) from account where aacreator = :userid AND estimatedmonthlyvolume = :volume AND accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalSAAccountsQuery = @"select count(*) from account where assignedsalesrep = :userid AND estimatedmonthlyvolume = :volume AND accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetAccountsQuery = @"select count(*) from account where assignedsalesrep = :sauserid AND aacreator = :userid  AND estimatedmonthlyvolume = :volume AND accountapprovaldate BETWEEN :startdate AND :enddate";
        public static IList<Domain.Report> Records = new List<Domain.Report>();
        Report newReport = new Report();
       
      

        private int GetTotalAccounts(DateTime startdate, DateTime enddate, int userid, string volume)
        {
            int accountscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalAccountsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("volume", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    command.Parameters[1].Value = startdate;
                    command.Parameters[2].Value = enddate;
                    command.Parameters[3].Value = volume;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    accountscount = dr;
                }
            }
            return accountscount;
        }

        private int GetTotalSAAccounts(DateTime startdate, DateTime enddate, int userid, string volume)
        {
            int accountscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalSAAccountsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("volume", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    command.Parameters[1].Value = startdate;
                    command.Parameters[2].Value = enddate;
                    command.Parameters[3].Value = volume;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    accountscount = dr;
                }
            }
            return accountscount;
        }

        private int GetAccounts(DateTime startdate, DateTime enddate, int userid, int sauserid, string volume)
        {
            int accountscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetAccountsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("sauserid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("volume", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    command.Parameters[1].Value = sauserid;
                    command.Parameters[2].Value = startdate;
                    command.Parameters[3].Value = enddate;
                    command.Parameters[4].Value = volume;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    accountscount = dr;
                }
            }
            return accountscount;
        }

        public IEnumerable<Report> GetMonthlyReport(DateTime startdate, DateTime enddate, int userid, string volume)
        {
            IList<Report> Records = new List<Report>();
            DateTime tempDate = enddate;
            // startdate = startdate.AddDays(-(startdate.Day) + 1);
            enddate = startdate.AddMonths(1).AddDays(-(startdate.Day));
            while (tempDate >= enddate)
            {

                Report Record = new Report();
                Boolean stop = false;

                int totalAccounts = 0;
               

              
                totalAccounts = GetTotalAccounts(startdate, enddate, userid , volume);
           

                Record.Month = Helper.ConverttoStringDate(startdate);
                Record.Year = startdate.Year;
                Record.MonthlyAccounts = totalAccounts;

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

        public IEnumerable<Report> GetMonthlySAReport(DateTime startdate, DateTime enddate, int userid, string volume)
        {
            IList<Report> Records = new List<Report>();
            DateTime tempDate = enddate;
            // startdate = startdate.AddDays(-(startdate.Day) + 1);
            enddate = startdate.AddMonths(1).AddDays(-(startdate.Day));
            while (tempDate >= enddate)
            {

                Report Record = new Report();
                Boolean stop = false;

                int totalAccounts = 0;



                totalAccounts = GetTotalAccounts(startdate, enddate, userid, volume);


                Record.Month = Helper.ConverttoStringDate(startdate);
                Record.Year = startdate.Year;
                Record.MonthlyAccounts = totalAccounts;

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

        public IEnumerable<Report> GetTotalReport(DateTime startdate, DateTime enddate, int aauserid, int sauserid, string volume)
        {
            IList<Report> Records = new List<Report>();
            DateTime tempDate = enddate;
            // startdate = startdate.AddDays(-(startdate.Day) + 1);
            enddate = startdate.AddMonths(1).AddDays(-(startdate.Day));
            while (tempDate >= enddate)
            {

                Report Record = new Report();
                Boolean stop = false;

                int totalAccounts = 0;



                totalAccounts = GetAccounts(startdate, enddate, aauserid , sauserid, volume);


                Record.Month = Helper.ConverttoStringDate(startdate);
                Record.Year = startdate.Year;
                Record.MonthlyAccounts = totalAccounts;

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
