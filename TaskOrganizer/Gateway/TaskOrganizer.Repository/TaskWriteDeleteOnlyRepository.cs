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

        public int Add(DomainTask domainTask)
        {
            var repositoryTask = MapperDomainTaskToRepositoryTask(domainTask); 
                        
            _context.Add(repositoryTask);
            _context.SaveChanges();

            return repositoryTask.TaskId;
        }

        public void Delete(DomainTask domainTask)
        {
            var repositoryTask = MapperDomainTaskToRepositoryTask(domainTask);

            _context.Remove(repositoryTask);
            _context.SaveChanges();
        }

        public void Update(DomainTask domainTask)
        {
            var repositoryTask = MapperDomainTaskToRepositoryTask(domainTask);
            
            _context.Update(repositoryTask);
            _context.SaveChanges();
        }

        #region AuxiliaryMethods

        private RepositoryTask MapperDomainTaskToRepositoryTask(DomainTask domainTask){
            var config = new MapperConfiguration(
                cfg => { cfg.CreateMap<DomainTask, RepositoryTask>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(x => x.TaskNumeber))
                .ForMember(dest => dest.ProgressId, opt => opt.MapFrom(x => (int)x.Progress));
            }); 

            return config.CreateMapper().Map<DomainTask, RepositoryTask>(domainTask);            
        }

        #endregion
    }
}