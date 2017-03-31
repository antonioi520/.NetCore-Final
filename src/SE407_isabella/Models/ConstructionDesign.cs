using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class ConstructionDesign
    {
        [Required(ErrorMessage = "ConstructionDesign is required")]
        public Guid ConstructionDesignId { get; set; }

        [Required(ErrorMessage = "ConstructionDesignType is required")]
        [MinLength(5, ErrorMessage = "Minimum length of ConstructionDesignType is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of ConsturctionDesignType is 50 characters")]
        public string ConstructionDesignType { get; set; }

    }
}
