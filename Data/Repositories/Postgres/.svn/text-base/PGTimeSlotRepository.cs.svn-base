using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Data.Repositories.Postgres
{
    public class PGTimeSlotRepository : ITimeSlotRepository
    {

        #region Query Strings
        private string TimeSlotSelectQuery = @"SELECT timeslot_id, num_available_sa, color, start_time, end_time, title, all_day, parent_user_id, zone FROM time_slots ";

        private string TimeSlotInsertQuery = @"INSERT INTO time_slots ( num_available_sa, color, start_time, end_time, title, all_day, parent_user_id, zone) VALUES (:num_available_sa, :color, :start_time, :end_time, :title, :all_day, :parent_user_id, :zone) ";

        private string TimeSlotUpdateQuery = @"UPDATE time_slots SET  num_available_sa=:num_available_sa, color=:color, start_time=:start_time, end_time=:end_time, title=:title, all_day=:all_day, parent_user_id = :parent_user_id, zone = :zone WHERE timeslot_id = :timeslot_id ";

      
        #endregion

        //  private static int counter = 1;
        public static IList<TimeSlot> realSlots = new List<TimeSlot>();
        public IQueryable<Domain.TimeSlot> TimeSlots
        {
            get
            {
                realSlots.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TimeSlotSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TimeSlot newSlot = populateSlotsFromDB(dr);
                                realSlots.Add(newSlot);
                            }
                        }
                    }
                }
                return realSlots.AsQueryable();

            }

        }

        public void SaveTimeSlot(TimeSlot timeSlot)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (timeSlot.TimeSlotId > 0)
            {
                query = TimeSlotUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = TimeSlotInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("num_available_sa", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("color", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("start_time", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("end_time", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("title", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("all_day", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("parent_user_id", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zone", NpgsqlTypes.NpgsqlDbType.Integer));
                    

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("timeslot_id", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = timeSlot.Num_Available_SA;
                    command.Parameters[1].Value = timeSlot.Color;
                    command.Parameters[2].Value = timeSlot.StartTime;
                    command.Parameters[3].Value = timeSlot.EndTime;
                    command.Parameters[4].Value = timeSlot.Title;
                    command.Parameters[5].Value = timeSlot.All_Day;
                    command.Parameters[6].Value = timeSlot.Parent_User_ID;
                    command.Parameters[7].Value = timeSlot.Zone;
                   
                    if (isUpdate)
                    {
                        command.Parameters[8].Value = timeSlot.TimeSlotId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

       

        #region Helper Methods
        private static TimeSlot populateSlotsFromDB(Npgsql.NpgsqlDataReader dr)
        {
            TimeSlot newSlot = new TimeSlot();
            newSlot.TimeSlotId = Helper.ConvertFromDBVal<int>(dr[0]);
            newSlot.Num_Available_SA = Helper.ConvertFromDBVal<int>(dr[1]);
            newSlot.Color= dr[2].ToString();
            newSlot.StartTime = dr[3].ToString();
            newSlot.EndTime = dr[4].ToString();
            newSlot.Title = Helper.ConvertFromDBVal<int>(dr[5]);
            newSlot.All_Day = Helper.ConvertFromDBVal<Boolean>(dr[6]);
            newSlot.Parent_User_ID = Helper.ConvertFromDBVal<int>(dr[7]);
            newSlot.Zone = Helper.ConvertFromDBVal<int>(dr[8]);
            
            return newSlot;
        }
        #endregion
    }
}
