using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class Card
    {
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int CardId { get; set; } //Database Id

        public int CreatorId { get; set; }

        public int ParentLeadId { get; set; }
        public int AssignedAAId { get; set; }

        [DataType(DataType.Date)] 
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CallBackDate { get; set; }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int NumberCalled { get; set; }

        public string CardType { get; set; } //This should likely be converted into an Enum or something similar
        
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public string NoInterestRea { get; set; }
        public Boolean NoInterestChk { get; set; }
        public Boolean AcquiredDMName { get; set; }
        public Boolean TalkedToPerson { get; set; }
        public Boolean TalkedToDM { get; set; }
        public Boolean TalkedToOfficeManager { get; set; }
        public Boolean TalkedToOther { get; set; }
        public Boolean LeftVM { get; set; }
        //public AppointmentSheet AppointmentSet { get; set; }
        public int AppointmentSheetId { get; set; }

        //Boolean value when Manager/Admin reassigned lead to someone else.
        public Boolean Reassigned { get; set; }

        // For diplaying the creator name in Cards
        public string CreatorName { get; set; }
    }
}
