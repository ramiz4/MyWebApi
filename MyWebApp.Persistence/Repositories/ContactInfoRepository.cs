using MyWebApp.Core.Entities;
using MyWebApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MyWebApp.Persistence.Repositories
{
    internal sealed class ContactInfoRepository : IContactInfoRepository
    {
        private readonly AppDbContext _dbContext;

        public ContactInfoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ContactInfo?>> GetAllByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ContactInfos.Where(x => x.PersonId == personId).ToListAsync(cancellationToken);
        }

        public async Task<ContactInfo?> GetByIdAsync(Guid contactInfoId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ContactInfos.FirstOrDefaultAsync(x => x.Id == contactInfoId, cancellationToken);
        }

        public void Insert(ContactInfo contactInfo)
        {
            _dbContext.ContactInfos.Add(contactInfo);
        }

        public void Remove(ContactInfo contactInfo)
        {
            _dbContext.ContactInfos.Remove(contactInfo);
        }

        public void Update(ContactInfo contactInfo)
        {
            _dbContext.Update(contactInfo);
        }
    }
}
