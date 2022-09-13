using MyWebApp.Core.Entities;

namespace MyWebApp.Core.Repositories
{
    public interface IContactInfoRepository
    {
        IQueryable<ContactInfo> GetAllByPersonId(Guid personId);

        IQueryable<ContactInfo> GetById(Guid contactInfoId);
        
        void Insert(ContactInfo contactInfo);

        void Update(ContactInfo contactInfo);

        void Remove(ContactInfo contactInfo);
    }
}
