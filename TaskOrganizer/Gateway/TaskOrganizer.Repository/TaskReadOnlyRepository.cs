using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TaskOrganizer.Repository
{
    public class TaskReadOnlyRepository : ITaskReadOnlyRepository
    {
        private readonly TaskOrganizerContext _context;
        private readonly IMapper _mapper;

        public TaskReadOnlyRepository(TaskOrganizerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DomainTask Get(int id)
        {
            var repositoryTask = _context
                                    .RepositoryTasks
                                    .AsNoTracking()
                                    .Single(x => x.TaskId.Equals(id));

            return _mapper.Map<DomainTask>(repositoryTask);
        }

        public List<DomainTask> GetAll()
        {
            var repositoryTasks = _context
                                    .RepositoryTasks
                                    .AsNoTracking()
                                    .ToList();
            
            return _mapper.Map<List<DomainTask>>(repositoryTasks);
        }
        
    }
}