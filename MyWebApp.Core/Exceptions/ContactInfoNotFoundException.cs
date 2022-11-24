namespace MyWebApp.Core.Exceptions
{
    public sealed class ContactInfoNotFoundException : NotFoundException
    {
        public ContactInfoNotFoundException(Guid contactInfoId) 
            : base($"The contact info with the identifier {contactInfoId} was not found.")
        {
        }
    }
}
