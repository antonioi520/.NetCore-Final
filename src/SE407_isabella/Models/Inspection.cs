using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class Inspection
    {
        [Required(ErrorMessage = "InspectionId is required")]
        public Guid InspectionId { get; set; }

        [Required(ErrorMessage = "BridgeId is required")]
        public Guid BridgeId { get; set; }

        [Required(ErrorMessage = "InspectorId is required")]
        public Guid InspectorId { get; set; }

        [Required(ErrorMessage = "InspectionDate is required")]
        public DateTime InspectionDate { get; set; }

        [Required(ErrorMessage = "DeckInspectionCodeId is required")]
        public Guid DeckInspectionCodeId { get; set; }

        [Required(ErrorMessage = "SuperstructureInspectionCodeId is required")]
        public Guid SuperstructureInspectionCodeId { get; set; }

        [Required(ErrorMessage = "SubstructureInspectionCodeId is required")]
        public Guid SubstructureInspectionCodeId { get; set; }

        [MinLength(5, ErrorMessage = "Minimum length of InspectionNotes is 5 characters")]
        [MaxLength(2000, ErrorMessage = "Max length of InspectionNotes is 2000 characters")]
        public string InspectionNotes { get; set; }

    }
}
