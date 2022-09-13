using Microsoft.EntityFrameworkCore;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Repositories;

namespace MyWebApp.Persistence.Repositories
{
    internal sealed class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _dbContext;

        public PersonRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public IQueryable<Person> GetAll()
        {
            return _dbContext.Persons;
        } 

        public IQueryable<Person> GetById(Guid personId)
        {
            return _dbContext.Persons.Where(p => p.Id == personId);
        }

        public void Insert(Person person)
        {
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();
        }

        public void Remove(Person person) 
        {
            _dbContext.Persons.Remove(person);
            _dbContext.SaveChanges();
        }

        public void Update(Person person)
        {
            _dbContext.Entry(person).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
