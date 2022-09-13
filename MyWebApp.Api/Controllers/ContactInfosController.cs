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
    public class ContactInfosController : ODataController
    {
        private readonly IRepositoryManager _repositoryManager;

        public ContactInfosController(IRepositoryManager repo)
        {
            _repositoryManager = repo;
        }

        [EnableQuery]
        public IQueryable<ContactInfo> Get([FromODataUri] Guid personId)
        {
            return _repositoryManager.ContactInfoRepository.GetAllByPersonId(personId);
        }

        [EnableQuery]
        public SingleResult<ContactInfo> GetById([FromODataUri] Guid id)
        {
            var contactInfo = _repositoryManager.ContactInfoRepository.GetById(id); //.Where(c => c.Id == id);
            return SingleResult.Create(contactInfo);
        }

        [EnableQuery]
        public IActionResult Put([FromODataUri] Guid id, [FromODataBody] ContactInfo contactInfo)
        {
            if (id != contactInfo.Id)
            {
                return BadRequest();
            }

            try
            {
                _repositoryManager.ContactInfoRepository.Update(contactInfo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [EnableQuery]
        public IActionResult Patch([FromODataUri] Guid id, Delta<ContactInfo> contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingContactInfo = _repositoryManager.ContactInfoRepository.GetById(id).First();
            if (existingContactInfo == null)
            {
                return NotFound();
            }

            contactInfo.Patch(existingContactInfo);
            try
            {
                _repositoryManager.ContactInfoRepository.Update(existingContactInfo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(existingContactInfo);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] ContactInfo contactInfo)
        {
            _repositoryManager.ContactInfoRepository.Insert(contactInfo);
            return Created(contactInfo);
        }

        [EnableQuery]
        public IActionResult Delete([FromODataUri] Guid id)
        {
            var contactInfo = _repositoryManager.ContactInfoRepository.GetById(id).First();
            if (contactInfo == null)
            {
                return NotFound();
            }

            _repositoryManager.ContactInfoRepository.Remove(contactInfo);
            return NoContent();
        }

        private bool ContactInfoExists(Guid id)
        {
            return _repositoryManager.ContactInfoRepository.GetById(id).First() != null;
        }
    }
}
