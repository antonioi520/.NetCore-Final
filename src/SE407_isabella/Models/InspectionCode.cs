using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class InspectionCode
    {
        [Required(ErrorMessage = "InspectionCodeID is required")]
        public Guid InspectionCodeId { get; set; }

        [Required(ErrorMessage = "InspectionCodeName is required")]
        [MinLength(5, ErrorMessage = "Minimum length of InspectionCodeName is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of InspectionCodeName is 50 characters")]
        public string InspectionCodeName { get; set; }

        [Required(ErrorMessage = "Bridge is required")]
        [MinLength(5, ErrorMessage = "Minimum length of InspectionCodeDesc is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of InspectionCodeDesc is 50 characters")]
        public string InspectionCodeDesc { get; set; }

    }
}
