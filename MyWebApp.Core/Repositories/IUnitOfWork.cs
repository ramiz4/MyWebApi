using MyWebApp.Core.Entities;

namespace MyWebApp.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
