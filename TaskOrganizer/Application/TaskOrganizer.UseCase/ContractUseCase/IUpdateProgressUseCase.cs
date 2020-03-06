using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    interface IUpdateProgressUseCase
    {
        // update stage task progress
        void UpdateStage(DomainTask domainTask);
    }
}
