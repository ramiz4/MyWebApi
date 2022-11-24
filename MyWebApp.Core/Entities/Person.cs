namespace MyWebApp.Core.Entities;

public sealed class Person : BaseEntity
{
    public Person() { }

    public Person(string firstName, string lastName, string email, string mobile, ICollection<ContactInfo> contactInfos)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Mobile = mobile;
        ContactInfos = contactInfos;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Mobile { get; set; }

    public ICollection<ContactInfo> ContactInfos { get; set; }
}
