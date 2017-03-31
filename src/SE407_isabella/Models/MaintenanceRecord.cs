using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class MaintenanceRecord
    {
        [Key]
        [Required(ErrorMessage = "MaintenanceRecordId is required")]
        public Guid MaintenanceRecordId { get; set; }


        [Required(ErrorMessage = "MaintenanceActionId is required")]
        public Guid MaintenanceActionId { get; set; }


        [Required(ErrorMessage = "InspecotrId is required")]
        public Guid InspectorId { get; set; }


        [Required(ErrorMessage = "MaintenanceProjectedStart is required")]
        public DateTime MaintenanceProjectedStart { get; set; }


        [Required(ErrorMessage = "MaintenanceProjectedEnd is required")]
        public DateTime MaintenanceProjectedEnd { get; set; }


        public DateTime MaintenanceActualStart { get; set; }


        public DateTime MaintenanceActualEnd { get; set; }


        [Required(ErrorMessage = "MaintenanceProjectedCost is required")]
        public decimal MaintenanceProjectedCost { get; set; }


        public decimal MaintenanceActualCost { get; set; }


        [MinLength(5, ErrorMessage = "Minimum length of MaintenanceNotes is 5 characters")]
        [MaxLength(1000, ErrorMessage = "Max length of MaintenanceNotes is 1000 characters")]
        public string MaintenanceNotes { get; set; }


        [MinLength(5, ErrorMessage = "Minimum length of InspectorNotes is 5 characters")]
        [MaxLength(1000, ErrorMessage = "Max length of InspectorNotes is 1000 characters")]
        public string InspectorNotes { get; set; }

    }
}
