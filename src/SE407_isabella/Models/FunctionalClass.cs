using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class FunctionalClass
    {
        [Required(ErrorMessage = "FunctionalClass is required")]
        public Guid FunctionalClassId { get; set; }

        [Required(ErrorMessage = "CountyName is required")]
        [MinLength(5, ErrorMessage = "Minimum length of FunctionalClassType is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of FunctionalClassType is 50 characters")]
        public string FunctionalClassType { get; set; }

    }
}
