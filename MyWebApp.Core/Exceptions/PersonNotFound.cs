namespace MyWebApp.Core.Exceptions
{
    public sealed class PersonNotFound : NotFoundException
    {
        public PersonNotFound(Guid personId) 
            : base($"The person with the identifier {personId} was not found.")
        {
        }
    }
}
