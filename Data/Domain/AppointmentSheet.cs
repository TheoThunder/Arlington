using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
using System.ComponentModel;
namespace Data.Domain
{
    public class AppointmentSheet : IValidatableObject
    {
        public AppointmentSheet()
        {
            AssignedUser = new User();
            Leadlist = new Lead();
        }
        
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int AppointmentSheetId { get; set; } //Database Id

        public int CreatorId { get; set; }
        public int ParentLeadId { get; set; }

        [DataType(DataType.Date)] 
        public DateTime CreatedAt { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DayOfAppointment { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Compare(DayOfAppointment, DateTime.Now).ToString().Equals("-1"))
            {
                yield return new ValidationResult("Must be a future appointment");
            }
        }

        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "hh:mm", ApplyFormatInEditMode = true )]
        public DateTime AppointmentDateFrom { get; set; }
        

        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDateTo { get; set; }
        
        

        public string AppointmentLocation { get; set; }

        [DataType(DataType.Text)]
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Location { get; set; }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int ZipCode { get; set; }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        //THIS ZONE CODE NEEDS NEEDS TO BE FIXED BY WHOEVER IS DOING THE ZONE
        //public int Zone {
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //} //Should not be set by user, is based on Zipcode to Zone mappings
        public Boolean CurrentlyAcceptingCards { get; set; }
        public Boolean NewSetUp { get; set; }
        public Boolean Price { get; set; }
        public Boolean NewEquipment { get; set; }
        public Boolean AddingServices { get; set; }
        public Boolean Unhappy { get; set; }
        public Boolean SingleLocation { get; set; }
        public Boolean MultiLocation { get; set; }
        public string CurrentProcessor { get; set; }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int HowManyLocations { get; set; }

        public string Volume { get; set; }
        public Boolean Swipe { get; set; }
        public Boolean Moto { get; set; }
        public Boolean Internet { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public int AssignedSalesAgent { get; set; }
        public string Score { get; set; }
        public Boolean SingleLocCheck { get; set; }
        public string Event_Reference { get; set; }
        //public int AppointmentZone { get; set; }
        // This user is to populate username etc for a user. AssignedUser.Userid id referenced using AssignedSalesAgent
        public User AssignedUser { get; set; }
        public Lead Leadlist { get; set; }

        // To get the companyname in appointment queue
        public string companyname { get; set; }

        //string (yes/no) value for AA Performance Report
        public string Closed { get; set; }

        //Number of accounts
        public int Accounts { get; set; }

        //Boolean Bit to check if the appointment was rescheduled
        public Boolean Reschedule { get; set; }
        
        // For diplaying the creator name in Appointment Sheets
        public string CreatorName { get; set; }
    }
}
