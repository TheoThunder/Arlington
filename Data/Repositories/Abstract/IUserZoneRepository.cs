using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IUserZoneRepository
    {
        IEnumerable<UserZone> GetAllUsersByZone(int zoneid);
        IEnumerable<UserZone> GetAllZonesByUser(int userid);

        void SaveUserZone(UserZone userzone);
        void DeleteUserZone(UserZone username);

        void DeleteUserZoneByUserID(int userzoneid);
    }
}
