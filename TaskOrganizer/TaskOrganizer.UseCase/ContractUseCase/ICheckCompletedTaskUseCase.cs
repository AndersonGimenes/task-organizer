using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    interface ICheckCompletedTaskUseCase
    {
        bool CheckCompletedTask(DomainTask domainTask);
    }
}
