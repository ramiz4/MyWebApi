namespace MyWebApp.Core.Repositories
{
    public interface IRepositoryManager
    {
        IPersonRepository PersonRepository { get; }

        IContactInfoRepository ContactInfoRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
