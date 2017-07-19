using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {

        }
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int id { get; set; } //Database Id

        public string title { get; set; }
        //[DataType(DataType.DateTime)]
        //public DateTime StartTime { get; set; }
        //[DataType(DataType.DateTime)]
        //public DateTime EndTime { get; set; }

        public string type { get; set; }

        public string description { get; set; }
        public Boolean appointment { get; set; }
        public Boolean personal { get; set; }

        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public int zone { get; set; }
        public int zipcode { get; set; }

        public string map { get; set; }
        
        public int creator { get; set; } //Who created the Event? 
        public int assigned { get; set; } //Who is the Event Assigned to?
        //NOTE: An event creator is only one able to modify it. Assigned can view event data, but cannot change. This allows for
        // Manager to create events for SA to attend AKA Appointment Assignment. In those cases where SA creates event for self,
        // They will be both Creator and Assigned


        public string start { get; set; }
        public string end { get; set; }

        public int AvailableSA {get; set;}

        public int Parent_User_Id { get; set; }
        public int Parent_Appointment_Id { get; set; }

        public string Appointment_Reference { get; set; }

        //These below are to be deleted
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // For displaying color for user
        public string CalendarColor { get; set; }
        //For displaying the name of the AssignedSA
        public string salesAgent { get; set; }
    }
}
