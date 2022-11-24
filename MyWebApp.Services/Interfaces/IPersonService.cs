using Microsoft.AspNetCore.OData.Deltas;
using MyWebApp.Core.DTOs;

namespace MyWebApp.Services.Interfaces
{
    public interface IPersonService
    {
        IQueryable<PersonDto> GetAll();

        Task<IQueryable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<PersonDto> GetByIdAsync(Guid personId, CancellationToken cancellationToken = default);

        Task<PersonDto> CreateAsync(PersonForCreationDto personForCreationDto, CancellationToken cancellationToken = default);

        Task<PersonDto> UpdateViaPutAsync(Guid personId, PersonForUpdateDto personForUpdateDto, CancellationToken cancellationToken = default);
        
        Task<PersonDto> UpdateViaPatchAsync(Guid personId, Delta<PersonForUpdateDto> personForUpdateDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid personId, CancellationToken cancellationToken = default);
    }
}