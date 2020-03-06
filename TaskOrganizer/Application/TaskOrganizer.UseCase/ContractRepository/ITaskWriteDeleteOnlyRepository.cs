using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    public interface ITaskWriteDeleteOnlyRepository
    {
        void Add(DomainTask domainTask);
        void Update(DomainTask domainTask);
        void Delete(DomainTask domainTask);
    }
}
