using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
using System.Web.Mvc;
namespace Data.Domain
{
    /// <summary>
    /// A Lead
    /// </summary>
    public class Lead
    {
        public Lead()
        {
            StatementFiles = new List<UploadedFile>();
            AssignedUser = new User();
          
        }

        [Integer]
        public int LeadId { get; set; } //Database Id

        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Contact1Title { get; set; }
        [Required]
        public string Contact1FirstName { get; set; }
        [Required]
        public string Contact1LastName { get; set; }
        [Required]
        public string Contact2Title { get; set; }
        [Required]
        public string Contact2FirstName { get; set; }
        [Required]
        public string Contact2LastName { get; set; }
        public int AssignedAAUserId { get; set; }
        public User AssignedAA { get; set; }
        [Required(ErrorMessage = "Primary Phone Number required")]
        public string PrimaryPhoneNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string AddtionalPhoneNumber { get; set; }

        /// <summary>
        /// Will decide which number to call, whether the Prmary Phone or the Addtitonal phone number 
        /// 1 - Call Primary Number
        /// 2 - Call Secondary Number
        /// </summary>
        public int NumberToCall { get; set; }

        //added cardType i.e  DNC, callBack
        public string CardType { get; set; }

        [Required(ErrorMessage = "Fax Number required")]
        public string FaxNumber { get; set; }

        [Required(ErrorMessage = "Primary Email required")]
        [Email]
        public string PrimaryEmailAddress { get; set; }

        [DataType(DataType.EmailAddress)]
        public string AdditonalEmailAddress { get; set; }

        [DataType(DataType.Url)]
        public string WebsiteLink { get; set; }

        [DataType(DataType.Text)]
        public string StreetAddress1 { get; set; }

        [DataType(DataType.MultilineText)]
        public string StreetAddress2 { get; set; }

        [Required(ErrorMessage = "City required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State required")]
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int ZoneNumber { get; set; }
        public List<UploadedFile> StatementFiles { get; set; }
        public string NewFilePath { get; set; }
        public string Status { get; set; }
        public Boolean Suppressed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CallbackDate { get; set; }

        public int AssignedSAUserId { get; set; }
        public bool Ignored { get; set; }

        //This is the role of the user that has been assigned the lead
        public int AssignedUserRoleId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime IgnoredDate { get; set; }
        public Boolean Active { get; set; }

        public Boolean PrimaryPhoneChecked { get; set; }

        // This user is to populate username etc for a user. AssignedUser.Userid id referenced using AssignedSAUserID
        public User AssignedUser { get; set; }


        //To get number of accounts
        public int accounts { get; set; }
        
        //to get number of tickets
        public int tickets { get; set; }

        //To get the dropdown for zone in /lead/index
        public string ZoneDropDown { get; set; }

        //For the followup list appointment description
        public string appointmentdesc { get; set; }

        // For displaying appointment schedule  in scheduled appointments page
        public DateTime appointmentdate { get; set; }

        public int numTickets { get; set; }
        //[DisplayFormat(DataFormatString = "hh:mm", ApplyFormatInEditMode = true )]
        public DateTime AptDateFrom { get; set; }
        //[DisplayFormat(DataFormatString = "{hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime AptDateTo { get; set; }

        //for displaying volume in Scheduled appointments page
        public string volume { get; set; }

        public DateTime DateTimeImported { get; set; }

        // Boolean value to find out if the ignored lead was reassigned to aa
        public Boolean Reassigned { get; set; }

        //To display the call date in Callback list.
        public DateTime CallDate { get; set; }

       
    }

}
