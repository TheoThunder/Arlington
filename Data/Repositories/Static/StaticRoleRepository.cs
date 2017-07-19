using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
namespace Data.Repositories.Static
{
    public class StaticRoleRepository : IRoleRepository
    {
        private static IList<Role> fakeRole = new List<Role>();
        private static int roleCount = 1;

        public Role GetRoleByRoleId(int roleId)
        {
            return fakeRole.SingleOrDefault(row => row.RoleId == roleId);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return fakeRole;
        }

        public Role GetRoleByRoleName(string roleName)
        {
            return fakeRole.SingleOrDefault(row => row.Name == roleName);
        }

        public void SaveRole(Role role)
        {
            //If new role, add it to the list
            if (role.RoleId == 0)
            {
                role.RoleId = roleCount;
                roleCount++;
                fakeRole.Add(role);
            }
            else if (fakeRole.Count(row => row.RoleId == role.RoleId) == 1)
            {
                //updating by deleting old and replace with new
                DeleteRole(role);
                fakeRole.Add(role);
            }
        }

        public void DeleteRole(Role role)
        {
            var temp = fakeRole.ToList();
            temp.RemoveAll(row => row.RoleId == role.RoleId);
            fakeRole = temp;
        }

        public void ClearRepo()
        {
            fakeRole.Clear();
        }
    }
}
