using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    public class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public RegisterTaskUseCase(ITaskWriteDeleteOnlyRepository taskReadWriteOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskReadWriteOnlyRepository; 
        }

        public void Register(DomainTask domainTask)
        {
            if(domainTask.EstimetedDate.Date.Equals(new DateTime().Date))
            {
                domainTask.EstimetedDate = DateTime.Now.Date.AddDays(30);
            }

            DecideFlow(domainTask);
            
        }

        private void DecideFlow(DomainTask domainTask)
        {
            if(domainTask.IsNew)
            {
                domainTask.CreateDate = DateTime.Now.Date;
                domainTask.Progress = Progress.ToDo;
                _taskWriteDeleteOnlyRepository.Add(domainTask);
            }
            else
            {
                _taskWriteDeleteOnlyRepository.Update(domainTask);
            }
        }
    }
}
