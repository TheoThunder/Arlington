using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;

namespace Data.Domain
{
    public class PhoneUser
    {
        public PhoneUser()
        {
            
        }

        
        public int PhoneUserId { get; set; } //Database Id
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
       
        public string LastName { get; set; }

        public int Extension { get; set; }
        public int CRMUserId { get; set; }
        public int AccountId { get; set; }
        public int VoiceMailId { get; set; }

        public string Email { get; set; }

        public DateTime Date_Created { get; set; }
        public int Extension_Server_UUID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        
    }

}
