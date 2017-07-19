using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGRoleRepository : IRoleRepository
    {
        #region Query Strings
        private string GetRoleByIdQuery = @"SELECT r.name AS role_name, p.name AS perm_name, p.action, p.permission_id
                                            FROM roles AS r
                                            INNER JOIN roles_permissions AS rp ON r.role_id = rp.role_id
                                            INNER JOIN permissions AS p ON p.permission_id = rp.permission_id
                                            WHERE r.role_id = :role_id";
        private string GetAllRolesQuery = @"SELECT r.name, r.role_id FROM roles AS r";
        #endregion

        /// <summary>
        /// Returns the role with it's permissions list populated
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Domain.Role GetRoleByRoleId(int roleId)
        {
            Role role = new Role() { RoleId = roleId };
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection())
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetRoleByIdQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("role_id", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = roleId;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (role.Name.Equals(""))
                                role.Name = dr[0].ToString();
                            role.Permissions.Add(new Permission() { Name = dr[1].ToString(), Action = dr[2].ToString(), PermissionId = Helper.ConvertFromDBVal<int>(dr[3]) });
                        }
                    }
                }
            }
            return role;
        }

        /// <summary>
        /// Returns a list of all roles in db. Permissions lists will NOT be populated. 
        /// Can be updated to do so if necessary
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.Role> GetAllRoles()
        {
            IList<Domain.Role> roles = new List<Domain.Role>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection())
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(GetAllRolesQuery, conn))
                {
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var temp = new Role() { RoleId = Helper.ConvertFromDBVal<int>(dr[1]), Name = dr[0].ToString() };
                            roles.Add(temp);
                        }
                    }
                }
            }

            return roles;
        }
    }
}
