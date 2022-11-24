using MyWebApp.Core.Entities;

namespace MyWebApp.Core.Repositories
{
    public interface IPersonRepository
    {
        public IQueryable<Person> GetAll();

        Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Person?> GetByIdAsync(Guid personId, CancellationToken cancellationToken = default);

        void Insert(Person person);

        void Update(Person person);

        void Remove(Person person);
    }
}
