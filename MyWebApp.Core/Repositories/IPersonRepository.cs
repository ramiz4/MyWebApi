using MyWebApp.Core.Entities;

namespace MyWebApp.Core.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetAll();

        IQueryable<Person> GetById(Guid personId);

        void Insert(Person person);

        void Update(Person person);

        void Remove(Person person);
    }
}
