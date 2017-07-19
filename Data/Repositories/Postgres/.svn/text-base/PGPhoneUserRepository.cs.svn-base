using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGPhoneUserRepository : IPhoneUserRepository
    {
        #region Query Strings
        private string UserSelectQuery = @"SELECT phoneuserid, extension, firstname, middlename, lastname, crmuserid, 
                 accountid, voicemailid, email, date_created, extension_server_uuid, username, password
                 FROM phone_user where crmuserid = :userid;";
        private string UserInsertQuery = @"INSERT INTO phone_user (extension, firstname, middlename, lastname, crmuserid,
                 accountid, voicemailid, email, date_created, extension_server_uuid, username, password) VALUES (  :extension,
                 :firstname, :middlename, :lastname, :crmuserid, :accountid, :voicemailid, :email, :date_created, :extension_server_uuid,
                 :username, :password)";
        private string UserUpdateQuery = @"UPDATE phone_user SET extension = :extension, firstname = :firstname,
                 middlename = :middlename, lastname = :lastname, accountid = :accountid, voicemailid = :voicemailid,
                 email = :email, date_created = :date_created, extension_server_uuid = :extension_server_uuid, username = :username,
                 password = :password WHERE crmuserid = :crmuserid";
                 
        #endregion

        //  private static int counter = 1;

       public PhoneUser GetPhoneUser(int userid)
       {
           PhoneUser newPhoneuser = new PhoneUser();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(UserSelectQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = userid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            newPhoneuser = populatePhoneUserFromDB(dr);
                            
                        }
                    }
                }
            }
            return newPhoneuser;

        }

        //save phone user to DB
        public void SavePhoneUser(PhoneUser phoneuser)
        {
            string query;
            bool isUpdate = false;
            // check for insert or update
            if(phoneuser.PhoneUserId > 0 )
            {
                query = UserUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = UserInsertQuery;
            }
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("extension", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("firstname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("middlename", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("lastname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("crmuserid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("voicemailid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("email", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("date_created", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("extension_server_uuid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("password", NpgsqlTypes.NpgsqlDbType.Text));
       
                    command.Prepare();

                    command.Parameters[0].Value = phoneuser.Extension;
                    command.Parameters[1].Value = phoneuser.FirstName;
                    command.Parameters[2].Value = phoneuser.MiddleName;
                    command.Parameters[3].Value = phoneuser.LastName;
                    command.Parameters[4].Value = phoneuser.CRMUserId;
                    command.Parameters[5].Value = phoneuser.AccountId;
                    command.Parameters[6].Value = phoneuser.VoiceMailId;
                    command.Parameters[7].Value = phoneuser.Email;
                    command.Parameters[8].Value = phoneuser.Date_Created;
                    command.Parameters[9].Value = phoneuser.Extension_Server_UUID;
                    command.Parameters[10].Value = phoneuser.UserName;
                    command.Parameters[11].Value = phoneuser.Password;
                      
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        #region Helper Methods
        private static PhoneUser populatePhoneUserFromDB(Npgsql.NpgsqlDataReader dr)
        {
            PhoneUser newUser = new PhoneUser();
            newUser.PhoneUserId = Helper.ConvertFromDBVal<int>(dr[0]);
            newUser.Extension = Helper.ConvertFromDBVal<int>(dr[1]);
            newUser.FirstName = dr[2].ToString();
            newUser.MiddleName = dr[3].ToString();
            newUser.LastName = dr[4].ToString();
            newUser.CRMUserId= Helper.ConvertFromDBVal<int>(dr[5]);
            newUser.AccountId = Helper.ConvertFromDBVal<int>(dr[6]);
            newUser.VoiceMailId = Helper.ConvertFromDBVal<int>(dr[7]);
            newUser.Email = (dr[8]).ToString();
            newUser.Date_Created = Helper.ConvertFromDBVal<DateTime>(dr[9]);
            newUser.Extension_Server_UUID = Helper.ConvertFromDBVal<int>(dr[10]);
            newUser.UserName = dr[11].ToString();
            newUser.Password = dr[12].ToString();

            return newUser;
        }
        #endregion
    }
}
