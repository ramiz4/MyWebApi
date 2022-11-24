using MyWebApp.Core.Entities;

namespace MyWebApp.Core.DTOs
{
    public sealed class PersonDto
    {
        public PersonDto(Guid id, string firstName, string lastName, string email, string mobile, ICollection<ContactInfo> contactInfos)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Mobile = mobile;
            ContactInfos = contactInfos;
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
