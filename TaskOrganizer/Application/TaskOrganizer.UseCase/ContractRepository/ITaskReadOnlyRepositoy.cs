using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    public interface ITaskReadOnlyRepository
    {
        IList<DomainTask> GetAll(); 
        DomainTask Get(int id);
    }
}
