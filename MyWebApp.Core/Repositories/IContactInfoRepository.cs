using MyWebApp.Core.Entities;

namespace MyWebApp.Core.Repositories
{
    public interface IContactInfoRepository
    {
        Task<IEnumerable<ContactInfo?>> GetAllByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        Task<ContactInfo?> GetByIdAsync(Guid contactInfoId, CancellationToken cancellationToken = default);
        
        void Insert(ContactInfo contactInfo);

        void Update(ContactInfo contactInfo);

        void Remove(ContactInfo contactInfo);
    }
}
