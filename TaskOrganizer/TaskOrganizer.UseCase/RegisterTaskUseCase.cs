using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        // rules***
        // there must be a estimeted date - case don't exists this, the system will put 30 days automatically
        // there must be a create date -the system will put automatically when the task is registered
        // when creating a new register the system will put automatically the progress with ToDo
        public void Register(DomainTask domainTask)
        {
            throw new System.NotImplementedException();
        }
    }
}
