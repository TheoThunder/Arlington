using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.IO;

namespace Data.Repositories.Static
{
    public class StaticLeadAccessRepository : ILeadAccessRepository
    {
        public IZoneRepository _ZoneRepository;
        public StaticLeadAccessRepository(IZoneRepository zonerepos)
        {
            _ZoneRepository = zonerepos;
        }

        //A Hardcoded list of leads.
        private static IList<Lead> fakeLeads = new List<Lead>();
        private static int counter = 1;

        public IQueryable<Domain.Lead> LeadAccessRecords
        {
            get { return fakeLeads.AsQueryable(); }
        }

       


        public void SaveLeadAccessRecord(string path)
        {
            Domain.ImportedLead[] parsedLeads = Helper.ParseCSVFile(path);

            string sql = "INSERT INTO lead (companyname, contact1title, contact1firstname, contact2title, contact2firstname, primaryphonenumber, additionalphonenumber, numbertocall, faxnumber, primaryemailaddress, additionalemailaddress, websitelink,  streetaddress1, streetaddress2, city, state, zipcode, zonenumber, status, assignedsauserid, callbackdate, ignoreddate, assignedaauserid, suppressed, contact1lastname, contact2lastname, ignored, dateimported, primaryphonechecked) VALUES (:companyname, :contact1title, :contact1firstname, :contact2title, :contact2firstname, :primaryphonenumber, :additionalphonenumber, :numbertocall, :faxnumber, :primaryemailaddress, :additionalemailaddress, :websitelink, :streetaddress1, :streetaddress2, :city, :state, :zipcode, :zonenumber, :status, :assignedsauserid, :callbackdate, :ignoreddate , :assignedaauserid, :suppressed, :contact1lastname, :contact2lastname, :ignored, :dateimported, :primaryphonechecked)";
  
          using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
          {
              conn.Open();
              Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(sql, conn);


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
              command.Parameters.Add(new Npgsql.NpgsqlParameter("callbackdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("ignoreddate", NpgsqlTypes.NpgsqlDbType.Timestamp));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedaauserid", NpgsqlTypes.NpgsqlDbType.Integer));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("suppressed", NpgsqlTypes.NpgsqlDbType.Boolean));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("contact1lastname", NpgsqlTypes.NpgsqlDbType.Text));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("contact2lastname", NpgsqlTypes.NpgsqlDbType.Text));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("ignored", NpgsqlTypes.NpgsqlDbType.Boolean));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("dateimported", NpgsqlTypes.NpgsqlDbType.Timestamp));
              command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryphonechecked", NpgsqlTypes.NpgsqlDbType.Boolean));
             //int leadid = command.Parameters.Count;
             //  System.Console.WriteLine(leadid);
              //System.Console.WriteLine(parsedData.Count);
              command.Prepare();
              foreach (Domain.ImportedLead parsedLead in parsedLeads)
              {
                  int zone = _ZoneRepository.GetZoneByZipcode(parsedLead.ZipCode);

                  command.Parameters["@companyname"].Value = parsedLead.CompanyName;
                  command.Parameters["@contact1firstname"].Value = parsedLead.ContactFirstName;
                  command.Parameters["@contact1lastname"].Value = parsedLead.ContactLastName;
                  command.Parameters["@contact1title"].Value = parsedLead.Title;
                  command.Parameters["@streetaddress1"].Value = parsedLead.Address;
                  command.Parameters["@city"].Value = parsedLead.City;
                  command.Parameters["@state"].Value = parsedLead.State;
                  command.Parameters["@zipcode"].Value = parsedLead.ZipCode;
                  command.Parameters["@primaryphonenumber"].Value = parsedLead.PrimaryPhoneNumber;
                  command.Parameters["@additionalphonenumber"].Value = parsedLead.AdditionalPhoneNumber;
                  command.Parameters["@faxnumber"].Value = parsedLead.FaxNumber;
                  command.Parameters["@primaryemailaddress"].Value = parsedLead.EmailAddress;
                  command.Parameters["@status"].Value = "Cold Lead";
                  command.Parameters["@assignedsauserid"].Value = 0;
                  command.Parameters["@assignedaauserid"].Value = 0;
                  command.Parameters["@zonenumber"].Value = zone;
                  command.Parameters["@ignored"].Value = false;
                  command.Parameters["@dateimported"].Value = DateTime.Now;
                  command.Parameters["@primaryphonechecked"].Value = true;
                  long x = command.ExecuteNonQuery();
        
              }

          }



            Lead lead = new Lead();
            // If it's a new lead, just add it to the list
            if (lead.LeadId == 0)
            {
                lead.LeadId = counter;
                counter += 1;
                fakeLeads.Add(lead);
            }
            else if (fakeLeads.Count(row => row.LeadId == lead.LeadId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteLeadAccessRecord(lead);
                fakeLeads.Add(lead);
            }

        }

        public void DeleteLeadAccessRecord(Domain.Lead lead)
        {
            var temp = fakeLeads.ToList();
            temp.RemoveAll(row => row.LeadId == lead.LeadId);
            fakeLeads = temp;
        }
        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeLeads.Clear();
        }
    }
}
