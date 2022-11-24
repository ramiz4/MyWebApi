namespace MyWebApp.Services.Interfaces
{
    public interface IServiceManager
    {
        IPersonService PersonService { get; }

        IContactInfoService ContactInfoService { get; }
    }
}
