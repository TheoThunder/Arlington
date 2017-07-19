using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;

namespace Data.Domain
{
    public class User
    {
        public User()
        {
            //AssignedLeads = new List<Lead>();
            //CreatedTickets = new List<Ticket>();
            AssignedZoneIds = new List<int>();
        }

        public bool firstTime = true;
        public int UserId { get; set; } //Database Id

        [Required(ErrorMessage = "Please enter a Username")]
        public string UserName { get; set; }

        /// <summary>
        /// Can only be read never set.
        /// </summary>
        //[Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        private string _newPassword;
        /// <summary>
        /// When you set newPassword with a Plain-Text value, it sets Password with it's encrypted value
        /// </summary>
        //[Required]
        [DataType(DataType.Password)]
        public string newPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                Password = EncryptPassword(value);
            }
        }
        
        public string changePassword1 { get; set; }
        public string changePassword2 { get; set; }
        public string oldPassword { get; set; }

        //[Required(ErrorMessage = "Please enter a First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
       // [Required(ErrorMessage = "Please enter a Last Name")]
        public string LastName { get; set; }

        public string PhoneNumberOne { get; set; }
        public string PhoneNumberTwo { get; set; }
        public string FaxNumber { get; set; }  

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public string EmailOne { get; set; }
        public string EmailTwo { get; set; }

        public int AssignedRoleId { get; set; }
        public string selectedRole { get; set; }
        //public IList<Lead> AssignedLeads { get; set; } Ask Leads repo for LeadsForAssignedUser
         
        [Integer(ErrorMessage = "Need only digits.")]
        public int OfficeNumber { get; set; }

        //public IList<Ticket> CreatedTickets { get; set; } Ask Tickets Repo for TicketsCreatedByUser
         [Integer(ErrorMessage = "Need only digits.")]
        public Single HourlyRate { get; set; }
        [Integer(ErrorMessage = "Need only digits.")]
        public int SalesRepNumber { get; set; }

        public string CalendarColor { get; set; }
        public IList<int> AssignedZoneIds { get; set; } //Only matters for SA, AA. Many to Many.
        public IList<int> ZoneIds { get; set; }

        public Boolean IsActive { get; set; }
        public static string EncryptPassword(string password)
        {
            if (!String.IsNullOrEmpty(password))
            {
                byte[] pwdBytes = Encoding.GetEncoding(1252).GetBytes(password);
                byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(pwdBytes);
                return Encoding.GetEncoding(1252).GetString(hashBytes);
            }
            else
            {
                return "";
            }
        }

        public string ErrorMessage { get; set; }

        public int TeamNumber { get; set; }

        // For dashboard 
        public int totalAppointments { get; set; }
        public int totalCalls { get; set; }
        public int totalCloses { get; set; }
        public int totalGoodAppointments { get; set; }
    }

}
