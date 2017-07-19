using System.Web.Security;
using Data.Domain;
using Data.Repositories.Abstract;
using System;
using System.Linq;
using Ninject;

namespace Web.Infrastructure.Authentication
{
    public class TrinityRoleProvider : RoleProvider
    {
        #region Not Implemented

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        [Inject]
        public IUserRepository userRepository
        {
            get;
            set;
        }

        [Inject]
        public IRoleRepository roleRepository
        {
            get;
            set;
        }

        public TrinityRoleProvider()
            : this(null)
        {

        }

        public TrinityRoleProvider(IUserRepository userRepository)
            : base()
        {
            //this.userRepository = userRepository;
        }


        public override bool IsUserInRole(string username, string roleName)
        {
            User user = null;
            try
            {
                user = userRepository.GetUserByUsername(username);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            if (user == null)
            {
                return false;
            }
            else
            {
                //Create Permission object from RoleName
                var perm = new Permission(roleName);

                //Get the Users Role.
                var usersRole = roleRepository.GetRoleByRoleId(user.AssignedRoleId);

                //See if Permission exist in user's role
                var exist = usersRole.Permissions.Contains(perm);
                return exist;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            User user = null;
            try
            {
                user = userRepository.GetUserByUsername(username);
            }
            catch (InvalidOperationException)
            {
                return new string[0];
            }

            if (user == null)
            {
                return new string[0];
            }
            else
            {
                //Technically, this should return user's roles. However, since we are weird and care about permissions instead
                //Return user's permissions instead
                var usersRole = roleRepository.GetRoleByRoleId(user.AssignedRoleId);

                string[] permissions = new string[usersRole.Permissions.Count];
                int idx = 0;
                foreach (var perm in usersRole.Permissions)
                    permissions[idx++] = perm.LongName;

                return permissions;
            }
        }
    }
}