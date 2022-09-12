namespace MyWebApi.Models;

#pragma warning disable CS1591
public enum ContactType
{
    Private,
    Business
}
#pragma warning restore CS1591

/// <summary>
/// ContactInfo class
/// </summary>
public class ContactInfo : BaseEntity
{
    /// <summary>
    /// ContactInfo constructor
    /// </summary>
    public ContactInfo(ContactType type, string street, string streetNumber, int postalCode, string city, string country)
    {
        Type = type;
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
    }

    /// <summary>
    /// ContactType
    /// </summary>
    public ContactType Type { get; set; }

    /// <summary>
    /// Street
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// StreetNumber
    /// </summary>
    public string StreetNumber { get; set; }

    /// <summary>
    /// PostalCode
    /// </summary>
    public int PostalCode { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public string Country { get; set; }
}