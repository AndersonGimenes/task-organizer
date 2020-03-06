using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    public class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        private readonly ITaskReadWriteOnlyRepository _taskReadWriteOnlyRepository;

        public RegisterTaskUseCase(ITaskReadWriteOnlyRepository taskReadWriteOnlyRepository)
        {
            _taskReadWriteOnlyRepository = taskReadWriteOnlyRepository; 
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
                // call method to add a new task
                _taskReadWriteOnlyRepository.Add(domainTask);
            }
            else
            {
                // call method to updade a existing task
                _taskReadWriteOnlyRepository.Update(domainTask);
            }
        }
    }
}
