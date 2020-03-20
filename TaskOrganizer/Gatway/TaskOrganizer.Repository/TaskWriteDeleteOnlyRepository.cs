using AutoMapper;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.Repository.Entities;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.Repository
{
    public class TaskWriteDeleteOnlyRepository : ITaskWriteDeleteOnlyRepository
    {
        private readonly TaskOrganizerContext _taskOrganizerContext;

        public TaskWriteDeleteOnlyRepository(TaskOrganizerContext taskOrganizerContext)
        {
            _taskOrganizerContext = taskOrganizerContext;
        }

        public void Add(DomainTask domainTask)
        {
            var returned = MapperDomainTaskToRepositoryTask(domainTask); 

            _taskOrganizerContext.Add(returned);

            _taskOrganizerContext.SaveChanges();
        }

        public void Delete(DomainTask domainTask)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DomainTask domainTask)
        {
            throw new System.NotImplementedException();
        }


        private RepositoryTask MapperDomainTaskToRepositoryTask(DomainTask domainTask){
             var config = new MapperConfiguration(
                cfg => { cfg.CreateMap<DomainTask, RepositoryTask>();
            }); 

            IMapper mapper = config.CreateMapper();
            return mapper.Map<DomainTask, RepositoryTask>(domainTask);
        }
    }
}