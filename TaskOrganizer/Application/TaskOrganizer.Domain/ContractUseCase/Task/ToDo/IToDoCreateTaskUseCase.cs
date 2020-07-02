using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task.ToDo
{
    public interface IToDoCreateTaskUseCase
    {
        DomainTask CreateNewTask(DomainTask domainTask);
    }
}