using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class County
    {
        [Required(ErrorMessage = "County is required")]
        public Guid CountyId { get; set; }

        [Required(ErrorMessage = "CountyName is required")]
        [MinLength(5, ErrorMessage = "Minimum length of CountyName is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of CountyName is 50 characters")]
        public string CountyName { get; set; }

    }
}
