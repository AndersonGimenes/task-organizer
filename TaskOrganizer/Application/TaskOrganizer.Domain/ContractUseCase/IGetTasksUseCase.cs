using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase
{
    public interface IGetTasksUseCase
    {
        List<DomainTask> GetAll();
        DomainTask Get(int id);
    }
}
