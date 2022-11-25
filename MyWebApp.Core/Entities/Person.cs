namespace MyWebApp.Core.Entities;

public sealed class Person : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public ICollection<ContactInfo>? ContactInfos { get; set; }
}
