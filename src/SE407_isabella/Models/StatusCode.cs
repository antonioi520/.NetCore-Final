using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class StatusCode
    {
        [Required(ErrorMessage = "StatusCodeId is required")]
        public Guid StatusCodeId { get; set; }

        [Required(ErrorMessage = "StatusName is required")]
        [MinLength(5, ErrorMessage = "Minimum length of StatusName is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of StatusName is 50 characters")]
        public string StatusName { get; set; }
    }
}
