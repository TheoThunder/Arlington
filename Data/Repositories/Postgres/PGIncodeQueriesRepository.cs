using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGIncodeQueriesRepository : IGenericUsageRepositoryInterface
    {
        #region Query Strings
        private string GetLeadsQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, reassigned, primaryphonechecked FROM lead where leadid = :leadid";
        private string GetAppointmentQuery = @"SELECT appointmentid, addingservices, appointmentlocation, assignedsalesagent, city, comment, createdat, currentlyacceptedcards, currentprocessor, dateofappointment, howmanylocations, internet, lastupdated, moto, multilocation, newequipment, newsetup, price, score, singlelocation, state, street, swipe, unhappy, volume, zipcode, creator, parentlead, location, appointmentdatefrom, appointmentdateto FROM appointmentsheet where appointmentid = :appointmentsheetid";
        private string GetAppointmentQueueQuery = @"SELECT appointmentid, addingservices, appointmentlocation, assignedsalesagent, city, comment, createdat, currentlyacceptedcards, currentprocessor, dateofappointment, howmanylocations, internet, lastupdated, moto, multilocation, newequipment, newsetup, price, score, singlelocation, state, street, swipe, unhappy, volume, zipcode, creator, parentlead, location, appointmentdatefrom, appointmentdateto FROM appointmentsheet where assignedsalesagent = :salesagent AND reschedule = :reschedule";
        private string LeadUpdateQuery = @"UPDATE lead SET companyname = :companyname, contact1title = :contact1title, contact1firstname = :contact1firstname, contact2title = :contact2title, contact2firstname = :contact2firstname, primaryphonenumber = :primaryphonenumber, additionalphonenumber = :additionalphonenumber, numbertocall = :numbertocall, faxnumber = :faxnumber, primaryemailaddress = :primaryemailaddress, additionalemailaddress = :additionalemailaddress, websitelink = :websitelink, streetaddress1 = :streetaddress1, streetaddress2 = :streetaddress2, city = :city, state = :state, zipcode = :zipcode, zonenumber = :zonenumber, status = :status, assignedsauserid = :assignedsauserid, callbackdate = :callbackdate, ignoreddate = :ignoreddate, assignedaauserid = :assignedaauserid, suppressed = :suppressed , contact1lastname = :contact1lastname, contact2lastname = :contact2lastname, ignored = :ignored, dateimported = :dateimported, reassigned = :reassigned, primaryphonechecked = :primaryphonechecked WHERE leadid = :leadid";
        private string SelectByUsernameQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users WHERE last_name = :lastname AND first_name = :firstname";

        private string GetLeadsByCardTypeQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, l.reassigned, primaryphonechecked FROM lead as l INNER JOIN card as c ON l.leadid = c.parentleadid WHERE c.cardtype = :cardtype AND l.suppressed = 'false'";
        private string AppointmentUpdateQuery = @"UPDATE appointmentsheet SET addingservices = :addingservices, appointmentlocation = :appointmentlocation, assignedsalesagent = :assignedsalesagent, city = :city, comment = :comment, createdat = :createdat, currentlyacceptedcards = :currentlyacceptedcards, currentprocessor = :currentprocessor, dateofappointment = :dateofappointment, howmanylocations = :howmanylocations, internet = :internet, lastupdated = :lastupdated, moto = :moto, multilocation = :multilocation, newequipment = :newequipment, newsetup = :newsetup, price = :price, score =:score, singlelocation = :singlelocation, state = :state, street = :street, swipe = :swipe, unhappy = :unhappy, volume = :volume, zipcode = :zipcode, creator = :creator, parentlead = :parentlead, location = :location, appointmentdatefrom = :appointmentdatefrom, appointmentdateto = :appointmentdateto WHERE appointmentid = :appointmentid";

        private string GetUnAssignedLeadsQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, reassigned, primaryphonechecked FROM lead where ignored = false and assignedsauserid = 0 and assignedaauserid = 0";
        private string GetIngoredLeadsQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, reassigned, primaryphonechecked FROM lead where ignored = true";
		private string GetWarmLeadsQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, reassigned, primaryphonechecked FROM lead where status = 'Warm Lead' OR status = 'Customer' and AssignedAAUserId = coalesce(:userId, AssignedAAUserId)";

        private string GetSalesAndAppointmentAgentsQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users WHERE assigned_role_id = 3 OR assigned_role_id = 4";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Lead> assignableLeads = new List<Domain.Lead>();
        public static IList<Domain.Lead> unassignedLeads = new List<Domain.Lead>();
        public static IList<Domain.Lead> ignoredLeads = new List<Domain.Lead>();
        public static IList<Domain.Lead> warmLeads = new List<Domain.Lead>();
        public static IList<User> userList = new List<User>();


        public static IList<Domain.AppointmentSheet> assignableAppointments = new List<Domain.AppointmentSheet>();
        public IList<Lead> Assignleads(int leadid)
        {
               assignableLeads.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetLeadsQuery, conn))
                    {
                        command.Parameters.Add(new Npgsql.NpgsqlParameter(":leadid", NpgsqlTypes.NpgsqlDbType.Integer));
                        command.Prepare();
                        command.Parameters[0].Value = leadid;
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Lead newLead = populateLeadFromDB(dr);
                               assignableLeads.Add(newLead);
                            }
                        }
                    }
                }
                return assignableLeads;     
        }

        public IList<AppointmentSheet> AssignAppointments(int appointmentid)
        {
            assignableAppointments.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetAppointmentQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter(":appointmentsheetid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = appointmentid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AppointmentSheet newAppointment = populateAppointmentFromDB(dr);
                            assignableAppointments.Add(newAppointment);
                        }
                    }
                }
            }
            return assignableAppointments;
        }

        public IList<AppointmentSheet> AppointmentQueue(int said, Boolean reschedule)
        {
            assignableAppointments.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetAppointmentQueueQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter(":salesagent", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter(":reschedule", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Prepare();
                    command.Parameters[0].Value = said;
                    command.Parameters[1].Value = reschedule;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AppointmentSheet newAppointment = populateAppointmentFromDB(dr);
                            assignableAppointments.Add(newAppointment);
                        }
                    }
                }
            }
            return assignableAppointments;
        }

        public void SaveAssignedAALeads(Lead lead, int userid)
        {
         
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(LeadUpdateQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("companyname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1title", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1firstname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2title", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2firstname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryphonenumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("additionalphonenumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("numbertocall", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("faxnumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryemailaddress", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("additionalemailaddress", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("websitelink", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("streetaddress1", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("streetaddress2", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zonenumber", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("status", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedsauserid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("callbackdate", NpgsqlTypes.NpgsqlDbType.Date));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ignoreddate", NpgsqlTypes.NpgsqlDbType.Date));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedaauserid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1lastname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2lastname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("suppressed", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ignored", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dateimported", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("reassigned", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryphonechecked", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = lead.CompanyName;
                    command.Parameters[1].Value = lead.Contact1Title;
                    command.Parameters[2].Value = lead.Contact1FirstName;
                    command.Parameters[3].Value = lead.Contact2Title;
                    command.Parameters[4].Value = lead.Contact2FirstName;
                    command.Parameters[5].Value = lead.PrimaryPhoneNumber;
                    command.Parameters[6].Value = lead.AddtionalPhoneNumber;
                    command.Parameters[7].Value = lead.NumberToCall;
                    command.Parameters[8].Value = lead.FaxNumber;
                    command.Parameters[9].Value = lead.PrimaryEmailAddress;
                    command.Parameters[10].Value = lead.AdditonalEmailAddress;
                    command.Parameters[11].Value = lead.WebsiteLink;
                    command.Parameters[12].Value = lead.StreetAddress1;
                    command.Parameters[13].Value = lead.StreetAddress2;
                    command.Parameters[14].Value = lead.City;
                    command.Parameters[15].Value = lead.State;
                    command.Parameters[16].Value = lead.ZipCode;
                    command.Parameters[17].Value = lead.ZoneNumber;
                    command.Parameters[18].Value = lead.Status;
                    command.Parameters[19].Value = lead.AssignedSAUserId;
                    command.Parameters[20].Value = lead.CallbackDate;
                    command.Parameters[21].Value = lead.IgnoredDate;
                    command.Parameters[22].Value = userid;
                    command.Parameters[23].Value = lead.Contact1LastName;
                    command.Parameters[24].Value = lead.Contact2LastName;
                    command.Parameters[25].Value = lead.Suppressed;
                    command.Parameters[26].Value = lead.Ignored;
                    command.Parameters[27].Value = lead.DateTimeImported;
                    command.Parameters[28].Value = lead.Reassigned;
                    command.Parameters[29].Value = lead.PrimaryPhoneChecked;
                    command.Parameters[30].Value = lead.LeadId;

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
  
        }

        public void SaveAssignedSALeads(Lead lead, int userid)
        {

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(LeadUpdateQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("companyname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1title", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1firstname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2title", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2firstname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryphonenumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("additionalphonenumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("numbertocall", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("faxnumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryemailaddress", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("additionalemailaddress", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("websitelink", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("streetaddress1", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("streetaddress2", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zonenumber", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("status", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedsauserid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("callbackdate", NpgsqlTypes.NpgsqlDbType.Date));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ignoreddate", NpgsqlTypes.NpgsqlDbType.Date));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedaauserid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1lastname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2lastname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("suppressed", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ignored", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dateimported", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("reassigned", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryphonechecked", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = lead.CompanyName;
                    command.Parameters[1].Value = lead.Contact1Title;
                    command.Parameters[2].Value = lead.Contact1FirstName;
                    command.Parameters[3].Value = lead.Contact2Title;
                    command.Parameters[4].Value = lead.Contact2FirstName;
                    command.Parameters[5].Value = lead.PrimaryPhoneNumber;
                    command.Parameters[6].Value = lead.AddtionalPhoneNumber;
                    command.Parameters[7].Value = lead.NumberToCall;
                    command.Parameters[8].Value = lead.FaxNumber;
                    command.Parameters[9].Value = lead.PrimaryEmailAddress;
                    command.Parameters[10].Value = lead.AdditonalEmailAddress;
                    command.Parameters[11].Value = lead.WebsiteLink;
                    command.Parameters[12].Value = lead.StreetAddress1;
                    command.Parameters[13].Value = lead.StreetAddress2;
                    command.Parameters[14].Value = lead.City;
                    command.Parameters[15].Value = lead.State;
                    command.Parameters[16].Value = lead.ZipCode;
                    command.Parameters[17].Value = lead.ZoneNumber;
                    command.Parameters[18].Value = lead.Status;
                    command.Parameters[19].Value = userid;
                    command.Parameters[20].Value = lead.CallbackDate;
                    command.Parameters[21].Value = lead.IgnoredDate;
                    command.Parameters[22].Value = lead.AssignedAAUserId;
                    command.Parameters[23].Value = lead.Contact1LastName;
                    command.Parameters[24].Value = lead.Contact2LastName;
                    command.Parameters[25].Value = lead.Suppressed;
                    command.Parameters[26].Value = lead.Ignored;
                    command.Parameters[27].Value = lead.DateTimeImported;
                    command.Parameters[28].Value = lead.Reassigned;
                    command.Parameters[29].Value = lead.PrimaryPhoneChecked;
                    command.Parameters[30].Value = lead.LeadId;

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

        }
        public User GetUserIDByName(string lastname, string firstname)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            User newUser = null;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectByUsernameQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("lastname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("firstname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = lastname;
                    command.Parameters[1].Value = firstname;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                            newUser = populateUserFromDB(dr);
                        //IF there is more than one row coming back for an id, than we should toss an exception
                        if (dr.Read())
                            throw new InvalidOperationException("More than one user came back when Querying by Username");
                    }
                }
			}
            return newUser;
        }

        public IList<Lead> GetLeadByCardType(string cardtype)
        {
            IList<Domain.Lead> getLeads = new List<Domain.Lead>();
            getLeads.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetLeadsByCardTypeQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter(":cardtype", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = cardtype;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lead newLead = populateLeadFromDB(dr);
                            getLeads.Add(newLead);
                        }
                    }
                }
            }
            return getLeads;
        }

        public void SaveAssignedAppointments(AppointmentSheet appointment, int userid)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AppointmentUpdateQuery, conn))
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

                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = appointment.AddingServices;
                    command.Parameters[1].Value = appointment.AppointmentLocation;
                    command.Parameters[2].Value = userid;
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

                    command.Parameters[30].Value = appointment.AppointmentSheetId;

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public IList<Lead> UnAssignedleads()
        {
            unassignedLeads.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetUnAssignedLeadsQuery, conn))
                {
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lead newLead = populateLeadFromDB(dr);
                            unassignedLeads.Add(newLead);
                        }
                    }
                }
            }
            return unassignedLeads;
        }
        public IList<Lead> Ignoredleads()
        {
            ignoredLeads.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetIngoredLeadsQuery, conn))
                {
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lead newLead = populateLeadFromDB(dr);
                            ignoredLeads.Add(newLead);
                        }
                    }
                }
            }
            return ignoredLeads;
        }

        public IList<Lead> GetWarmLeads(int userId)
        {
            warmLeads.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetWarmLeadsQuery, conn))
                {
					if(userId == -1)
						command.Parameters.AddWithValue("userId", null);
					else
						command.Parameters.AddWithValue("userId", userId);
					command.Prepare();

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lead newLead = populateLeadFromDB(dr);
                            warmLeads.Add(newLead);
                        }
                    }
                }
            }
            return warmLeads;
        }

        public IList<User> GetAllSalesAndAppointmentAgents()
        {
            userList.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetSalesAndAppointmentAgentsQuery, conn))
                {
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            User newUser = populateUserFromDB(dr);
                            userList.Add(newUser);
                        }
                    }
                }
            }
            return userList;
        }

        #region Helper Methods
        private static Lead populateLeadFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Lead newUser = new Lead();
            newUser.LeadId = Helper.ConvertFromDBVal<int>(dr[0]);
            newUser.CompanyName = dr[1].ToString();
            newUser.Contact1Title = dr[2].ToString();
            newUser.Contact1FirstName = dr[3].ToString();
            newUser.Contact2Title = dr[4].ToString();
            newUser.Contact2FirstName = dr[5].ToString();
            newUser.PrimaryPhoneNumber = dr[6].ToString();
            newUser.AddtionalPhoneNumber = dr[7].ToString();
            newUser.NumberToCall = Helper.ConvertFromDBVal<int>(dr[8]);
            newUser.FaxNumber = dr[9].ToString();
            newUser.PrimaryEmailAddress = dr[10].ToString();
            newUser.AdditonalEmailAddress = dr[11].ToString();
            newUser.WebsiteLink = dr[12].ToString();
            newUser.StreetAddress1 = dr[13].ToString();
            newUser.StreetAddress2 = dr[14].ToString();
            newUser.City = dr[15].ToString();
            newUser.State = dr[16].ToString();
            newUser.ZipCode = dr[17].ToString();
            newUser.ZoneNumber = Helper.ConvertFromDBVal<int>(dr[18]);
            newUser.Status = dr[19].ToString();
            newUser.AssignedSAUserId = Helper.ConvertFromDBVal<int>(dr[20]);
            newUser.CallbackDate = Helper.ConvertFromDBVal<DateTime>(dr[21]);
            newUser.IgnoredDate = Helper.DateConvertFromDBVal<DateTime>(dr[22]);
            newUser.AssignedAAUserId = Helper.ConvertFromDBVal<int>(dr[23]);
            newUser.Suppressed = Helper.ConvertFromDBVal<Boolean>(dr[24]);
            newUser.Contact1LastName = dr[25].ToString();
            newUser.Contact2LastName = dr[26].ToString();
            newUser.Ignored = Helper.ConvertFromDBVal<Boolean>(dr[27]);
            newUser.DateTimeImported = Helper.ConvertFromDBVal<DateTime>(dr[28]);
            newUser.Reassigned = Helper.ConvertFromDBVal<Boolean>(dr[29]);
            newUser.PrimaryPhoneChecked = Helper.ConvertFromDBVal<Boolean>(dr[30]);

            PGUploadedfileRepository upFile = new PGUploadedfileRepository();
            //newUser.StatementFile = upFile.UploadedFiles.Single(row => row.leadID == newUser.LeadId);
            newUser.StatementFiles.AddRange(upFile.GetFileByLeadId(newUser.LeadId));

            return newUser;
        }
       
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

            return newAppointment;
        }

        private static User populateUserFromDB(Npgsql.NpgsqlDataReader dr)
        {
            // We are using ToString() here instead of (string)dr[] to fight DBNulls
            User newUser = new User();
            newUser.UserId = Helper.ConvertFromDBVal<int>(dr[0]);
            newUser.UserName = dr[1].ToString();
            newUser.Password = dr[2].ToString();
            newUser.FirstName = dr[3].ToString();
            newUser.MiddleName = dr[4].ToString();
            newUser.LastName = dr[5].ToString();
            newUser.Address1 = dr[6].ToString();
            newUser.Address2 = dr[7].ToString();
            newUser.City = dr[8].ToString();
            newUser.State = dr[9].ToString();
            newUser.ZipCode = dr[10].ToString();
            newUser.AssignedRoleId = Helper.ConvertFromDBVal<int>(dr[11]);
            newUser.OfficeNumber = Helper.ConvertFromDBVal<int>(dr[12]);
            newUser.SalesRepNumber = Helper.ConvertFromDBVal<int>(dr[13]);
            newUser.CalendarColor = dr[14].ToString();
            newUser.PhoneNumberOne = dr[15].ToString();
            newUser.PhoneNumberTwo = dr[16].ToString();
            newUser.FaxNumber = dr[17].ToString();
            newUser.HourlyRate = Helper.ConvertFromDBVal<Single>(dr[18]);
            newUser.EmailOne = dr[19].ToString();
            newUser.EmailTwo = dr[20].ToString();
            newUser.IsActive = Helper.ConvertFromDBVal<Boolean>(dr[21]);
            newUser.TeamNumber = Helper.ConvertFromDBVal<int>(dr[22]);
            return newUser;
        }
                
        #endregion
    }
}
