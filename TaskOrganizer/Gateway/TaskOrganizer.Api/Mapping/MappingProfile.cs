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

            CreateMap<DomainTask, GetTaskResponse>()
                .ForMember(dest => dest.TaskResponse, src => src.MapFrom(x => x));

            CreateMap<DomainTask, ToDoTaskResponse>()
                .ForMember(dest => dest.TaskResponse, src => src.MapFrom(x => x));

            CreateMap<ToDoTaskRequest, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskRequest.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskRequest.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskRequest.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskRequest.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskRequest.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskRequest.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskRequest.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskRequest.EndDate));

            CreateMap<InProgressTaskRequest, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskRequest.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskRequest.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskRequest.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskRequest.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskRequest.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskRequest.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskRequest.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskRequest.EndDate));

            CreateMap<DoneTaskRequest, DomainTask>()
                .ForMember(dest => dest.TaskNumber, src => src.MapFrom(x => x.TaskRequest.TaskNumber))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.TaskRequest.Title))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.TaskRequest.Description))
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => x.TaskRequest.CreateDate))
                .ForMember(dest => dest.EstimatedDate, src => src.MapFrom(x => x.TaskRequest.EstimatedDate))
                .ForMember(dest => dest.Progress, src => src.MapFrom(x => x.TaskRequest.Progress))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.TaskRequest.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.TaskRequest.EndDate));
        }
    }
}