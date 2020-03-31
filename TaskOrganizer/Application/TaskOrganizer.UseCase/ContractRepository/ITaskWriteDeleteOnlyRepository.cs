using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    public interface ITaskWriteDeleteOnlyRepository
    {
        int Add(DomainTask domainTask);
        void Update(DomainTask domainTask);
        void Delete(DomainTask domainTask);
    }
}
