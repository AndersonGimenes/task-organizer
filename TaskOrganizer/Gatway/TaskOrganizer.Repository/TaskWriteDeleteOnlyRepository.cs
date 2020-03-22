using AutoMapper;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.Repository.Entities;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.Repository
{
    public class TaskWriteDeleteOnlyRepository : ITaskWriteDeleteOnlyRepository
    {
        private readonly TaskOrganizerContext _context;

        public TaskWriteDeleteOnlyRepository(TaskOrganizerContext context)
        {
            _context = context;
        }

        public void Add(DomainTask domainTask)
        {
            var returned = MapperDomainTaskToRepositoryTask(domainTask); 
                        
            _context.Add(returned);
            _context.SaveChanges();
            
        }

        public void Delete(DomainTask domainTask)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DomainTask domainTask)
        {
            throw new System.NotImplementedException();
        }

        #region AuxiliaryMethods

        private RepositoryTask MapperDomainTaskToRepositoryTask(DomainTask domainTask){
            var config = new MapperConfiguration(
                cfg => { cfg.CreateMap<DomainTask, RepositoryTask>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(x => x.TaskNumeber))
                .ForMember(dest => dest.ProgressId, opt => opt.MapFrom(x => (int)x.Progress));
            }); 

            IMapper mapper = config.CreateMapper();
            return mapper.Map<DomainTask, RepositoryTask>(domainTask);            
        }

        #endregion
    }
}