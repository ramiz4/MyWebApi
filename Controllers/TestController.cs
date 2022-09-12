using Microsoft.AspNetCore.Mvc;
using MyWebApi.Infrastructure;
using MyWebApi.Models;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private static readonly string[] FirstNames = new[]
    {
        "Larry", "Chaz", "Cullen", "Camille", "Heidy", "Henry", "Pablo", "Charlize", "Armani", "Cale"
    };

    private static readonly string[] LastNames = new[]
    {
        "Estrada", "Rojas", "Tucker", "Wilkerson", "Mejia", "Parks", "Vaughan", "Winters", "Chandler", "Whitehead"
    };

    private readonly AppDbContext _context;
    private readonly ILogger<PersonsController> _logger;

    public TestController(AppDbContext context, ILogger<PersonsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Seeds some sample random Persons.
    /// </summary>
    [HttpPost("SeedSampleData")]
    public async Task<IEnumerable<Person>> SeedSampleData()
    {
        var newPersons = Enumerable.Range(1, 5).Select(index => {
            var newPerson = new Person(
                FirstNames[Random.Shared.Next(FirstNames.Length)],
                LastNames[Random.Shared.Next(LastNames.Length)],
                "lorem@impsum.com",
                "0041 76 123 45 67"
            );
            newPerson.ContactInfos = new List<ContactInfo>() {
                new ContactInfo(ContactType.Private, "Street lorem ipsum", $"{index}", 5400, "Baden", "CH")
            };
            return newPerson;
        })
        .ToArray();

        await Create(newPersons);

        return newPersons;
    }

    private async Task<IActionResult> Create(IEnumerable<Person> persons)
    {
        await _context.AddRangeAsync(persons);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
