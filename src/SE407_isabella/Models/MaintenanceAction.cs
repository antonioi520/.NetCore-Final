using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class MaintenanceAction
    {
        [Required(ErrorMessage = "MaintenanceActionId is required")]
        public Guid MaintenanceActionId { get; set; }

        [Required(ErrorMessage = "MaintenanceActionName is required")]
        [MinLength(5, ErrorMessage = "Minimum length of MaintenanceActionName is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of MaintenanceActionName is 50 characters")]
        public string MaintenanceActionName { get; set; }

    }
}
