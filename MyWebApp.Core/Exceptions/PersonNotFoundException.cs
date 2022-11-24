namespace MyWebApp.Core.Exceptions
{
    public sealed class PersonNotFoundException : NotFoundException
    {
        public PersonNotFoundException(Guid personId) 
            : base($"The person with the identifier {personId} was not found.")
        {
        }
    }
}
