using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    class RegisterTask : IRegisterTask
    {
        // rules***
        // there must be a title
        // there must be a descripiton
        // there must be a estimeted date - case don't exists this the system will put 30 days automatically
        // the create date will put automatically when the task is registered
        public void Register(DomainTask domainTask)
        {
            throw new System.NotImplementedException();
        }
    }
}
