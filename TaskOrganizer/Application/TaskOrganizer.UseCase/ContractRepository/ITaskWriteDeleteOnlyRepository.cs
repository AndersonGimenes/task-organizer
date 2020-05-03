using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    public interface ITaskWriteDeleteOnlyRepository
    {
        DomainTask Add(DomainTask domainTask);
        void Update(DomainTask domainTask);
        void Delete(DomainTask domainTask);
    }
}
