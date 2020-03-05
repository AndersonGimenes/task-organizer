using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    interface IRegisterTaskUseCase
    {
        // create a new task or update a existing task
        void Register(DomainTask domainTask);
    }
}
