using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task.ToDo
{
    public interface IToDoUpdateTaskUseCase
    {
        void UpdateTask(DomainTask domainTask);
    }
}