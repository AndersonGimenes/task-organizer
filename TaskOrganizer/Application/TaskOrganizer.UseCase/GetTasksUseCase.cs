using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    public class GetTasksUseCase : IGetTasksUseCase
    {
        private readonly ITaskReadOnlyRepositoy _taskReadOnlyRepositoy;

        public GetTasksUseCase(ITaskReadOnlyRepositoy taskReadOnlyRepositoy)
        {
            _taskReadOnlyRepositoy = taskReadOnlyRepositoy;
        }

        public IList<DomainTask> Get()
        {
            return _taskReadOnlyRepositoy.GetAll();
        }
    }
}
