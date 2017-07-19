using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGThresholdRepository : IThresholdRepository
    {
        #region Query Strings
        private string ThresholdSelectQuery = @"SELECT thresholdid, uppercalendar, lowercalendar, we_upperdashboard, we_lowerdashboard, nc_upperdashboard, nc_lowerdashboard, weg_upperdashboard, wes_upperdashboard, weg_lowerdashboard, wes_lowerdashboard FROM threshold";
        private string ThresholdInsertQuery = @"INSERT INTO threshold (uppercalendar, lowercalendar, we_upperdashboard, we_lowerdashboard, nc_upperdashboard, nc_lowerdashboard, weg_upperdashboard, wes_upperdashboard, weg_lowerdashboard, wes_lowerdashboard) VALUES (:uppercalendar, :lowercalendar, :we_upperdashboard, :we_lowerdashboard, :nc_upperdashboard, :nc_lowerdashboard, :weg_upperdashboard, :wes_upperdashboard, :weg_lowerdashboard, :wes_lowerdashboard)";

        private string ThresholdUpdateQuery = @"UPDATE threshold SET uppercalendar = :uppercalendar, lowercalendar = :lowercalendar, we_upperdashboard = :we_upperdashboard, we_lowerdashboard = :we_lowerdashboard, nc_upperdashboard = :nc_upperdashboard, nc_lowerdashboard = :nc_lowerdashboard, weg_upperdashboard = :weg_upperdashboard, wes_upperdashboard = :wes_upperdashboard, weg_lowerdashboard = :weg_lowerdashboard, wes_lowerdashboard = :wes_lowerdashboard WHERE thresholdid = :thresholdid";

        
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Threshold> fakeThresholds = new List<Domain.Threshold>();
        public IQueryable<Domain.Threshold> Thresholds
        {

            get
            {
                fakeThresholds.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ThresholdSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Threshold newThreshold = populateThresholdFromDB(dr);
                                fakeThresholds.Add(newThreshold);
                            }
                        }
                    }
                }
                return fakeThresholds.AsQueryable();

            }

        }

        public void SaveThreshold(Domain.Threshold Threshold)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (Threshold.ThresholdId > 0)
            {
                query = ThresholdUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = ThresholdInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("uppercalendar", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("lowercalendar", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("we_upperdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("we_lowerdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("nc_upperdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("nc_lowerdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("weg_upperdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("wes_upperdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("weg_lowerdashboard", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("wes_lowerdashboard", NpgsqlTypes.NpgsqlDbType.Integer));

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("thresholdid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = Threshold.Upper_Calendar;
                    command.Parameters[1].Value = Threshold.Lower_Calendar;
                    command.Parameters[4].Value = Threshold.NC_Upper_Dashboard;
                    command.Parameters[5].Value = Threshold.NC_Lower_Dashboard;
                    command.Parameters[6].Value = Threshold.WE_GA_Upper_Dashboard;
                    command.Parameters[7].Value = Threshold.WE_SA_Upper_Dashboard;
                    command.Parameters[8].Value = Threshold.WE_GA_Lower_Dashboard;
                    command.Parameters[9].Value = Threshold.WE_SA_Lower_Dashboard;

                    if (isUpdate)
                    {
                        command.Parameters[10].Value = Threshold.ThresholdId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }


        }

       
        #region Helper Methods
        private static Threshold populateThresholdFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Threshold newThreshold = new Threshold();
            newThreshold.ThresholdId = Helper.ConvertFromDBVal<int>(dr[0]);
            newThreshold.Upper_Calendar = Helper.ConvertFromDBVal<int>(dr[1]);
            newThreshold.Lower_Calendar = Helper.ConvertFromDBVal<int>(dr[2]);
            newThreshold.NC_Upper_Dashboard = Helper.ConvertFromDBVal<int>(dr[5]);
            newThreshold.NC_Lower_Dashboard = Helper.ConvertFromDBVal<int>(dr[6]);
            newThreshold.WE_GA_Upper_Dashboard = Helper.ConvertFromDBVal<int>(dr[7]);
            newThreshold.WE_SA_Upper_Dashboard = Helper.ConvertFromDBVal<int>(dr[8]);
            newThreshold.WE_GA_Lower_Dashboard = Helper.ConvertFromDBVal<int>(dr[9]);
            newThreshold.WE_SA_Lower_Dashboard = Helper.ConvertFromDBVal<int>(dr[10]);
            return newThreshold;
        }

        #endregion
    }
}
