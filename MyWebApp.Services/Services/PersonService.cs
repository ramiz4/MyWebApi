using Mapster;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using MyWebApp.Core.DTOs;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Exceptions;
using MyWebApp.Core.Repositories;
using MyWebApp.Services.Interfaces;

namespace MyWebApp.Services.Services
{
    internal sealed class PersonService : IPersonService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PersonService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public IQueryable<PersonDto> GetAll()
        {
            var persons = _repositoryManager.PersonRepository.GetAll();

            var personsDto = persons.Adapt<List<PersonDto>>().AsQueryable();

            return personsDto;
        }

        public async Task<IQueryable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var persons = await _repositoryManager.PersonRepository.GetAllAsync(cancellationToken);

            var personsDto = persons.Adapt<List<PersonDto>>().AsQueryable();

            return personsDto;
        }

        public async Task<PersonDto> GetByIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            var personDto = person.Adapt<PersonDto>();

            return personDto;
        }

        public async Task<PersonDto> CreateAsync(PersonForCreationDto personForCreationDto, CancellationToken cancellationToken = default)
        {
            var person = personForCreationDto.Adapt<Person>();

            _repositoryManager.PersonRepository.Insert(person);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return person.Adapt<PersonDto>();
        }

        public async Task<PersonDto> UpdateViaPutAsync(Guid personId, PersonForUpdateDto personForUpdateDto, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            person = personForUpdateDto.Adapt<Person>();

            _repositoryManager.PersonRepository.Update(person);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return person.Adapt<PersonDto>();
        }

        public async Task<PersonDto> UpdateViaPatchAsync(Guid personId, Delta<PersonForUpdateDto> personForUpdateDto, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            var personForUpdateDtoFromDb = person.Adapt<PersonForUpdateDto>();

            personForUpdateDto.Patch(personForUpdateDtoFromDb);

            person = personForUpdateDtoFromDb.Adapt<Person>();

            _repositoryManager.PersonRepository.Update(person);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return person.Adapt<PersonDto>();
        }

        public async Task DeleteAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.PersonRepository.GetByIdAsync(personId, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(personId);
            }

            _repositoryManager.PersonRepository.Remove(person);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}