using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MyWebApp.Core.DTOs;
using MyWebApp.Services.Interfaces;

namespace MyWebApp.Api.Controllers
{
    [ApiController]
    [Route("api/Persons/{personId:guid}/ContactInfos")]
    public class ContactInfosController : ODataController
    {
        private readonly IServiceManager _serviceManager;

        public ContactInfosController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactInfosByPersonId(Guid personId, CancellationToken cancellationToken = default)
        {
            var contactInfosDto = await _serviceManager.ContactInfoService.GetAllByPersonIdAsync(personId, cancellationToken);
            return Ok(contactInfosDto);
        }

        [HttpGet("{contactInfoId:guid}")]
        public async Task<IActionResult> GetContactInfoById(Guid personId, Guid contactInfoId, CancellationToken cancellationToken = default)
        {
            var contactInfo = await _serviceManager.ContactInfoService.GetByIdAsync(personId, contactInfoId, cancellationToken);
            return Ok(contactInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactInfo(Guid personId, ContactInfoForCreationDto contactInfoForCreationDto, CancellationToken cancellationToken)
        {
            var contactInfo = await _serviceManager.ContactInfoService.CreateAsync(personId, contactInfoForCreationDto, cancellationToken);
            return Created(contactInfo);
        }

        [HttpPut("{contactInfoId:guid}")]
        public async Task<IActionResult> UpdateContactInfoViaPut(Guid personId, Guid contactInfoId, ContactInfoForUpdateDto contactInfo, CancellationToken cancellationToken)
        { 
            await _serviceManager.ContactInfoService.UpdateViaPutAsync(contactInfoId, contactInfo, cancellationToken);
            return NoContent();
        }

        [HttpPatch("{contactInfoId:guid}")]
        public async Task<IActionResult> UpdateContactInfoViaPatch(Guid personId, Guid contactInfoId, Delta<ContactInfoForUpdateDto> contactInfo, CancellationToken cancellationToken)
        {
            await _serviceManager.ContactInfoService.UpdateViaPatchAsync(contactInfoId, contactInfo, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{contactInfoId:guid}")]
        public async Task<IActionResult> DeleteContactInfo(Guid personId, Guid contactInfoId, CancellationToken cancellationToken)
        {
            await _serviceManager.ContactInfoService.DeleteAsync(personId, contactInfoId, cancellationToken);
            return NoContent();
        }
    }
}
