using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGAAPSReportRepository : IAAPSReportRepository
    {
        #region Query Strings
        private string ReportSelectQuery = @"SELECT mbsreport_id, month, year, calls, appointments, goodapts, closes, accounts, aauserid, sauserid FROM aaps_mbsreport";

        private string ReportCheckExistingRecordQuery = @"select mbsreport_id, month, year, calls, appointments, goodapts, closes, accounts, aauserid, sauserid from aaps_mbsreport where month = :month AND year = :year AND aauserid = :aauserid";
        private string ReportInsertQuery = @"INSERT INTO aaps_mbsreport(month, year, calls, appointments, goodapts, closes, accounts, aauserid, sauserid) VALUES (:month, :year, :calls, :appointments, :goodapts, :closes, :accounts, :aauserid, :sauserid)";

        private string ReportUpdateQuery = @"UPDATE aaps_mbsreport SET month = :month, year = :year, calls = :calls, appointments = :appointments, goodapts = :goodapts, closes = :closes, accounts = :accounts, aauserid = :aauserid, sauserid = :sauserid WHERE mbsreport_id = :mbsreport_id";

        private string GetTotalAppointmentsQuery = @"select count(*) from appointmentsheet where creator = :userid AND createdat BETWEEN :startdate AND :enddate";
        private string GetTotalAccountsQuery = @"select count(*) from account where aacreator = :userid AND accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalCallsQuery = @"select count(*) from card where creatorid = :userid AND createdon BETWEEN :startdate AND :enddate";
        private string GetTotalClosesQuery = @"select count(distinct parentlead) from account where aacreator = :userid AND accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalGoodAppointmentsQuery = @"select count(*) from appointmentsheet where creator = :userid AND score = 'Good' AND createdat BETWEEN :startdate AND :enddate";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Report> Records = new List<Domain.Report>();
        Report newReport = new Report();
        public IQueryable<Domain.Report> Reports
        {

            get
            {
                Records.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ReportSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Report newRecord = populateRecordsFromDB(dr);
                                Records.Add(newRecord);
                            }
                        }
                    }
                }
                return Records.AsQueryable();

            }

        }

        public Report CheckExistingRecord(int aaid)
        {

             
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ReportCheckExistingRecordQuery, conn))
                    {
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("month", NpgsqlTypes.NpgsqlDbType.Text));
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("year", NpgsqlTypes.NpgsqlDbType.Integer));
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("aauserid", NpgsqlTypes.NpgsqlDbType.Integer));
                        command.Prepare();
                        command.Parameters[0].Value = Helper.ConverttoStringDate(DateTime.Now);
                        command.Parameters[1].Value = DateTime.Now.Year;
                        command.Parameters[2].Value = aaid;
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                newReport = populateRecordsFromDB(dr); ;
                            }
                        }
                    }
                }
                return newReport;

            

        }

        public void SaveReports(Domain.Report Record)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (CheckExistingRecord(Record.AssignedAAUserID).ReportId != 0)
            {
                query = ReportUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = ReportInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("month", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("year", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("calls", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointments", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("goodapts", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("closes", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accounts", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("aauserid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("sauserid", NpgsqlTypes.NpgsqlDbType.Integer));

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("mbsreport_id", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = Helper.ConverttoStringDate(DateTime.Now);
                    command.Parameters[1].Value = DateTime.Now.Year;
                    command.Parameters[2].Value = Record.MonthlyCalls; 
                    command.Parameters[3].Value = Record.MonthlyAppointments;
                    command.Parameters[4].Value = Record.MonthlyGoodAppointments;
                    command.Parameters[5].Value = Record.MonthlyCloses;
                    command.Parameters[6].Value = Record.MonthlyAccounts;
                    command.Parameters[7].Value = Record.AssignedAAUserID;
                    command.Parameters[8].Value = Record.AssignedSAUserID;

                    if (isUpdate)
                    {
                        command.Parameters[9].Value = Record.ReportId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }


        }

        public int[] GetTotalValue(DateTime startdate, DateTime enddate, int userid)
        {
            int[] TotalList = new int[5];
            int totalAppointments = 0;
            int totalAccounts = 0;
            int totalCalls = 0;
            int totalCloses = 0;
            int totalGoodAppointments = 0;

            totalAppointments = GetTotalAppointments(startdate, enddate, userid);
            totalAccounts = GetTotalAccounts(startdate,  enddate, userid);
            totalCalls = GetTotalCalls( startdate,  enddate, userid);
            totalCloses = GetTotalCloses(startdate,  enddate, userid);
            totalGoodAppointments = GetTotalGoodAppointments(startdate, enddate, userid);

            TotalList[0] = totalAppointments;
            TotalList[1] = totalAccounts;
            TotalList[2] = totalCalls;
            TotalList[3] = totalCloses;
            TotalList[4] = totalGoodAppointments;

            return TotalList;
            
    }

        private int GetTotalGoodAppointments(DateTime startdate, DateTime enddate, int userid)
        {
            int appointmentscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalGoodAppointmentsQuery, conn))
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

        private int GetTotalCloses(DateTime startdate, DateTime enddate, int userid)
        {
            int closescount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalClosesQuery , conn))
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

        private int GetTotalCalls(DateTime startdate, DateTime enddate, int userid)
        {
            int callscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalCallsQuery, conn))
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
                    callscount = dr;
                }
            }
            return callscount;
        }

        private int GetTotalAccounts(DateTime startdate, DateTime enddate, int userid)
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
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    command.Parameters[1].Value = startdate;
                    command.Parameters[2].Value = enddate;

                    int dr;
                    dr = Convert.ToInt32(command.ExecuteScalar());
                    accountscount = dr;
                }
            }
            return accountscount;
        }

        private int GetTotalAppointments(DateTime startdate, DateTime enddate, int userid)
        {
            int appointmentscount;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalAppointmentsQuery, conn))
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
                
                int totalAppointments = 0;
                int totalAccounts = 0;
                int totalCalls = 0;
                int totalCloses = 0;
                int totalGoodAppointments = 0;

                totalAppointments = GetTotalAppointments(startdate, enddate, userid);
                totalAccounts = GetTotalAccounts(startdate, enddate, userid);
                totalCalls = GetTotalCalls(startdate, enddate, userid);
                totalCloses = GetTotalCloses(startdate, enddate, userid);
                totalGoodAppointments = GetTotalGoodAppointments(startdate, enddate, userid);

                Record.Month = Helper.ConverttoStringDate(startdate);
                Record.Year = startdate.Year;
                Record.MonthlyAccounts = totalAccounts;
                Record.MonthlyAppointments = totalAppointments;
                Record.MonthlyCalls = totalCalls;
                Record.MonthlyCloses = totalCloses;
                Record.MonthlyGoodAppointments = totalGoodAppointments;

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
