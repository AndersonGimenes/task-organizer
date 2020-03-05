using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractUseCase
{
    interface IUpdateProgress
    {
        // update stage task progress
        void UpdateStage(DomainTask domainTask);
    }
}
