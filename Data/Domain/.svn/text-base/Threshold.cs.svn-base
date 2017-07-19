using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class Threshold
    {
        public Threshold()
        {
        }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int ThresholdId { get; set; } //Database Id

        public int Upper_Calendar { get; set; }
        public int Lower_Calendar { get; set; }
        
        public int WE_GA_Upper_Dashboard { get; set; }
        public int WE_GA_Lower_Dashboard { get; set; }

        public int WE_SA_Upper_Dashboard { get; set; }
        public int WE_SA_Lower_Dashboard { get; set; }

        public int NC_Upper_Dashboard { get; set; }
        public int NC_Lower_Dashboard { get; set; }

    }
}
