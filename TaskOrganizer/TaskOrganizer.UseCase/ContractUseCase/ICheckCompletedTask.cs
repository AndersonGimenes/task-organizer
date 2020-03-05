using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    interface ICheckCompletedTask
    {
        bool CheckCompletedTask(DomainTask domainTask);
    }
}
