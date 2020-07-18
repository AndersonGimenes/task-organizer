using System.Collections.Generic;
using TaskOrganizer.Domain.ContractUseCase.Task;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.UseCase.Task
{
    public class TaskUseCase : ITaskUseCase
    {
        private ITaskReadOnlyRepository _taskReadOnlyRepository;

        public TaskUseCase(ITaskReadOnlyRepository taskReadOnlyRepository)
        {
            _taskReadOnlyRepository = taskReadOnlyRepository;
        }

        public DomainTask Get(int id)
        {
            return _taskReadOnlyRepository.Get(id);
        }

        public IList<DomainTask> GetAll()
        {
            return _taskReadOnlyRepository.GetAll();
        }
    }
}