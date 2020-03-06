using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    public interface ITaskReadOnlyRepositoy
    {
        IList<DomainTask> GetAll(); 
    }
}
