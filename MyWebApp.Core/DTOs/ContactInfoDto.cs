using MyWebApp.Core.Entities;

namespace MyWebApp.Core.DTOs
{
    public class ContactInfoDto
    {
        public ContactInfoDto(Guid id, ContactType type, string street, string streetNumber, int postalCode, string city, string country, Guid personId, PersonDto person)
        {
            Id = id;
            Type = type;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
            PersonId = personId;
            Person = person;
        }

        public Guid Id { get; set; }
        
        public ContactType Type { get; set; }
        
        public string Street { get; set; }
        
        public string StreetNumber { get; set; }
        
        public int PostalCode { get; set; }
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public Guid PersonId { get; set; }

        public PersonDto Person { get; set; }
    }
}
