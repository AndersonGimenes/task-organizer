using AutoMapper;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository.Entities;

namespace TaskOrganizer.Repository.Mapping
{
    public class MappingProfileRepository : Profile
    {
        public MappingProfileRepository()
        {
            CreateMap<DomainTask, RepositoryTask>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(x => x.TaskNumber))
                .ForMember(dest => dest.ProgressId, opt => opt.MapFrom(x => (int)x.Progress));

            CreateMap<RepositoryTask, DomainTask>()
                .ForMember(dest => dest.TaskNumber, opt => opt.MapFrom(x => x.TaskId))
                .ForMember(dest => dest.Progress, opt => opt.MapFrom(x => x.ProgressId));
        }
    }
}