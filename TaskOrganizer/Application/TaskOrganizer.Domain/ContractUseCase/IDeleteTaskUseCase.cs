using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase
{
    public interface IDeleteTaskUseCase
    {
        void Delete(DomainTask domainTask);
    }
}