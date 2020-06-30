using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task
{
    public interface ITaskUseCase
    {
        IList<DomainTask> GetAll();
        DomainTask Get(int id);
    }
}