using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class CalendarSettings
    {
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int RedLimit { get; set; }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int YellowLimit { get; set; }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int GreenLimit { get; set; }
    }
}
