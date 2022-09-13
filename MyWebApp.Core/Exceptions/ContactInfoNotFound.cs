namespace MyWebApp.Core.Exceptions
{
    public sealed class ContactInfoNotFound : NotFoundException
    {
        public ContactInfoNotFound(Guid contactInfoId) 
            : base($"The contact info with the identifier {contactInfoId} was not found.")
        {
        }
    }
}
