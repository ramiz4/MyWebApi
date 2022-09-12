using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Infrastructure;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]
    public class ContactInfosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactInfosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ContactInfos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactInfo>>> GetContactInfos()
        {
            if (_context.ContactInfos == null)
            {
                return NotFound();
            }
            return await _context.ContactInfos.ToListAsync();
        }

        // GET: api/ContactInfos/8988bcee-6328-4f02-961d-daf19023ee2e
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInfo>> GetContactInfo(Guid id)
        {
            if (_context.ContactInfos == null)
            {
                return NotFound();
            }
            var contactInfo = await _context.ContactInfos.FindAsync(id);

            if (contactInfo == null)
            {
                return NotFound();
            }

            return contactInfo;
        }

        // PUT: api/ContactInfos/8988bcee-6328-4f02-961d-daf19023ee2e
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactInfo(Guid id, ContactInfo contactInfo)
        {
            if (id != contactInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/ContactInfos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactInfo>> PostContactInfo(ContactInfo contactInfo)
        {
            if (_context.ContactInfos == null)
            {
                return Problem("Entity set 'AppDbContext.ContactInfos'  is null.");
            }
            _context.ContactInfos.Add(contactInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactInfo", new { id = contactInfo.Id }, contactInfo);
        }

        // DELETE: api/ContactInfos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInfo(Guid id)
        {
            if (_context.ContactInfos == null)
            {
                return NotFound();
            }
            var contactInfo = await _context.ContactInfos.FindAsync(id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            _context.ContactInfos.Remove(contactInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactInfoExists(Guid id)
        {
            return (_context.ContactInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
