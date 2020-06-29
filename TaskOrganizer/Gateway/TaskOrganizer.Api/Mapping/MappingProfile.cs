using AutoMapper;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;

namespace TaskOrganizer.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DomainTask, TaskModel>();

            CreateMap<TaskModel, DomainTask>();
        }
    }
}