using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Core.DTOs
{
    public class PersonForCreationDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(60, ErrorMessage = "FirstName can't be longer than 60 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(60, ErrorMessage = "LastName can't be longer than 60 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(60, ErrorMessage = "Email can't be longer than 60 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [StringLength(100, ErrorMessage = "Mobile cannot be loner then 100 characters")]
        public string Mobile { get; set; }
    }
}
