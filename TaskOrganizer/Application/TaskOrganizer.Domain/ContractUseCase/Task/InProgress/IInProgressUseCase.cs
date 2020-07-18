using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task.InProgress
{
    public interface IInProgressUseCase 
    {
        void UpdateTask(DomainTask domainTask);
        void UpdateProgressTask(DomainTask domainTask);
    }
}