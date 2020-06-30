using System.Collections.Generic;
using TaskOrganizer.Domain.ContractUseCase.Task;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.Task
{
    public class TaskUseCase : ITaskUseCase
    {
        public DomainTask Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<DomainTask> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}