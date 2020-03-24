using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using System.Linq;
using AutoMapper;
using TaskOrganizer.Repository.Entities;

namespace TaskOrganizer.Repository
{
    public class TaskReadOnlyRepository : ITaskReadOnlyRepository
    {
        private readonly TaskOrganizerContext _context;

        public TaskReadOnlyRepository(TaskOrganizerContext context)
        {
            _context = context;
        }

        public DomainTask Get(int id)
        {
            var domainTask = _context.RepositoryTasks.Single(x => x.TaskId.Equals(id));
            return RepositoryTaskToMapperDomainTask(domainTask);
        }

        public IList<DomainTask> GetAll()
        {
            var domainTask = _context.RepositoryTasks.Select(x => x).ToList();
            return ReturnDomainTaskList(domainTask);
        }

        #region AuxiliaryMethods

        private IList<DomainTask> ReturnDomainTaskList(List<RepositoryTask> list)
        {
            var listReturn = new List<DomainTask>();
            foreach(var item in list)
            {
                listReturn.Add(RepositoryTaskToMapperDomainTask(item));
            }

            return listReturn;
        }

        private DomainTask RepositoryTaskToMapperDomainTask(RepositoryTask repositoryTask){
            var config = new MapperConfiguration(
                cfg => { cfg.CreateMap<RepositoryTask, DomainTask>()
                .ForMember(dest => dest.TaskNumeber, opt => opt.MapFrom(x => x.TaskId))
                .ForMember(dest => dest.Progress, opt => opt.MapFrom(x => (int)x.ProgressId));
            }); 

            IMapper mapper = config.CreateMapper();
            return mapper.Map<RepositoryTask,DomainTask>(repositoryTask);            
        }

        #endregion
    }
}