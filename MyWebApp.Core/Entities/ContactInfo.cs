using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApp.Core.Entities;

public class ContactInfo : BaseEntity
{
    public ContactInfo(ContactType type, string street, string streetNumber, int postalCode, string city, string country)
    {
        Type = type;
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
    }

    public ContactInfo(ContactType type, string street, string streetNumber, int postalCode, string city, string country, Guid personId, Person person)
    {
        Type = type;
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
        PersonId = personId;
        Person = person;
    }

    public ContactType Type { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    [ForeignKey("Person")]
    public Guid PersonId { get; set; }
    public Person Person { get; set; }
}