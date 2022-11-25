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
        var newPersons = Enumerable.Range(1, 5).Select(index =>
        {
            var newPerson = new Person()
            {
                FirstName = FirstNames[Random.Shared.Next(FirstNames.Length)],
                LastName = LastNames[Random.Shared.Next(LastNames.Length)],
                Email = "lorem@impsum.com",
                Mobile = "0041 76 123 45 67",
                ContactInfos = new List<ContactInfo>()
            };
            newPerson.ContactInfos = new List<ContactInfo>()
            {
                new()
                {
                    Type = ContactType.Private,
                    Street = "Bergstrasse",
                    StreetNumber = $"{index}",
                    PostalCode = 5430,
                    City = Addresses[Random.Shared.Next(Addresses.Length)],
                    Country = "Schweiz",
                    PersonId = newPerson.Id,
                    Person = newPerson
                },
                new()
                {
                    Type = ContactType.Business,
                    Street = "Bahnhofstrasse",
                    StreetNumber = $"{index}",
                    PostalCode = 5400,
                    City = Addresses[Random.Shared.Next(Addresses.Length)],
                    Country = "Schweiz",
                    PersonId = newPerson.Id,
                    Person = newPerson
                }
            };
            _repositoryManager.PersonRepository.Insert(newPerson);
            _repositoryManager.UnitOfWork.SaveChanges();
            return newPerson;
        });

        return newPersons;
    }
}
