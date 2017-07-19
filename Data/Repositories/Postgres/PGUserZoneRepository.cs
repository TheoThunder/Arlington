using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGUserZoneRepository : IUserZoneRepository
    {
        #region Query Strings
        private string GetUserByZoneQuery = @"Select user_id, zone_id, userzoneid from userzones WHERE zone_id = :zoneid";
        private string GetZoneByUserQuery = @"Select user_id, zone_id, userzoneid from userzones WHERE user_id = :userid";
        private string InsertUserZoneQuery = @"Insert into userzones (user_id, zone_id) values (:userid, :zoneid)";
        private string UpdateUserZoneQuery = @"Update userzones set values user_id = :userid, zone_id = :zoneid Where userzoneid = :userzoneid";
        private string DeleteUserZoneQuery = @"Delete from userzones where userzoneid = :userzoneid";
        private string DeleteUserZoneByUserIDQuery = @"Delete from userzones where user_id = :userid";
        #endregion


        public IEnumerable<UserZone> GetAllUsersByZone(int zoneid)
        {
            IList<UserZone> users = new List<UserZone>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetUserByZoneQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zoneid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = zoneid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var temp = new UserZone() { UserID = Helper.ConvertFromDBVal<int>(dr[0]), ZoneId = Helper.ConvertFromDBVal<int>(dr[1]) };
                            users.Add(temp);
                        }
                    }
                }
            }
            return users;
        }

        public IEnumerable<UserZone> GetAllZonesByUser(int userid)
        {
            IList<UserZone> zones = new List<UserZone>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetZoneByUserQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var temp = new UserZone() { UserID = Helper.ConvertFromDBVal<int>(dr[0]), ZoneId = Helper.ConvertFromDBVal<int>(dr[1]) };
                            zones.Add(temp);
                        }
                    }
                }
            }
            return zones;

        }

        public void SaveUserZone(UserZone userzone)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (userzone.UserZoneID > 0)
            {
                query = UpdateUserZoneQuery;
                isUpdate = true;
            }
            else
            {
                query = InsertUserZoneQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zoneid", NpgsqlTypes.NpgsqlDbType.Integer));
                  
                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("userzoneid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = userzone.UserID;
                    command.Parameters[1].Value = userzone.ZoneId;

                    if (isUpdate)
                    {
                        command.Parameters[2].Value = userzone.UserZoneID;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserZone(UserZone userzone)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(DeleteUserZoneQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userzoneid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = userzone.UserZoneID;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserZoneByUserID(int userzoneid)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(DeleteUserZoneByUserIDQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = userzoneid;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
    }

    
}
