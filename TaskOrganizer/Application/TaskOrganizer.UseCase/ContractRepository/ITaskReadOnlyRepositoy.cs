using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    interface ITaskReadOnlyRepositoy
    {
        IList<DomainTask> GetAll(DomainTask domainTask); 
    }
}
