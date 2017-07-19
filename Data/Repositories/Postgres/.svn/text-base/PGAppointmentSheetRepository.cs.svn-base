using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGAppointmentSheetRepository : IAppointmentSheet
    {
        #region Query Strings
        private string AppointmentSelectQuery = @"SELECT appointmentid, addingservices, appointmentlocation, assignedsalesagent, city, comment, createdat, currentlyacceptedcards, currentprocessor, dateofappointment, howmanylocations, internet, lastupdated, moto, multilocation, newequipment, newsetup, price, score, singlelocation, state, street, swipe, unhappy, volume, zipcode, creator, parentlead, location, appointmentdatefrom, appointmentdateto, reschedule, creatorname, singleloccheck, event_reference FROM appointmentsheet ORDER BY CreatedAt";

        private string AppointmentSelectonLeadIDQuery = @"SELECT appointmentid, addingservices, appointmentlocation, assignedsalesagent, a.city, comment, createdat, currentlyacceptedcards, currentprocessor, dateofappointment, howmanylocations, internet, lastupdated, moto, multilocation, newequipment, newsetup, price, score, singlelocation, a.state, street, swipe, unhappy, volume, a.zipcode, creator, parentlead, location, appointmentdatefrom, appointmentdateto, reschedule, creatorname, singleloccheck, event_reference FROM appointmentsheet AS a WHERE a.parentlead = :leadid ORDER BY CreatedAt";

        private string AppointmentByAppointmentIdQuery = @"SELECT appointmentid, addingservices, appointmentlocation, assignedsalesagent, city, comment, createdat, currentlyacceptedcards, currentprocessor, dateofappointment, howmanylocations, internet, lastupdated, moto, multilocation, newequipment, newsetup, price, score, singlelocation, state, street, swipe, unhappy, volume, zipcode, creator, parentlead, location, appointmentdatefrom, appointmentdateto, reschedule, creatorname, singleloccheck, event_reference FROM appointmentsheet where appointmentid = :appointmentid";
        private string AppointmentInsertQuery = @"INSERT INTO appointmentsheet (addingservices, appointmentlocation, assignedsalesagent, city, comment, createdat, currentlyacceptedcards, currentprocessor, dateofappointment, howmanylocations, internet, lastupdated, moto, multilocation, newequipment, newsetup, price, score, singlelocation, state, street, swipe, unhappy, volume, zipcode, creator, parentlead, location, appointmentdatefrom, appointmentdateto, reschedule, creatorname, singleloccheck, event_reference) VALUES (:addingservices, :appointmentlocation, :assignedsalesagent, :city, :comment, :createdat, :currentlyacceptedcards, :currentprocessor, :dateofappointment, :howmanylocations, :internet, :lastupdated, :moto, :multilocation, :newequipment, :newsetup, :price, :score, :singlelocation, :state, :street, :swipe, :unhappy, :volume, :zipcode, :creator, :parentlead, :location, :appointmentdatefrom, :appointmentdateto, :reschedule, :creatorname, :singleloccheck, :event_reference)";

        private string AppointmentUpdateQuery = @"UPDATE appointmentsheet SET  addingservices = :addingservices, appointmentlocation = :appointmentlocation, assignedsalesagent = :assignedsalesagent, city = :city, comment = :comment, createdat = :createdat, currentlyacceptedcards = :currentlyacceptedcards, currentprocessor = :currentprocessor, dateofappointment = :dateofappointment, howmanylocations = :howmanylocations, internet = :internet, lastupdated = :lastupdated, moto = :moto, multilocation = :multilocation, newequipment = :newequipment, newsetup = :newsetup, price = :price, score =:score, singlelocation = :singlelocation, state = :state, street = :street, swipe = :swipe, unhappy = :unhappy, volume = :volume, zipcode = :zipcode, creator = :creator, parentlead = :parentlead, location = :location, appointmentdatefrom = :appointmentdatefrom, appointmentdateto = :appointmentdateto, reschedule = :reschedule, creatorname = :creatorname, singleloccheck = :singleloccheck, event_reference = :event_reference WHERE appointmentid = :appointmentid";

        private string AppointmentDeleteQuery = @"DELETE FROM appointmentsheet WHERE appointmentid = :appointmentid";

        private string CalendarAppointmentUpdateQuery = @"UPDATE appointmentsheet SET city = :city, dateofappointment = :dateofappointment, lastupdated = :lastupdated, state = :state, street = :street, zipcode = :zipcode,  appointmentdatefrom = :appointmentdatefrom, appointmentdateto = :appointmentdateto, event_reference = :event_reference WHERE appointmentid = :appointmentid";
        #endregion

        //  private static int counter = 1;
        public static IList<AppointmentSheet> fakeAppointment = new List<AppointmentSheet>();
        public IQueryable<AppointmentSheet> AppointmentSheets
        {
            get
            {
                fakeAppointment.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AppointmentSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                AppointmentSheet newAppointment = populateAppointmentFromDB(dr);
                                fakeAppointment.Add(newAppointment);
                            }
                        }
                    }
                }
                return fakeAppointment.AsQueryable();

            }

        }

        public void SaveAppointmentSheet(AppointmentSheet appointment)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (appointment.AppointmentSheetId > 0)
            {
                query = AppointmentUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = AppointmentInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("addingservices", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentlocation", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedsalesagent", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("comment", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("createdat", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("currentlyacceptedcards", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("currentprocessor", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dateofappointment", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("howmanylocations", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("internet", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("lastupdated", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("moto", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("multilocation", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("newequipment", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("newsetup", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("price", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("score", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("singlelocation", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("street", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("swipe", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("unhappy", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("volume", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("creator", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("parentlead", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("location", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentdateto", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("reschedule", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("creatorname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("singleloccheck", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("event_reference", NpgsqlTypes.NpgsqlDbType.Text));
                    //command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentzone", NpgsqlTypes.NpgsqlDbType.Integer));

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = appointment.AddingServices;
                    command.Parameters[1].Value = appointment.AppointmentLocation;
                    command.Parameters[2].Value = appointment.AssignedSalesAgent;
                    command.Parameters[3].Value = appointment.City;
                    command.Parameters[4].Value = appointment.Comment;
                    command.Parameters[5].Value = appointment.CreatedAt;
                    command.Parameters[6].Value = appointment.CurrentlyAcceptingCards;
                    command.Parameters[7].Value = appointment.CurrentProcessor;
                    command.Parameters[8].Value = appointment.DayOfAppointment;
                    command.Parameters[9].Value = appointment.HowManyLocations;
                    command.Parameters[10].Value = appointment.Internet;
                    command.Parameters[11].Value = appointment.LastUpdated;
                    command.Parameters[12].Value = appointment.Moto;
                    command.Parameters[13].Value = appointment.MultiLocation;
                    command.Parameters[14].Value = appointment.NewEquipment;
                    command.Parameters[15].Value = appointment.NewSetUp;
                    command.Parameters[16].Value = appointment.Price;
                    command.Parameters[17].Value = appointment.Score;
                    command.Parameters[18].Value = appointment.SingleLocation;
                    command.Parameters[19].Value = appointment.State;
                    command.Parameters[20].Value = appointment.Street;
                    command.Parameters[21].Value = appointment.Swipe;
                    command.Parameters[22].Value = appointment.Unhappy;
                    command.Parameters[23].Value = appointment.Volume;
                    command.Parameters[24].Value = appointment.ZipCode;
                    command.Parameters[25].Value = appointment.CreatorId;
                    command.Parameters[26].Value = appointment.ParentLeadId;
                    command.Parameters[27].Value = appointment.Location;
                    command.Parameters[28].Value = appointment.AppointmentDateFrom;
                    command.Parameters[29].Value = appointment.AppointmentDateTo;
                    command.Parameters[30].Value = appointment.Reschedule;
                    command.Parameters[31].Value = appointment.CreatorName;
                    command.Parameters[32].Value = appointment.SingleLocCheck;
                    command.Parameters[33].Value = appointment.Event_Reference;
                    //command.Parameters[34].Value = appointment.AppointmentZone;
                                       
                 

                    //command.Parameters[10].Value = card.Creator;





                    if (isUpdate)
                    {
                        command.Parameters[34].Value = appointment.AppointmentSheetId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        public void SaveAppointmentSheetFromCalendar(AppointmentSheet appointment)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (appointment.AppointmentSheetId > 0)
            {
                query = CalendarAppointmentUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = AppointmentInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    
                   
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dateofappointment", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("lastupdated", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("street", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentdatefrom", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentdateto", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("event_reference", NpgsqlTypes.NpgsqlDbType.Text));

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentid", NpgsqlTypes.NpgsqlDbType.Integer));
                 
                    command.Prepare();
                    command.Parameters[0].Value = appointment.City;
                    command.Parameters[1].Value = appointment.DayOfAppointment;
                    command.Parameters[2].Value = appointment.LastUpdated;
                    command.Parameters[3].Value = appointment.State;
                    command.Parameters[4].Value = appointment.Street;
                    command.Parameters[5].Value = appointment.ZipCode;
                    command.Parameters[6].Value = appointment.AppointmentDateFrom;
                    command.Parameters[7].Value = appointment.AppointmentDateTo;
                    command.Parameters[8].Value = appointment.Event_Reference;



                    //command.Parameters[10].Value = card.Creator;





                    if (isUpdate)
                    {
                        command.Parameters[9].Value = appointment.AppointmentSheetId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<AppointmentSheet> GetAppointmentByLeadId(int leadId)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            IList<Domain.AppointmentSheet> leadAppointments = new List<Domain.AppointmentSheet>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AppointmentSelectonLeadIDQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = leadId;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AppointmentSheet newAppointment = populateAppointmentFromDB(dr);
                            leadAppointments.Add(newAppointment);
                            
                        }
                    }
                }
            }

            return leadAppointments;
        }

        public void DeleteAppointmentSheet(AppointmentSheet appointment)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AppointmentDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = appointment.AppointmentSheetId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public AppointmentSheet GetAppointmentByAppointmentId(int appointmentid)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            Domain.AppointmentSheet newAppointment = new Domain.AppointmentSheet();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AppointmentByAppointmentIdQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = appointmentid;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            newAppointment = populateAppointmentFromDB(dr);

                        }
                    }
                }
            }

            return newAppointment;
        }

        #region Helper Methods
        private static AppointmentSheet populateAppointmentFromDB(Npgsql.NpgsqlDataReader dr)
        {
            AppointmentSheet newAppointment = new AppointmentSheet();
            newAppointment.AppointmentSheetId = Helper.ConvertFromDBVal<int>(dr[0]);
            newAppointment.AddingServices = Helper.ConvertFromDBVal<bool>(dr[1]);
            newAppointment.AppointmentLocation = dr[2].ToString();
            newAppointment.AssignedSalesAgent = Helper.ConvertFromDBVal<int>(dr[3]);
            newAppointment.City = dr[4].ToString();
            newAppointment.Comment = dr[5].ToString();
            newAppointment.CreatedAt = Helper.ConvertFromDBVal<DateTime>(dr[6]);
            newAppointment.CurrentlyAcceptingCards = Helper.ConvertFromDBVal<bool>(dr[7]);
            newAppointment.CurrentProcessor = dr[8].ToString();
            newAppointment.DayOfAppointment = Helper.ConvertFromDBVal<DateTime>(dr[9]);
            newAppointment.HowManyLocations = Helper.ConvertFromDBVal<int>(dr[10]);
            newAppointment.Internet = Helper.ConvertFromDBVal<bool>(dr[11]);
            newAppointment.LastUpdated = Helper.ConvertFromDBVal<DateTime>(dr[12]);
            newAppointment.Moto = Helper.ConvertFromDBVal<bool>(dr[13]);
            newAppointment.MultiLocation = Helper.ConvertFromDBVal<bool>(dr[14]);
            newAppointment.NewEquipment = Helper.ConvertFromDBVal<bool>(dr[15]);
            newAppointment.NewSetUp = Helper.ConvertFromDBVal<Boolean>(dr[16]);
            newAppointment.Price = Helper.ConvertFromDBVal<Boolean>(dr[17]);
            newAppointment.Score = dr[18].ToString();
            newAppointment.SingleLocation = Helper.ConvertFromDBVal<Boolean>(dr[19]);
            newAppointment.State = dr[20].ToString();
            newAppointment.Street = dr[21].ToString();
            newAppointment.Swipe = Helper.ConvertFromDBVal<bool>(dr[22]);
            newAppointment.Unhappy = Helper.ConvertFromDBVal<bool>(dr[23]);
            newAppointment.Volume = dr[24].ToString();
            newAppointment.ZipCode = Helper.ConvertFromDBVal<int>(dr[25]);
            newAppointment.CreatorId = Helper.ConvertFromDBVal<int>(dr[26]);
            newAppointment.ParentLeadId = Helper.ConvertFromDBVal<int>(dr[27]);
            newAppointment.Location = dr[28].ToString();
            newAppointment.AppointmentDateFrom = Helper.ConvertFromDBVal<DateTime>(dr[29]);
            newAppointment.AppointmentDateTo = Helper.ConvertFromDBVal<DateTime>(dr[30]);
            newAppointment.Reschedule = Helper.ConvertFromDBVal<bool>(dr[31]);
            newAppointment.CreatorName = dr[32].ToString();
            newAppointment.SingleLocCheck = Helper.ConvertFromDBVal<bool>(dr[33]);
            newAppointment.Event_Reference = (dr[34]).ToString();

            return newAppointment;
        }
        #endregion
    }
}
