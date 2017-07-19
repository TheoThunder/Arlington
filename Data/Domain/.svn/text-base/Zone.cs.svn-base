using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
using System.Web.Mvc;
namespace Data.Domain
{
    public class Zone
    {
        public Zone()
        { 
           UserIds = new List<int>();
           ZipCodesCovered = new List<string>();
        }

       
        public int ZoneId { get; set; } //Database Id
        [Integer(ErrorMessage = "Need only digits.")]
        public int ZoneNumber { get; set; }
        public IList<string> ZipCodesCovered { get; set; }
        public IList<int> UserIds { get; set; }
    }
}
