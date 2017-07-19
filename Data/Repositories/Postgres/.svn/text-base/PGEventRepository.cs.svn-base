using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Data.Repositories.Postgres
{
    public class PGEventRepository : ICalendarEventRepository
    {

        #region Query Strings
        private string EventSelectQuery = @"SELECT eventid, title, type, description, appointment, personal, city, state, zip, zone, map, creatorid, assigneduserid, starttime, endtime, street, parent_user_id, parent_appointment_id, appointment_reference FROM event ";

        private string EventInsertQuery = @"INSERT INTO event (title, type, description, appointment, personal, city, state, zip, zone, map, creatorid, assigneduserid, starttime, endtime, street, parent_user_id, parent_appointment_id, appointment_reference) VALUES (:title, :type, :description, :appointment, :personal, :city, :state, :zip, :zone, :map, :creatorid, :assigneduserid, :stringstarttime, :stringendtime, :street, :parent_user_id, :parent_appointment_id, :appointment_reference)";

        private string EventUpdateQuery = @"UPDATE event SET title = :title, type = :type, description = :description, appointment = :appointment,  personal = :personal, city = :city, state = :state, zip = :zip, zone = :zone, map = :map , creatorid = :creatorid, assigneduserid = :assigneduserid, starttime = :stringstarttime, endtime = :stringendtime, street = :street, parent_user_id = :parent_user_id, parent_appointment_id = :parent_appointment_id, appointment_reference = :appointment_reference WHERE eventid = :eventid";

        private string EventByAppointmentIdQuery = @"SELECT eventid, title, type, description, appointment, personal, city, state, zip, zone, map, creatorid, assigneduserid, starttime, endtime, street, parent_user_id, parent_appointment_id, appointment_reference FROM event Where appointment_reference = :appointmentid";

        private string EventDeleteQuery = @"DELETE FROM event WHERE eventid = :eventid";
        private string EventByZoneNumberQuery = @"SELECT eventid, title, type, description, appointment, personal, city, state, zip, zone, map, creatorid, assigneduserid, starttime, endtime, street, parent_user_id, parent_appointment_id, appointment_reference FROM event Where zone = :zone";
        private string EventByUserIdQuery = @"SELECT eventid, title, type, description, appointment, personal, city, state, zip, zone, map, creatorid, assigneduserid, starttime, endtime, street, parent_user_id, parent_appointment_id, appointment_reference FROM event Where parent_user_id = :userid";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.CalendarEvent> realEvents = new List<Domain.CalendarEvent>();
        public IQueryable<Domain.CalendarEvent> CalendarEvents
        {
            get
            {
                realEvents.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EventSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                CalendarEvent newEvent = populateEventsFromDB(dr);
                                realEvents.Add(newEvent);
                            }
                        }
                    }
                }
                return realEvents.AsQueryable();

            }

        }

        public void SaveCalendarEvent(CalendarEvent calendarEvent)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (calendarEvent.id > 0)
            {
                query = EventUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = EventInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("title", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("type", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("description", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointment", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("personal", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zip", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zone", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("map", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("creatorid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assigneduserid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("stringstarttime", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("stringendtime", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("street", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("parent_user_id", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("parent_appointment_id", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointment_reference", NpgsqlTypes.NpgsqlDbType.Text));

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("eventid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = calendarEvent.title;
                    command.Parameters[1].Value = calendarEvent.type;
                    command.Parameters[2].Value = calendarEvent.description;
                    command.Parameters[3].Value = calendarEvent.appointment;
                    command.Parameters[4].Value = calendarEvent.personal;
                    command.Parameters[5].Value = calendarEvent.city;
                    command.Parameters[6].Value = calendarEvent.state;
                    command.Parameters[7].Value = calendarEvent.zipcode;
                    command.Parameters[8].Value = calendarEvent.zone;
                    command.Parameters[9].Value = calendarEvent.map;
                    command.Parameters[10].Value = calendarEvent.creator;
                    command.Parameters[11].Value = calendarEvent.assigned;
                    command.Parameters[12].Value = calendarEvent.start;
                    command.Parameters[13].Value = calendarEvent.end;
                    command.Parameters[14].Value = calendarEvent.street;
                    command.Parameters[15].Value = calendarEvent.Parent_User_Id;
                    command.Parameters[16].Value = calendarEvent.Parent_Appointment_Id;
                    command.Parameters[17].Value = calendarEvent.Appointment_Reference;
                   
                    if (isUpdate)
                    {
                        command.Parameters[18].Value = calendarEvent.id;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCalendarEvent(CalendarEvent calendarEvent)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EventDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("eventid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = calendarEvent.id;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public CalendarEvent GetEventByAppointmentID(string id)
        {
            CalendarEvent currentEvent = new CalendarEvent();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EventByAppointmentIdQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentid", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = id;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                           currentEvent = populateEventsFromDB(dr);
                           
                        }
                    }
                }
            }

            return currentEvent;
        }

        public IEnumerable<CalendarEvent> GetEventsByZoneNumber(int zone)
        {
            IList<CalendarEvent> zoneEvents = new List<CalendarEvent>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EventByZoneNumberQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zone", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = zone;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CalendarEvent newEvent =  populateEventsFromDB(dr);
                            zoneEvents.Add(newEvent);

                        }
                    }
                }
            }

            return zoneEvents;
        }

        public IEnumerable<CalendarEvent> GetEventsByUserId(int userid)
        {
            IList<CalendarEvent> userEvents = new List<CalendarEvent>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EventByUserIdQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CalendarEvent newEvent = populateEventsFromDB(dr);
                            userEvents.Add(newEvent);

                        }
                    }
                }
            }

            return userEvents;
        }


        #region Helper Methods
        private static CalendarEvent populateEventsFromDB(Npgsql.NpgsqlDataReader dr)
        {
            CalendarEvent newEvent = new CalendarEvent();
            newEvent.id= Helper.ConvertFromDBVal<int>(dr[0]);
            newEvent.title = dr[1].ToString();
            newEvent.type =dr[2].ToString();
            newEvent.description = dr[3].ToString();
            newEvent.appointment = Helper.ConvertFromDBVal<Boolean>(dr[4]);
            newEvent.personal = Helper.ConvertFromDBVal<Boolean>(dr[5]);
            newEvent.city = dr[6].ToString();
            newEvent.state = dr[7].ToString();
            newEvent.zipcode = Helper.ConvertFromDBVal<int>(dr[8]);
            newEvent.zone = Helper.ConvertFromDBVal<int>(dr[9]);
            newEvent.map = dr[10].ToString();
            newEvent.creator = Helper.ConvertFromDBVal<int>(dr[11]);
            newEvent.assigned = Helper.ConvertFromDBVal<int>(dr[12]);
            newEvent.start = dr[13].ToString();
            newEvent.end = dr[14].ToString();
            newEvent.street= dr[15].ToString();
            newEvent.Parent_User_Id = Helper.ConvertFromDBVal<int>(dr[16]);
            newEvent.Parent_Appointment_Id = Helper.ConvertFromDBVal<int>(dr[17]);
            newEvent.Appointment_Reference = (dr[18]).ToString();

            return newEvent;
        }
        #endregion
    }
}
