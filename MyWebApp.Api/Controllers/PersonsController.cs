using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Query;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Repositories;

namespace MyWebApp.Api.Controllers
{
    public class PersonsController : ODataController
    {
        private readonly IRepositoryManager _repositoryManager;

        public PersonsController(IRepositoryManager context)
        {
            _repositoryManager = context;
        }

        [EnableQuery]
        public IQueryable<Person> Get()
        {
            return _repositoryManager.PersonRepository.GetAll();
        }

        //[HttpGet("{{id}}")]
        [EnableQuery]
        public SingleResult<Person> GetById(Guid id)
        {
            var person = _repositoryManager.PersonRepository.GetById(id);
            return SingleResult.Create(person);
        }

        [EnableQuery]
        public IActionResult Put([FromODataUri] Guid id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            try
            {
                _repositoryManager.PersonRepository.Update(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        [EnableQuery]
        public IActionResult Patch([FromODataUri] Guid id, Delta<Person> person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingPerson = _repositoryManager.PersonRepository.GetById(id).First();
            if (existingPerson == null)
            {
                return NotFound();
            }

            person.Patch(existingPerson);

            // for testing $batch isolation scope
            if (existingPerson.FirstName == "invalid_name") { 
                return BadRequest();
            }

            try
            {
                _repositoryManager.PersonRepository.Update(existingPerson);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(existingPerson);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Person person)
        {
            _repositoryManager.PersonRepository.Insert(person);
            return Created(person);
        }

        [EnableQuery]
        public IActionResult Delete([FromODataUri] Guid id)
        {
            var existingPerson = _repositoryManager.PersonRepository.GetById(id).First();
            if (existingPerson == null)
            {
                return NotFound();
            }

            _repositoryManager.PersonRepository.Remove(existingPerson);

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _repositoryManager.PersonRepository.GetById(id).First() != null;
        }
    }
}
