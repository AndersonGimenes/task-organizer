using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase
{
    public interface IRegisterTaskUseCase
    {
        DomainTask Register(DomainTask domainTask);
    }
}
