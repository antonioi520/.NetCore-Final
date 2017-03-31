using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class Inspector
    {
        [Required(ErrorMessage = "InspectorId is required")]
        public Guid InspectorId { get; set; }

        [Required(ErrorMessage = "InspectorFirst is required")]
        [MinLength(5, ErrorMessage = "Minimum length of InspectorFirst is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of InspectorFirst is 50 characters")]
        public string InspectorFirst { get; set; }

        [Required(ErrorMessage = "InspectorLast is required")]
        [MinLength(5, ErrorMessage = "Minimum length of InspectorLast is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of InspectorLast is 50 characters")]
        public string InspectorLast { get; set; }

        [Required(ErrorMessage = "InspectorOrg is required")]
        [MinLength(5, ErrorMessage = "Minimum length of InspectorOrg is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of InspectorOrg is 50 characters")]
        public string InspectorOrg { get; set; }

        [Required(ErrorMessage = "InspectorCertEffective is required")]
        public DateTime InspectorCertEffective { get; set; }

        [Required(ErrorMessage = "InspectorCertExpires is required")]
        public DateTime InspectorCertExpires { get; set; }

    }
}
