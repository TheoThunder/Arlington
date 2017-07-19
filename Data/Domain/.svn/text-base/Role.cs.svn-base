using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class Role
    {
        public Role()
        { 
           Permissions = new List<Permission>();
        
        }
        [Integer]
       //[Min(1, ErrorMessage = "Need only digits.")]
        public int RoleId { get; set; } //Database Id
        public string Name { get; set; }
        public IList<Permission> Permissions { get; set; } //Will not be chaging this fellow to use Id's 
                                                           //because Permissions and Roles have no CRUD

        public override string ToString()
        {
            return "Role Name: " + Name + ", RoleId: " + RoleId;
        }
    }
}
