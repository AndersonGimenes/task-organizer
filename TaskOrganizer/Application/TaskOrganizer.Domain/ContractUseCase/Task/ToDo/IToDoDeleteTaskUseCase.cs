using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase.Task.ToDo
{
    public interface IToDoDeleteTaskUseCase
    {
        void Delete(DomainTask domainTask);
    }
}