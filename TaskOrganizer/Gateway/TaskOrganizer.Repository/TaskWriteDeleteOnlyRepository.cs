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
        private readonly IMapper _mapper;

        public TaskWriteDeleteOnlyRepository(TaskOrganizerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DomainTask Add(DomainTask domainTask)
        {
            var repositoryTask = _mapper.Map<RepositoryTask>(domainTask);
                            
            _context.Add(repositoryTask);
            _context.SaveChanges();

            domainTask.TaskNumber = repositoryTask.TaskId;

            return domainTask;
        }

        public void Delete(DomainTask domainTask)
        {
            var repositoryTask = _mapper.Map<RepositoryTask>(domainTask);

            _context.Remove(repositoryTask);
            _context.SaveChanges();
        }

        public void Update(DomainTask domainTask)
        {
            var repositoryTask = _mapper.Map<RepositoryTask>(domainTask);
            
            _context.Update(repositoryTask);
            _context.SaveChanges();
        }

    }
}