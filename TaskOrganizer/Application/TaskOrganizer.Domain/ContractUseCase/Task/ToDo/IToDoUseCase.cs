using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task.ToDo
{
    public interface IToDoUseCase : IBaseTaskUseCase
    {
        DomainTask InsertNewTask(DomainTask domainTask);
        
        void Delete(DomainTask domainTask);
    }
}