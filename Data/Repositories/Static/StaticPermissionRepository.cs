using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Static
{
    public class StaticPermissionRepository : IPermissionRepository
    {
        private static IList<Permission> fakePermission = new List<Permission>();
        private static int permissionCount = 1;
        
        public IQueryable<Domain.Permission> Permissions
        {
            get { return fakePermission.AsQueryable(); }
        }

        public void SavePermission(Permission permission)
        {
           //if new Permission, add to the list
            if (permission.PermissionId == 0)
            {
                permission.PermissionId = permissionCount;
                permissionCount++;
                fakePermission.Add(permission);
            }
            else if (fakePermission.Count(row => row.PermissionId == permission.PermissionId) == 1)
            {
                //updating deleting old and adding new
                DeletePermission(permission);
                fakePermission.Add(permission);
            }
        }

        public void DeletePermission(Permission permission)
        {
            var temp = fakePermission.ToList();
            temp.RemoveAll(row => row.PermissionId == permission.PermissionId);
            fakePermission = temp;
        }

        public void ClearRepo()
        {
            fakePermission.Clear();
        }
    }
}
