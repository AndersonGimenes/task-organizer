using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task.Done
{
    public interface IDoneUseCase 
    {
        void UpdateProgressTask(DomainTask domainTask);
    }
}