using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    public class GetTasksUseCase : IGetTasksUseCase
    {
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepositoy;

        public GetTasksUseCase(ITaskReadOnlyRepository taskReadOnlyRepositoy)
        {
            _taskReadOnlyRepositoy = taskReadOnlyRepositoy;
        }
        public DomainTask Get(int id)
        {
            return _taskReadOnlyRepositoy.Get(id);
        }

        public List<DomainTask> GetAll()
        {
            return _taskReadOnlyRepositoy.GetAll();
        }
    }
}
