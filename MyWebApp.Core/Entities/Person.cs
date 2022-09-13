namespace MyWebApp.Core.Entities;

/// <summary>
/// Person class
/// </summary>
public class Person : BaseEntity
{
    public Person() { }

    public Person(string firstName, string lastName, string email, string mobile)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Mobile = mobile;
    }

    /// <summary>
    /// FirstName
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// LastName
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Mobile
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// ContactInfos
    /// </summary>
    public List<ContactInfo>? ContactInfos { get; set; }
}
