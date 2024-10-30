// Crea una nueva clase llamada MappingProfile.cs
using AutoMapper;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configura el mapeo entre PersonDTO y Person
            CreateMap<PersonDTO, Person>();
            CreateMap<Person, PersonDTO>();

            CreateMap<CreatePersonDTO, Person>();
            CreateMap<CreateTaskDTO, TaskEntity>();

            // Configura el mapeo entre TaskDTO y TaskEntity
            CreateMap<TaskDTO, TaskEntity>();
            CreateMap<TaskEntity, TaskDTO>();
        }
    }
}