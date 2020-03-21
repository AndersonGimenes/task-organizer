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

        public IList<DomainTask> GetAll()
        {
            return _taskReadOnlyRepositoy.GetAll();
        }

        public DomainTask Get()
        {
            throw new System.NotImplementedException();
        }
    }
}
