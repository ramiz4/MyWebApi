using System.ComponentModel.DataAnnotations;
using MyWebApp.Core.Entities;

namespace MyWebApp.Core.DTOs
{
    public class ContactInfoForUpdateDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public ContactType Type { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(120, ErrorMessage = "Street can't be longer than 120 characters")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "StreetNumber is required")]
        [StringLength(10, ErrorMessage = "StreetNumber can't be longer than 10 characters")]
        public string? StreetNumber { get; set; }

        [Required(ErrorMessage = "PostalCode is required")]
        [StringLength(8, ErrorMessage = "FirstName can't be longer than 8 characters")]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(30, ErrorMessage = "City can't be longer than 30 characters")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(30, ErrorMessage = "Country can't be longer than 30 characters")]
        public string? Country { get; set; }
    }
}