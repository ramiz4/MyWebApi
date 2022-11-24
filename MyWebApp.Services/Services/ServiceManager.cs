using MyWebApp.Core.Repositories;
using MyWebApp.Services.Interfaces;

namespace MyWebApp.Services.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPersonService> _lazyPersonService;
        private readonly Lazy<IContactInfoService> _lazyContactInfoService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyPersonService = new Lazy<IPersonService>(() => new PersonService(repositoryManager));
            _lazyContactInfoService = new Lazy<IContactInfoService>(() => new ContactInfoService(repositoryManager));
        }

        public IPersonService PersonService => _lazyPersonService.Value;

        public IContactInfoService ContactInfoService => _lazyContactInfoService.Value;
    }
}
