using AutoMapper;
using MyWebApp.Core.DTOs;
using MyWebApp.Core.Entities;

namespace MyWebApp.Api
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<ContactInfo, ContactInfoDto>();
        }
    }
}
