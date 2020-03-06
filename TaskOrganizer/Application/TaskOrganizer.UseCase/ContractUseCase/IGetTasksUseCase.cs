using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    public interface IGetTasksUseCase
    {
        IList<DomainTask> Get();
    }
}
