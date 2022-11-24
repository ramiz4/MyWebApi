using Mapster;
using Microsoft.AspNetCore.OData.Deltas;
using MyWebApp.Core.DTOs;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Exceptions;
using MyWebApp.Core.Repositories;
using MyWebApp.Services.Interfaces;

namespace MyWebApp.Services.Services
{
    internal sealed class ContactInfoService : IContactInfoService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ContactInfoService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<IEnumerable<ContactInfoDto>> GetAllByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var contactInfos = await _repositoryManager.ContactInfoRepository.GetAllByPersonIdAsync(personId, cancellationToken);

            var contactInfosDto = contactInfos.Adapt<IEnumerable<ContactInfoDto>>();

            return contactInfosDto;
        }

        public async Task<ContactInfoDto> GetByIdAsync(Guid personId, Guid contactInfoId, CancellationToken cancellationToken)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            var contactInfo = await _repositoryManager.ContactInfoRepository.GetByIdAsync(contactInfoId, cancellationToken);

            if (contactInfo is null)
            {
                throw new ContactInfoNotFoundException(contactInfoId);
            }

            if (contactInfo.PersonId != person.Id)
            {
                throw new ContactInfoDoesNotBelongToPersonException(person.Id, contactInfo.Id);
            }

            var contactInfoDto = contactInfo.Adapt<ContactInfoDto>();

            return contactInfoDto;
        }

        public async Task<ContactInfoDto> CreateAsync(Guid personId, ContactInfoForCreationDto contactInfoForCreationDto, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            var contactInfo = contactInfoForCreationDto.Adapt<ContactInfo>();

            contactInfo.PersonId = person.Id;

            _repositoryManager.ContactInfoRepository.Insert(contactInfo);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return contactInfo.Adapt<ContactInfoDto>();
        }

        public async Task<ContactInfoDto> UpdateViaPutAsync(Guid contactInfoId, ContactInfoForUpdateDto contactInfoForUpdateDto, CancellationToken cancellationToken = default)
        {
            var contactInfo = await _repositoryManager.ContactInfoRepository.GetByIdAsync(contactInfoId, cancellationToken);

            if (contactInfo is null)
            {
                throw new ContactInfoNotFoundException(contactInfoId);
            }

            contactInfo = contactInfoForUpdateDto.Adapt<ContactInfo>();
            
            _repositoryManager.ContactInfoRepository.Update(contactInfo);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return contactInfo.Adapt<ContactInfoDto>();
        }

        public async Task<ContactInfoDto> UpdateViaPatchAsync(Guid contactInfoId, Delta<ContactInfoForUpdateDto> contactInfoForUpdateDto, CancellationToken cancellationToken = default)
        {
            var contactInfo = await _repositoryManager.ContactInfoRepository.GetByIdAsync(contactInfoId, cancellationToken);

            if (contactInfo is null)
            {
                throw new ContactInfoNotFoundException(contactInfoId);
            }

            var contactInfoForUpdateDtoFromDb = contactInfo.Adapt<ContactInfoForUpdateDto>();

            contactInfoForUpdateDto.Patch(contactInfoForUpdateDtoFromDb);

            contactInfo = contactInfoForUpdateDtoFromDb.Adapt<ContactInfo>();

            _repositoryManager.ContactInfoRepository.Update(contactInfo);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return contactInfo.Adapt<ContactInfoDto>();
        }

        public async Task DeleteAsync(Guid personId, Guid contactInfoId, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            var contactInfo = await _repositoryManager.ContactInfoRepository.GetByIdAsync(contactInfoId, cancellationToken);

            if (contactInfo is null)
            {
                throw new ContactInfoNotFoundException(contactInfoId);
            }

            if (contactInfo.PersonId != person.Id)
            {
                throw new ContactInfoDoesNotBelongToPersonException(person.Id, contactInfo.Id);
            }

            _repositoryManager.ContactInfoRepository.Remove(contactInfo);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
