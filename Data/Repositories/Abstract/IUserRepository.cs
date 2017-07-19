using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersByPermission(string PermissionName);
        IEnumerable<User> GetAllUsersByTeam(int TeamNumber);
        IEnumerable<User> GetAllUsersByRole(int roleid);
        User GetUserById(int id);
        string GetCalendarColorForUser(int userid);
        /// <summary>
        /// Retrieves a user record by username.
        /// NOTE: Only use when id is difficult to get hands on. This will always be slower than by id
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUserByUsername(string username);
        void SaveUser(User user);
        void DeleteUser(int UserId);
    }
}
