using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGGroupPPReportRepository : IGroupPPReportRepository
    {
        #region Query Strings
        // Region Monthly report
        private string GetTotalAppointmentsQuery = @"select count(*) from appointmentsheet WHERE createdat BETWEEN :startdate AND :enddate";
        private string GetTotalAccountsQuery = @"select count(*) from account where accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalCallsQuery = @"select count(*) from card where  createdon BETWEEN :startdate AND :enddate";
        private string GetTotalClosesQuery = @"select count(distinct parentlead) from account where accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalGoodAppointmentsQuery = @"select count(*) from appointmentsheet where score = 'Good' AND createdat BETWEEN :startdate AND :enddate";

        // Region Weekly Report

        private string GetWorkingAAPerWeek = @"select count(distinct creator) from appointmentsheet WHERE createdat BETWEEN :startdate AND :enddate";
        #endregion

        // Region Sales Agent Breakdown Summary

        private string GetTotalClosesSAQuery = @"select count(distinct parentlead) from account where aacreator = :userid AND accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalGoodAppointmentsSAQuery = @"select count(*) from appointmentsheet where assignedsalesagent = :userid AND score = 'Good' AND createdat BETWEEN :startdate AND :enddate";

        //  private static int counter = 1;
        public static IList<Domain.Report> Records = new List<Domain.Report>();
        Report newReport = new Report();
       

       

        private int GetTotalGoodAppointments(DateTime startdate, DateTime enddate)
        {
            int appointmentscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalGoodAppointmentsQuery, conn))
                {
                    
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();
                   
                    command.Parameters[0].Value = startdate;
                    command.Parameters[1].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    appointmentscount = dr;
                }
            }
            return appointmentscount;
        }

        private int GetTotalCloses(DateTime startdate, DateTime enddate)
        {
            int closescount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalClosesQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();

                    command.Parameters[0].Value = startdate;
                    command.Parameters[1].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    closescount = dr;
                }
            }
            return closescount;

        }

        private int GetTotalCalls(DateTime startdate, DateTime enddate)
        {
            int callscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalCallsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();

                    command.Parameters[0].Value = startdate;
                    command.Parameters[1].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    callscount = dr;
                }
            }
            return callscount;
        }

        private int GetTotalAccounts(DateTime startdate, DateTime enddate)
        {
            int accountscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalAccountsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();

                    command.Parameters[0].Value = startdate;
                    command.Parameters[1].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    accountscount = dr;
                }
            }
            return accountscount;
        }

        private int GetTotalAppointments(DateTime startdate, DateTime enddate)
        {
            int appointmentagents;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalAppointmentsQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();

                    command.Parameters[0].Value = startdate;
                    command.Parameters[1].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    appointmentagents = dr;
                }
            }
            return appointmentagents;
        }

        private int GetTotalAppointmentAgents(DateTime startdate, DateTime enddate)
        {
            int appointmentscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetWorkingAAPerWeek, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("startdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("enddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Prepare();

                    command.Parameters[0].Value = startdate;
                    command.Parameters[1].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    appointmentscount = dr;
                }
            }
            return appointmentscount;
        }

        public Report GetMonthlyReport(DateTime startdate, DateTime enddate)
        {
            
                Report Record = new Report();
               
                int totalAppointments = 0;
                int totalAccounts = 0;
                int totalCalls = 0;
                int totalCloses = 0;
                int totalGoodAppointments = 0;
                int totalAppointmentAgents = 0;

                totalAppointments = GetTotalAppointments(startdate, enddate);
                totalAccounts = GetTotalAccounts(startdate, enddate);
                totalCalls = GetTotalCalls(startdate, enddate);
                totalCloses = GetTotalCloses(startdate, enddate);
                totalGoodAppointments = GetTotalGoodAppointments(startdate, enddate);
                totalAppointmentAgents = GetTotalAppointmentAgents(startdate, enddate);

                Record.MonthlyAccounts = totalAccounts;
                Record.MonthlyAppointments = totalAppointments;
                Record.MonthlyCalls = totalCalls;
                Record.MonthlyCloses = totalCloses;
                Record.MonthlyGoodAppointments = totalGoodAppointments;
                Record.NumberOfAAPerMonth = totalAppointmentAgents;


            return Record;
        }

        public IList<GroupReport> GetMonthlyReportForSA(DateTime startdate, DateTime enddate, IEnumerable<User> sa_list)
        {
            IList<GroupReport> group_report = new List<GroupReport>();

            foreach (var user in sa_list)
            {

                GroupReport newRecord = new GroupReport();

                newRecord.firstName = user.FirstName;
                newRecord.lastName = user.LastName;
                newRecord.TGAPM = GetTGAPM(startdate, enddate, user.UserId);
                newRecord.GAPW = (double)newRecord.TGAPM / (((double)enddate.Day) / (double)7);
                newRecord.AppointmentPercentage = (double)newRecord.TGAPM / (double)GetTotalGoodAppointments(startdate, enddate);
                newRecord.TCPM = GetTCPM(startdate, enddate, user.UserId);
                newRecord.SACR = (double)newRecord.TCPM / (double)newRecord.TGAPM;

                group_report.Add(newRecord);

            }


            return group_report;
        }

        private int GetTCPM(DateTime startdate, DateTime enddate, int userid)
        {
            int closescount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalClosesSAQuery, conn))
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

        private int GetTGAPM(DateTime startdate, DateTime enddate, int userid)
        {
            int appointmentscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalGoodAppointmentsSAQuery, conn))
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
                    appointmentscount = dr;
                }
            }
            return appointmentscount;
        }

     
        #region Helper Methods
        private static Report populateRecordsFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Report newRecord = new Report();
            newRecord.ReportId = Helper.ConvertFromDBVal<int>(dr[0]);
            newRecord.Month = dr[1].ToString();
            newRecord.Year = Helper.ConvertFromDBVal<int>(dr[2]);
            newRecord.MonthlyCalls = Helper.ConvertFromDBVal<int>(dr[3]);
            newRecord.MonthlyAppointments = Helper.ConvertFromDBVal<int>(dr[4]);
            newRecord.MonthlyGoodAppointments = Helper.ConvertFromDBVal<int>(dr[5]);
            newRecord.MonthlyCloses = Helper.ConvertFromDBVal<int>(dr[6]);
            newRecord.MonthlyAccounts = Helper.ConvertFromDBVal<int>(dr[7]);
            newRecord.AssignedAAUserID = Helper.ConvertFromDBVal<int>(dr[8]);
            newRecord.AssignedSAUserID = Helper.ConvertFromDBVal<int>(dr[9]);

            return newRecord;
        }

        #endregion
    }
}
