using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task
{
    public interface IBaseTaskUseCase
    {
        void UpdateTask(DomainTask domainTask);
    }
}