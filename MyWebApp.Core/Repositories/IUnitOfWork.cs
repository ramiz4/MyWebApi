namespace MyWebApp.Core.Repositories
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
