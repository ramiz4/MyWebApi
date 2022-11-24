using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
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

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetContactInfosByPersonId(Guid personId, CancellationToken cancellationToken = default)
        {
            var contactInfosDto = await _serviceManager.ContactInfoService.GetAllByPersonIdAsync(personId, cancellationToken);
            
            return Ok(contactInfosDto);
        }

        [EnableQuery]
        [HttpGet("{contactInfoId:guid}")]
        public async Task<IActionResult> GetContactInfoById(Guid personId, Guid contactInfoId, CancellationToken cancellationToken = default)
        {
            var contactInfo = await _serviceManager.ContactInfoService.GetByIdAsync(personId, contactInfoId, cancellationToken);
            
            return Ok(contactInfo);
        }

        [EnableQuery]
        [HttpPost]
        public async Task<IActionResult> CreateContactInfo([FromODataUri] Guid personId, [FromBody] ContactInfoForCreationDto contactInfoForCreationDto, CancellationToken cancellationToken)
        {
            var contactInfo = await _serviceManager.ContactInfoService.CreateAsync(personId, contactInfoForCreationDto, cancellationToken);
            return Created(contactInfo);
        }

        [EnableQuery]
        [HttpPut]
        public async Task<IActionResult> UpdateContactInfoViaPut([FromODataUri] Guid id, [FromODataBody] ContactInfoForUpdateDto contactInfo, CancellationToken cancellationToken)
        { 
            await _serviceManager.ContactInfoService.UpdateViaPutAsync(id, contactInfo, cancellationToken);
            
            return NoContent();
        }

        [EnableQuery]
        [HttpPatch]
        public async Task<IActionResult> UpdateContactInfoViaPatch([FromODataUri] Guid id, Delta<ContactInfoForUpdateDto> contactInfo, CancellationToken cancellationToken)
        {
            await _serviceManager.ContactInfoService.UpdateViaPatchAsync(id, contactInfo, cancellationToken);

            return NoContent();
        }

        [EnableQuery]
        [HttpDelete("{contactInfoId:guid}")]
        public async Task<IActionResult> DeleteContactInfo([FromODataUri] Guid personId, [FromODataUri] Guid contactInfoId, CancellationToken cancellationToken)
        {
            await _serviceManager.ContactInfoService.DeleteAsync(personId, contactInfoId, cancellationToken);
            
            return NoContent();
        }
    }
}
