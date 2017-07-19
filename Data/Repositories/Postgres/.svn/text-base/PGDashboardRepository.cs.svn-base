using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGDashboardRepository : IDashboardRepository
    {
        #region Query Strings
        private string GetTotalCallsQuery = @"select count(*) from card where creatorid = :userid AND createdon BETWEEN :startdate AND :enddate";

		private string GetTotalAppointmentsQuery = @"select count(*) from appointmentsheet where creator = :userid AND createdat BETWEEN :startdate AND :enddate";
        private string GetTotalClosesQuery = @"select count(distinct parentlead) from account where aacreator = :userid AND accountapprovaldate BETWEEN :startdate AND :enddate";
        private string GetTotalGoodAppointmentsQuery = @"select count(*) from appointmentsheet where creator = :userid AND score = 'Good' AND createdat BETWEEN :startdate AND :enddate";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Report> Records = new List<Domain.Report>();
        Report newReport = new Report();
        
        
        public int GetTotalGoodAppointments(DateTime startdate, DateTime enddate, int userid)
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
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetTotalClosesQuery, conn))
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

        public Report GetWeeklyDashboard(DateTime startdate, DateTime enddate, int userid)
        {
            
                Report Record = new Report();

                int totalAppointments = 0;
                
                int totalCloses = 0;
                int totalGoodAppointments = 0;

                totalAppointments = GetTotalAppointments(startdate, enddate, userid);
                totalCloses = GetTotalCloses(startdate, enddate, userid);
                totalGoodAppointments = GetTotalGoodAppointments(startdate, enddate, userid);

              
                Record.MonthlyAppointments = totalAppointments;
                Record.MonthlyCloses = totalCloses;
                Record.MonthlyGoodAppointments = totalGoodAppointments;


            return Record;
        }
        public Report GetDailyDashboard(DateTime startdate, DateTime enddate, int userid)
        {

            Report Record = new Report();
            int totalcalls = 0;

            totalcalls = GetTotalCalls(startdate, enddate, userid);

            Record.MonthlyCalls = totalcalls;
            return Record;
        }
      
    }
}
