using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGZoneRepository : IZoneRepository
    {
        #region Query Strings
        private string ZoneSelectQuery = @"SELECT zone_id, zone_number FROM zones order by zone_number ASC";
        private string ZipSelectQuery = @"SELECT zipcode_id, zipcode, zoneid from zipcodes";
        private string ZoneSelectonZipCodeQuery = @"SELECT zipcode_id, zipcode, zoneid FROM zipcodes AS z INNER JOIN zones AS zo ON z.zoneid = zo.zone_number WHERE zo.zone_number = :zone_id";
        private string ZipSelectionZoneQuery = @"select zoneid from zipcodes where zipcode = :zip";
        private string ZoneInsertQuery = @"INSERT INTO zones (zone_number) VALUES (:zone_number)";

        private string ZoneUpdateQuery = @"UPDATE zones SET zone_id = :zone_id, zone_number = :zone_number";

        private string ZoneDeleteQuery = @"DELETE FROM zones WHERE zone_id = :zoneid";
        private string ZipDeleteQuery = @"DELETE FROM zipcodes WHERE zipcode = :zipcodeid";
        private string ZipInsertquery = @"INSERT INTO zipcodes (zipcode, zoneid) VALUES (:zipcode, :zonenumber)";
        #endregion

        //  private static int counter = 1;

        
        public static IList<Domain.Zone> fakeZones = new List<Domain.Zone>();
        
        public IQueryable<Domain.Zone> Zones
        {
            get
            {
                fakeZones.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZoneSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Zone newZone = populateZoneFromDB(dr);
                                fakeZones.Add(newZone);
                                
                            }
                        }
                    }
                }
                return fakeZones.AsQueryable();

            }

        }

        public void SaveZone(Zone zone)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (zone.ZoneId > 0)
            {
                query = ZoneUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = ZoneInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zone_number", NpgsqlTypes.NpgsqlDbType.Integer));
                    


                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("zone_id", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value =zone.ZoneNumber;
                    

                    if (isUpdate)
                    {
                        command.Parameters[2].Value = zone.ZoneId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteZone(Zone zone)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZoneDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zoneid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = zone.ZoneId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public int GetZoneByZipcode(string zipcode)
        {
            string tranformedzip;
            //to get rid of -XXXX
            if (zipcode != "")
                tranformedzip = zipcode.Substring(0, 5);
            else
                tranformedzip = "00000";

            int zip = Convert.ToInt32(tranformedzip);

            int zone = 0;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZipSelectionZoneQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zip", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = zip;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        if(dr.Read() == true)
                            zone = (int)dr[0];

                        else
                            zone = 0;
            
                    }
                }
            }
            return zone;
        }

        public IEnumerable<ZipCodes> GetZipcodesByZone(int zone_id)
        {
            //This is a fairly generic db query with no parameters and pulling all values from reader and tossing data into User
            IList<ZipCodes> allZips = new List<ZipCodes>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZoneSelectonZipCodeQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter(":zone_id", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = zone_id;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ZipCodes newZipcode = populateZipcodesFromDB(dr);
                            allZips.Add(newZipcode);
                        }
                    }
                }
            }
            return allZips;
        }

        public void SaveZipCode(int zoneid, int zipcode)
        {

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZipInsertquery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zonenumber", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    var storezip = zipcode.ToString().Substring(0, 5);

                    command.Parameters[0].Value = storezip;
                    command.Parameters[1].Value = zoneid;


                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        #region Helper Methods
        private static Zone populateZoneFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Zone newZone = new Zone();
            newZone.ZoneId = Helper.ConvertFromDBVal<int>(dr[0]);
            newZone.ZoneNumber = Helper.ConvertFromDBVal<int>(dr[1]);
            return newZone;
        }

        public void DeleteZipCode(int zipcode)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZipDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcodeid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = zipcode;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        public ZipCodes GetZipCode(int zipcodeid)
        {
            ZipCodes zip = new ZipCodes();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(ZipSelectQuery, conn))
                {
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            zip = populateZipcodesFromDB(dr);

                        }
                    }
                }
            }
            return zip;

        }

        private static ZipCodes populateZipcodesFromDB(Npgsql.NpgsqlDataReader dr)
        {
            ZipCodes newZip = new ZipCodes();
            newZip.ZipCodeID = Helper.ConvertFromDBVal<int>(dr[0]);
            newZip.ZipCode = Helper.ConvertFromDBVal<int>(dr[1]);
            newZip.ZoneID = Helper.ConvertFromDBVal<int>(dr[2]);
            return newZip;
        }
      
        #endregion
    }
}
