using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
	public class PGUserRepository : IUserRepository
	{
		#region Query Strings
		private string SelectAllQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users";
		private string SelectAllByRoleQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users AS u INNER JOIN roles AS r ON u.assigned_role_id = r.role_id WHERE r.name = :role_name";
		private string SelectAllByRoleIDQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users WHERE assigned_role_id = :roleid";
		private string SelectAllByTeamNumberQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users WHERE team = :team AND (assigned_role_id = 4 OR assigned_role_id = 3)";
		private string SelectByUserIdQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users WHERE user_id = :user_id";

		private string SelectByUsernameQuery = @"SELECT user_id, username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team FROM users WHERE username = :username";

		private string InsertQuery = @"INSERT INTO users (username, password, first_name, middle_name, last_name, address1, address2, city, state, zipcode, assigned_role_id, office_number, sales_rep_number, calendar_color, phone1, phone2, faxnumber, wage, email1, email2, isactive, team) VALUES
									(:username, :password, :first_name, :middle_name, :last_name, :address1, :address2, :city, :state, :zipcode, :assigned_role_id, :office_number, :sales_rep_number, :calendar_color, :phone1, :phone2, :faxnumber, :wage, :email1, :email2, :isactive, :team)";

		private string UpdateQuery = @"UPDATE users SET username = :username, password = :password, first_name = :first_name, middle_name = :middle_name,
											last_name = :last_name, address1 = :address1, address2 = :address2, city = :city, state = :state, zipcode = :zipcode,
											assigned_role_id = :assigned_role_id, office_number = :office_number, sales_rep_number = :sales_rep_number, calendar_color = :calendar_color, phone1 = :phone1, phone2 = :phone2, faxnumber = :faxnumber, wage = :wage, email1 = :email1, email2 = :email2, isactive = :isactive, team = :team
									   WHERE user_id = :user_id";
		private string UpdateNonPasswordQuery = @"UPDATE users SET username = :username, first_name = :first_name, middle_name = :middle_name,
											last_name = :last_name, address1 = :address1, address2 = :address2, city = :city, state = :state, zipcode = :zipcode,
											assigned_role_id = :assigned_role_id, office_number = :office_number, sales_rep_number = :sales_rep_number, calendar_color = :calendar_color, phone1 = :phone1, phone2 = :phone2, faxnumber = :faxnumber, wage = :wage, email1 = :email1, email2 = :email2, isactive = :isactive, team = :team
									   WHERE user_id = :user_id";

		private string DeleteQuery = @"DELETE FROM users WHERE user_id = :user_id";

		private string GetCalendarColorQuery = @"Select calendar_color from users Where user_id = :userid";
		#endregion

		/// <summary>
		/// Get's all users from database.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<User> GetAllUsers()
		{
			//This is a fairly generic db query with no parameters and pulling all values from reader and tossing data into User
			IList<User> allUsers = new List<User>();
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectAllQuery, conn))
				{
					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						while (dr.Read())
						{
							User newUser = populateUserFromDB(dr);
							allUsers.Add(newUser);
						}
					}
				}
			}
			return allUsers;
		}

		public IEnumerable<User> GetAllUsersByPermission(string RoleName)
		{
			//This is a fairly generic db query with no parameters and pulling all values from reader and tossing data into User
			IList<User> allUsers = new List<User>();
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectAllByRoleQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("role_name", NpgsqlTypes.NpgsqlDbType.Text));
					command.Prepare();
					command.Parameters[0].Value = RoleName;
					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						while (dr.Read())
						{
							User newUser = populateUserFromDB(dr);
							allUsers.Add(newUser);
						}
					}
				}
			}
			return allUsers;
		}

		public IEnumerable<User> GetAllUsersByTeam(int TeamNumber)
		{
			//This is a fairly generic db query with no parameters and pulling all values from reader and tossing data into User
			IList<User> allUsers = new List<User>();
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectAllByTeamNumberQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("team", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Prepare();
					command.Parameters[0].Value = TeamNumber;
					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						while (dr.Read())
						{
							User newUser = populateUserFromDB(dr);
							allUsers.Add(newUser);
						}
					}
				}
			}
			return allUsers;
		}
		//Return Users with roleid in database.

		public IEnumerable<User> GetAllUsersByRole(int roleid)
		{
			//This is a fairly generic db query with no parameters and pulling all values from reader and tossing data into User
			IList<User> allUsers = new List<User>();
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectAllByRoleIDQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Prepare();
					command.Parameters[0].Value = roleid;
					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						while (dr.Read())
						{
							User newUser = populateUserFromDB(dr);
							allUsers.Add(newUser);
						}
					}
				}
			}
			return allUsers;
		}

		/// <summary>
		/// Returns a single user by using a User Id to locate it
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public User GetUserById(int id)
		{
			//Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
			User newUser = null;
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectByUserIdQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("user_id", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Prepare();
					command.Parameters[0].Value = id;

					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						if (dr.Read())
							newUser = populateUserFromDB(dr);
						//IF there is more than one row coming back for an id, than we should toss an exception
						if (dr.Read())
							throw new InvalidOperationException("More than one user came back when Querying by UserId");
					}
				}
			}

			return newUser;
		}

		public User GetUserByUsername(string username)
		{
			//Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
			User newUser = null;
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(SelectByUsernameQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text));
					command.Prepare();
					command.Parameters[0].Value = username;

					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						while (dr.Read())
						{
							newUser = populateUserFromDB(dr);
						}
						
					   
					}
				}
			}

			return newUser;
		}

		public string GetCalendarColorForUser(int userid)
		{

			string color = "";
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetCalendarColorQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Prepare();
					command.Parameters[0].Value = userid;

					using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
					{
						while (dr.Read())
						{
							color = dr[0].ToString();
						}


					}
				}
			}
			return color;
		}

		/// <summary>
		/// Based on whether or not UserId field has value, will either insert or update a user row in db
		/// </summary>
		/// <param name="user"></param>
		public void SaveUser(User user)
		{
			string query;
			bool isUpdate = false;
			//Want to know right off the bat if we're doing a insert or update
			if (user.UserId > 0)
			{
				if (user.newPassword == null)
				{
					query = UpdateNonPasswordQuery;
				}
				else
				{
					query = UpdateQuery;
				}

				isUpdate = true;
			}
			else
			{
				query = InsertQuery;
			}

			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text));
					if (user.newPassword != null)
					{
						command.Parameters.Add(new Npgsql.NpgsqlParameter("password", NpgsqlTypes.NpgsqlDbType.Text));
					}
					command.Parameters.Add(new Npgsql.NpgsqlParameter("first_name", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("middle_name", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("last_name", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("address1", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("address2", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("assigned_role_id", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("office_number", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("sales_rep_number", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("calendar_color", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("phone1", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("phone2", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("faxnumber", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("wage", NpgsqlTypes.NpgsqlDbType.Real));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("email1", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("email2", NpgsqlTypes.NpgsqlDbType.Text));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("isactive", NpgsqlTypes.NpgsqlDbType.Boolean));
					command.Parameters.Add(new Npgsql.NpgsqlParameter("team", NpgsqlTypes.NpgsqlDbType.Integer));
					if (isUpdate)
						command.Parameters.Add(new Npgsql.NpgsqlParameter("user_id", NpgsqlTypes.NpgsqlDbType.Integer));

					command.Prepare();

					command.Parameters["username"].Value = user.UserName;
					if (user.newPassword != null)
					{
						if (isUpdate)
						{
							command.Parameters["password"].Value = user.newPassword;

						}
						else
						{
							command.Parameters["password"].Value = user.Password;
						}
					}
					command.Parameters["first_name"].Value = user.FirstName;
					command.Parameters["middle_name"].Value = user.MiddleName;
					command.Parameters["last_name"].Value = user.LastName;
					command.Parameters["address1"].Value = user.Address1;
					command.Parameters["address2"].Value = user.Address2;
					command.Parameters["city"].Value = user.City;
					command.Parameters["state"].Value = user.State;
					command.Parameters["zipcode"].Value = user.ZipCode;
					command.Parameters["assigned_role_id"].Value = user.AssignedRoleId;
					command.Parameters["office_number"].Value = user.OfficeNumber;
					command.Parameters["sales_rep_number"].Value = user.SalesRepNumber;
					command.Parameters["calendar_color"].Value = user.CalendarColor;
					command.Parameters["phone1"].Value = user.PhoneNumberOne;
					command.Parameters["phone2"].Value = user.PhoneNumberTwo;
					command.Parameters["faxnumber"].Value = user.FaxNumber;
					command.Parameters["wage"].Value = user.HourlyRate;
					command.Parameters["email1"].Value = user.EmailOne;
					command.Parameters["email2"].Value = user.EmailTwo;
					command.Parameters["isactive"].Value = user.IsActive;
					command.Parameters["team"].Value = user.TeamNumber;

					
					if (isUpdate)
					{
						command.Parameters["user_id"].Value = user.UserId;
					}

					int rowsAffected = command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteUser(int Userid)
		{
			using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
			{
				conn.Open();
				using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(DeleteQuery, conn))
				{
					command.Parameters.Add(new Npgsql.NpgsqlParameter("user_id", NpgsqlTypes.NpgsqlDbType.Integer));
					command.Prepare();
					command.Parameters[0].Value = Userid;
					int rowsAffected = command.ExecuteNonQuery();
				}
			}
		}

		#region Helper Methods
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
