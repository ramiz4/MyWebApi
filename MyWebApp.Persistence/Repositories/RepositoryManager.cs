using MyWebApp.Core.Repositories;

namespace MyWebApp.Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IPersonRepository> _lazyPersonRepository;
        private readonly Lazy<IContactInfoRepository> _lazyContactInfoRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(AppDbContext dbContext)
        {
            _lazyPersonRepository = new Lazy<IPersonRepository>(() => new PersonRepository(dbContext));
            _lazyContactInfoRepository = new Lazy<IContactInfoRepository>(() => new ContactInfoRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IPersonRepository PersonRepository => _lazyPersonRepository.Value;

        public IContactInfoRepository ContactInfoRepository => _lazyContactInfoRepository.Value;

        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
