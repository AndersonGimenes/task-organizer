using AutoMapper;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;

namespace TaskOrganizer.Api.Mapping
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            CreateMap<DomainTask, TaskModel>();

            CreateMap<TaskModel, DomainTask>();

            CreateMap<DomainTask, ToDoModel>()
                .ForMember(dest => dest.TaskModel, src => src.MapFrom(x => x));

            CreateMap<ToDoModel, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskModel.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskModel.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskModel.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskModel.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskModel.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskModel.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskModel.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskModel.EndDate));
        }
    }
}