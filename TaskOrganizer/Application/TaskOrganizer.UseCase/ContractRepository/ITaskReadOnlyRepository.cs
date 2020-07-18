using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    public interface ITaskReadOnlyRepository
    {
        DomainTask Get(int id);
        List<DomainTask> GetAll(); 
    }
}
