using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Infrastructure;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]
    public class PersonsController : ODataController //ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
          if (_context.Persons == null)
          {
              return NotFound();
          }
            return await _context.Persons.ToListAsync();
        }

        // GET: api/Persons/8988bcee-6328-4f02-961d-daf19023ee2e
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Persons/8988bcee-6328-4f02-961d-daf19023ee2e
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // Patch: api/Persons/8988bcee-6328-4f02-961d-daf19023ee2e
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPerson(Guid id, Delta<Person> person)
        {
            var personToUpdate = await _context.Persons.FindAsync(id);
            if (personToUpdate == null)
            {
                return NotFound();
            }

            person.Patch(personToUpdate);

            try
            {
                _context.Update(personToUpdate);
                await _context.SaveChangesAsync();
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

            return Updated(personToUpdate);
        }

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
          if (_context.Persons == null)
          {
              return Problem("Entity set 'AppDbContext.Persons'  is null.");
          }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return Created(person);
            // return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
