using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class MaterialDesign
    {
        [Required(ErrorMessage = "MaterialDeisgnId is required")]
        public Guid MaterialDesignId { get; set; }

        [Required(ErrorMessage = "MaterialDesignType is required")]
        [MinLength(5, ErrorMessage = "Minimum length of MaterialDesignType is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of MaterialDesignType is 50 characters")]
        public string MaterialDesignType { get; set; }

    }
}
