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
    public class Equipment
    {
        public Equipment()
        {
        }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int EquipmentId { get; set; } //Database Id

        public string Name { get; set; }
        public string Type { get; set; } //Really, Really Pondering an Enum for this. For now, will use String Constants
        public Boolean Active { get; set; }
    }
}
