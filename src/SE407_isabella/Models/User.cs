using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE407_isabella
{
    public class User
    {
        [Required(ErrorMessage = "User is required")]
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of First Name is 5 characters")]
        [MaxLength(25, ErrorMessage = "Max length of First Name is 25 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Last Name is 5 characters")]
        [MaxLength(25, ErrorMessage = "Max length of Last Name is 25 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Email is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Email is 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Password is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of Password is 255 characters")]
        public string Password { get; set; }

    }
}
