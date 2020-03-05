using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    interface IRegisterTask
    {
        // create a new task or update a existing task
        void Register(DomainTask domainTask);
    }
}
