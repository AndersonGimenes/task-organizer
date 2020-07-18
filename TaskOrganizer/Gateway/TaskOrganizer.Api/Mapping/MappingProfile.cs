using AutoMapper;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Api.Models.Request;
using TaskOrganizer.Api.Models.Response;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Api.Mapping
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            CreateMap<DomainTask, TaskBase>();

            CreateMap<TaskBase, DomainTask>();

            CreateMap<DomainTask, TaskResponse>()
                .ForMember(dest => dest.TaskBase, src => src.MapFrom(x => x));

            CreateMap<DomainTask, ToDoTaskResponse>()
                .ForMember(dest => dest.TaskBase, src => src.MapFrom(x => x));

            CreateMap<ToDoTaskRequest, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskBase.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskBase.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskBase.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskBase.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskBase.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskBase.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskBase.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskBase.EndDate));

            CreateMap<InProgressTaskRequest, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskBase.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskBase.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskBase.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskBase.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskBase.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskBase.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskBase.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskBase.EndDate));

            CreateMap<DoneTaskRequest, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskBase.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskBase.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskBase.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskBase.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskBase.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskBase.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskBase.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskBase.EndDate));
        }
    }
}