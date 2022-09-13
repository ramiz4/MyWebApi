using Microsoft.AspNetCore.Mvc;
using MyWebApp.Core.Entities;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MyWebApp.Core.Repositories;

namespace MyWebApp.Api.Controllers;

public class SeedController : ODataController
{
    private static readonly string[] FirstNames = new[]
    {
        "Larry", "Chaz", "Cullen", "Camille", "Heidy", "Henry", "Pablo", "Charlize", "Armani", "Cale"
    };

    private static readonly string[] LastNames = new[]
    {
        "Estrada", "Rojas", "Tucker", "Wilkerson", "Mejia", "Parks", "Vaughan", "Winters", "Chandler", "Whitehead"
    };

    private static readonly string[] Addresses = new[]
    {
        "Bern", "Luzern", "Zürich", "Baden"
    };

    private readonly IRepositoryManager _repositoryManager;

    public SeedController(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    /// <summary>
    /// Seeds some sample random Persons.
    /// </summary>
    [HttpPost("SeedSampleData")]
    public IEnumerable<Person> SeedSampleData()
    {
        var newPersons = Enumerable.Range(1, 5).Select(index => {
            var newPerson = new Person(
                FirstNames[Random.Shared.Next(FirstNames.Length)],
                LastNames[Random.Shared.Next(LastNames.Length)],
                "lorem@impsum.com",
                "0041 76 123 45 67"
            );
            newPerson.ContactInfos = new List<ContactInfo>() {
                new ContactInfo(
                    ContactType.Private, 
                    "Bergstrasse", 
                    $"{index}", 
                    5430, 
                    Addresses[Random.Shared.Next(Addresses.Length)], 
                    "Schweiz",
                    newPerson.Id,
                    newPerson
                ),
                new ContactInfo(
                    ContactType.Business,
                    "Bahnhofstrasse",
                    $"{index}",
                    5400,
                    Addresses[Random.Shared.Next(Addresses.Length)],
                    "Schweiz",
                    newPerson.Id,
                    newPerson
                )
            };
            _repositoryManager.PersonRepository.Insert(newPerson);
            return newPerson;
        })
        .ToArray();

        return newPersons;
    }
}
