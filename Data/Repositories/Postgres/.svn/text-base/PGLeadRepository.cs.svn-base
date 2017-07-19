using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGLeadRepository : ILeadRepository
    {
        #region Query Strings
        private string LeadSelectQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, contact1lastname, contact2lastname, suppressed, ignored, dateimported, reassigned, primaryphonechecked FROM lead";
        private string LeadInsertQuery = @"INSERT INTO lead (companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, reassigned, primaryphonechecked) VALUES (:companyname, :contact1title, :contact1firstname, :contact2title, :contact2firstname, :primaryphonenumber, :additionalphonenumber, :numbertocall, :faxnumber, :primaryemailaddress, :additionalemailaddress, :websitelink, :streetaddress1, :streetaddress2, :city, :state, :zipcode, :zonenumber, :status, :assignedsauserid, :callbackdate, :ignoreddate , :assignedaauserid, :suppressed, :contact1lastname, :contact2lastname, :ignored, :dateimported, :reassigned, :primaryphonechecked)";

        private string LeadUpdateQuery = @"UPDATE lead SET companyname = :companyname, contact1title = :contact1title, contact1firstname = :contact1firstname, contact2title = :contact2title, contact2firstname = :contact2firstname, primaryphonenumber = :primaryphonenumber, additionalphonenumber = :additionalphonenumber, numbertocall = :numbertocall, faxnumber = :faxnumber, primaryemailaddress = :primaryemailaddress, additionalemailaddress = :additionalemailaddress, websitelink = :websitelink, streetaddress1 = :streetaddress1, streetaddress2 = :streetaddress2, city = :city, state = :state, zipcode = :zipcode, zonenumber = :zonenumber, status = :status, assignedsauserid = :assignedsauserid, callbackdate = :callbackdate, ignoreddate = :ignoreddate, assignedaauserid = :assignedaauserid, suppressed = :suppressed , contact1lastname = :contact1lastname, contact2lastname = :contact2lastname, ignored = :ignored, dateimported = :dateimported, reassigned = :reassigned, primaryphonechecked = :primaryphonechecked WHERE leadid = :leadid";

        private string LeadDeleteQuery = @"DELETE FROM lead WHERE leadid = :leadid";
        private string LeadByLeadIDSelectQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, contact1lastname, contact2lastname, suppressed, ignored, dateimported, reassigned, primaryphonechecked FROM lead where leadid = :leadid";
		private string LeadByStatusSelectQuery = @"SELECT leadid, companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, contact1lastname, contact2lastname, suppressed, ignored, dateimported, reassigned, primaryphonechecked FROM lead where status = :status and ignored = false and AssignedAAUserId = :userId";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Lead> fakeLeads = new List<Domain.Lead>();
        public IQueryable<Domain.Lead> Leads
        {
            
            get
            {
                fakeLeads.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(LeadSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Lead newLead = populateLeadFromDB(dr);
                                fakeLeads.Add(newLead);
                            }
                        }
                    }
                }
                return fakeLeads.AsQueryable();
                
            }
            
        }

        public IQueryable<Domain.Lead> LeadByStatus(string status, int userId)
        {
                fakeLeads.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(LeadByStatusSelectQuery, conn))
                    {
                        command.Parameters.AddWithValue("status", status);
						command.Parameters.AddWithValue("userId", userId);
                        command.Prepare();

                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Lead newLead = populateLeadFromDB(dr);
                                fakeLeads.Add(newLead);
                            }
                        }
                    }
                }
                return fakeLeads.AsQueryable();

        }

        public Domain.Lead LeadByLeadID(int leadid)
        {

                Lead newLead = new Lead();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(LeadByLeadIDSelectQuery, conn))
                    {
                        
                            command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));
                            command.Prepare();
                            command.Parameters[0].Value = leadid;
                            using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.Read())
                                    newLead = populateLeadFromDB(dr);
                                //IF there is more than one row coming back for an id, than we should toss an exception
                                if (dr.Read())
                                    throw new InvalidOperationException("More than one user came back when Querying by UserId");
                            }
                        
                    }
                }
                return newLead;

        }


        public void SaveLead(Domain.Lead lead)
        {
            string query;
            bool isUpdate = false;
           // Want to know right off the bat if we're doing a insert or update
            if (lead.LeadId > 0)
            {
                query = LeadUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = LeadInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
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

                    if (isUpdate)
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
                    command.Parameters[22].Value = lead.AssignedAAUserId;
                    
                    command.Parameters[23].Value = lead.Contact1LastName;
                    command.Parameters[24].Value = lead.Contact2LastName;
                    command.Parameters[25].Value = lead.Suppressed;

                    command.Parameters[26].Value = lead.Ignored;
                    command.Parameters[27].Value = lead.DateTimeImported;
                    command.Parameters[28].Value = lead.Reassigned;
                    command.Parameters[29].Value = lead.PrimaryPhoneChecked;


                    if (isUpdate)
                    {
                        command.Parameters[30].Value = lead.LeadId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

            if (lead.NewFilePath != null && lead.NewFilePath != string.Empty)
            {
                PGUploadedfileRepository upFile = new PGUploadedfileRepository();
                upFile.InsertLeadFile(lead.LeadId, lead.NewFilePath);
            }
            
        }

        public void DeleteLead(Domain.Lead lead)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(LeadDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = lead.LeadId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
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
            
            newUser.Contact1LastName = dr[24].ToString();
            newUser.Contact2LastName = dr[25].ToString();
            newUser.Suppressed = Helper.ConvertFromDBVal<Boolean>(dr[26]);
            newUser.Ignored = Helper.ConvertFromDBVal<Boolean>(dr[27]);
            newUser.DateTimeImported = Helper.ConvertFromDBVal<DateTime>(dr[28]);
            newUser.Reassigned = Helper.ConvertFromDBVal<Boolean>(dr[29]);
            newUser.PrimaryPhoneChecked = Helper.ConvertFromDBVal<Boolean>(dr[30]);

            PGUploadedfileRepository upFile = new PGUploadedfileRepository();
            //newUser.StatementFile = upFile.UploadedFiles.Single(row => row.leadID == newUser.LeadId);
            newUser.StatementFiles.AddRange(upFile.GetFileByLeadId(newUser.LeadId));

            return newUser;
        }

       #endregion
    }
}
