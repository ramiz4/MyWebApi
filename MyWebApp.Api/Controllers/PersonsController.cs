using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MyWebApp.Core.DTOs;
using MyWebApp.Services.Interfaces;

namespace MyWebApp.Api.Controllers
{
    public class PersonsController : ODataController
    {
        private readonly IServiceManager _serviceManager;
        
        public PersonsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [EnableQuery]
        [HttpGet("api/Persons/all")]
        public IActionResult GetAllPersons()
        {
            var persons = _serviceManager.PersonService.GetAll();
            return Ok(persons);
        }

        [EnableQuery]
        // Folgende zwei Attribute dürfen nicht gesetzt werden, da Swagger dann die Doku nicht mehr generieren kann. 
        [HttpGet("api/Persons/old")]
        //[HttpGet("api/Persons/$count")]
        public async Task<ActionResult<IQueryable<PersonDto>>> GetPersons(CancellationToken cancellationToken)
        {
            var personsDto = await _serviceManager.PersonService.GetAllAsync(cancellationToken);

            return Ok(personsDto);
        }

        [EnableQuery]
        [HttpGet("api/Persons({personId:guid})")]
        [HttpGet("api/Persons/{personId:guid}")]
        public async Task<IActionResult> GetPersonById(Guid personId, CancellationToken cancellationToken)
        {
            var personDto = await _serviceManager.PersonService.GetByIdAsync(personId, cancellationToken);
            
            return Ok(personDto);
        }

        [HttpPost("api/Persons")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonForCreationDto personForCreationDto)
        {
            var personDto = await _serviceManager.PersonService.CreateAsync(personForCreationDto);
            return Ok(personDto);
        }

        [HttpPut("api/Persons/{personId:guid}")]
        public async Task<IActionResult> UpdatePersonViaPut([FromODataUri] Guid personId, [FromBody] PersonForUpdateDto personForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.PersonService.UpdateViaPutAsync(personId, personForUpdateDto, cancellationToken);
            return NoContent();
        }

        [HttpPatch("api/Persons/{personId:guid}")]
        public async Task<IActionResult> UpdatePersonViaPatch([FromODataUri] Guid personId, [FromBody] Delta<PersonForUpdateDto> personForUpdateDtoDelta, CancellationToken cancellationToken)
        {
            await _serviceManager.PersonService.UpdateViaPatchAsync(personId, personForUpdateDtoDelta, cancellationToken);
            return NoContent();
        }
        
        [HttpDelete("api/Persons/{personId:guid}")]
        public async Task<IActionResult> DeletePerson([FromODataUri] Guid personId)
        {
            await _serviceManager.PersonService.DeleteAsync(personId);
            return NoContent();
        }
    }
}
