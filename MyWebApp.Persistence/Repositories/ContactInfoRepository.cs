using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public IQueryable<ContactInfo> GetAllByPersonId(Guid personId)
        {
            return _dbContext.ContactInfos.Where(x => x.PersonId == personId);
        }

        public IQueryable<ContactInfo> GetById(Guid contactInfoId)
        {
            return _dbContext.ContactInfos.Where(x => x.Id == contactInfoId);
        }

        public void Insert(ContactInfo contactInfo)
        {
            _dbContext.ContactInfos.Add(contactInfo);
            _dbContext.SaveChanges();
        }

        public void Remove(ContactInfo contactInfo)
        {
            _dbContext.ContactInfos.Remove(contactInfo);
            _dbContext.SaveChanges();
        }

        public void Update(ContactInfo contactInfo)
        {
            _dbContext.Entry(contactInfo).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
