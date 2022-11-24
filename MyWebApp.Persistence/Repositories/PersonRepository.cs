using Microsoft.EntityFrameworkCore;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Repositories;

namespace MyWebApp.Persistence.Repositories
{
    internal sealed class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _dbContext;

        public PersonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Person> GetAll()
        {
            return _dbContext.Persons.AsQueryable();
        }

        public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Persons.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Person?> GetByIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == personId, cancellationToken);
        }

        public void Insert(Person person)
        {
            _dbContext.Persons.Add(person);
        }

        public void Remove(Person person) 
        {
            _dbContext.Persons.Remove(person);
        }

        public void Update(Person person)
        {
            _dbContext.Update(person);
        }
    }
}
