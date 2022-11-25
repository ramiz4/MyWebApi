using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApp.Core.Entities;

public class ContactInfo : BaseEntity
{
    public ContactType? Type { get; set; }

    public string? Street { get; set; }

    public string? StreetNumber { get; set; }

    public int? PostalCode { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    [ForeignKey("Person")]
    public Guid PersonId { get; set; }

    public Person? Person { get; set; }
}