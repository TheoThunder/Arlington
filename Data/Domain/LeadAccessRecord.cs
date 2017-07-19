using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class LeadAccessRecord
    {
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int LeadAccessId { get; set; } //Database Id

        public Lead LeadAccessed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeAccessed { get; set; }
        public User WhoAccessed { get; set; }
    }
}
