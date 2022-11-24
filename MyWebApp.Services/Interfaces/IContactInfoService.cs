using Microsoft.AspNetCore.OData.Deltas;
using MyWebApp.Core.DTOs;

namespace MyWebApp.Services.Interfaces
{
    public interface IContactInfoService
    {
        Task<IEnumerable<ContactInfoDto>> GetAllByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        Task<ContactInfoDto> GetByIdAsync(Guid personId, Guid contactInfoId, CancellationToken cancellationToken);

        Task<ContactInfoDto> CreateAsync(Guid personId, ContactInfoForCreationDto contactInfoForCreationDto, CancellationToken cancellationToken = default);
        
        Task<ContactInfoDto> UpdateViaPutAsync(Guid contactInfoId, ContactInfoForUpdateDto contactInfoForCreationDto, CancellationToken cancellationToken = default);

        Task<ContactInfoDto> UpdateViaPatchAsync(Guid contactInfoId, Delta<ContactInfoForUpdateDto> contactInfoForCreationDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid personId, Guid contactInfoId, CancellationToken cancellationToken = default);
    }
}