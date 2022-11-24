using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MyWebApp.Core.DTOs;
using MyWebApp.Services.Interfaces;

namespace MyWebApp.Api.Controllers
{
    [ApiController]
    [Route("api/Persons")]
    public class PersonsController : ODataController
    {
        private readonly IServiceManager _serviceManager;
        
        public PersonsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [EnableQuery]
        [HttpGet("all")]
        public IActionResult GetAllPersons()
        {
            var persons = _serviceManager.PersonService.GetAll();
            return Ok(persons);
        }

        [EnableQuery]
        // Folgende zwei Attribute dürfen nicht gesetzt werden, da Swagger dann die Doku nicht mehr generieren kann. 
        [HttpGet("old")]
        //[HttpGet("api/Persons/$count")]
        public async Task<ActionResult<IQueryable<PersonDto>>> GetPersons(CancellationToken cancellationToken)
        {
            var personsDto = await _serviceManager.PersonService.GetAllAsync(cancellationToken);

            return Ok(personsDto);
        }

        [EnableQuery]
        [HttpGet("{personId:guid}")]
        public async Task<IActionResult> GetPersonById(Guid personId, CancellationToken cancellationToken)
        {
            var personDto = await _serviceManager.PersonService.GetByIdAsync(personId, cancellationToken);
            
            return Ok(personDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(PersonForCreationDto personForCreationDto)
        {
            var personDto = await _serviceManager.PersonService.CreateAsync(personForCreationDto);
            return Ok(personDto);
        }

        [HttpPut("{personId:guid}")]
        public async Task<IActionResult> UpdatePersonViaPut(Guid personId, PersonForUpdateDto personForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.PersonService.UpdateViaPutAsync(personId, personForUpdateDto, cancellationToken);
            return NoContent();
        }

        [HttpPatch("{personId:guid}")]
        public async Task<IActionResult> UpdatePersonViaPatch(Guid personId, Delta<PersonForUpdateDto> personForUpdateDtoDelta, CancellationToken cancellationToken)
        {
            await _serviceManager.PersonService.UpdateViaPatchAsync(personId, personForUpdateDtoDelta, cancellationToken);
            return NoContent();
        }
        
        [HttpDelete("{personId:guid}")]
        public async Task<IActionResult> DeletePerson([FromODataUri] Guid personId)
        {
            await _serviceManager.PersonService.DeleteAsync(personId);
            return NoContent();
        }
    }
}
