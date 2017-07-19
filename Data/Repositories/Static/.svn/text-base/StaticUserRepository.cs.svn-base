using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;

namespace Data.Repositories.Static
{
    public class StaticUserRepository:IUserRepository
    {
        private static IList<User> fakeUser= new List<User>();
        private static int counter = 1;

        public IEnumerable<User> GetAllUsers()
        {
            return fakeUser;
        }

        public User GetUserById(int id)
        {
            return fakeUser.SingleOrDefault(row => row.UserId == id);
        }

        public User GetUserByUsername(String username)
        {
            return fakeUser.SingleOrDefault(row => row.UserName == username);
        }
        public IEnumerable<User> GetAllUsersByTeam(int TeamNumber)
        {
            //This is a fairly generic db query with no parameters and pulling all values from reader and tossing data into User

            return fakeUser;
        }
        public void SaveUser(User user)
        {
            // If it's a new user, just add it to the list
            if (user.UserId == 0)
            {
                user.UserId = counter;
                counter += 1;
                fakeUser.Add(user);
            }
            else if (fakeUser.Count(row => row.UserId == user.UserId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteUser(user.UserId);
                fakeUser.Add(user);
            }
        }

        public void DeleteUser(int UserId)
        {
            var temp = fakeUser.ToList();
            temp.RemoveAll(row => row.UserId == UserId);
            fakeUser = temp;
        }

        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeUser.Clear();
        }

        public string GetCalendarColorForUser(int userid)
        {

            string color = "";
            return color;
        }
        public IEnumerable<User> GetAllUsersByPermission(string PermissionName)
        {
            var perm = new Permission(PermissionName);
            var rolerepo = new StaticRoleRepository();
            var roles = rolerepo.GetAllRoles();
            var roleIdsWithPermission = roles.Where(row => row.Permissions.Contains(perm)).Select(row => row.RoleId);

            return fakeUser.Join(roleIdsWithPermission, o => o.AssignedRoleId, id => id, (o, id) => o);
        }
        public IEnumerable<User> GetAllUsersByRole(int role)
        {
            throw new NotImplementedException();
        }
    }
}
