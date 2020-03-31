using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    public class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public RegisterTaskUseCase(ITaskWriteDeleteOnlyRepository taskReadWriteOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskReadWriteOnlyRepository; 
        }

        public int Register(DomainTask domainTask)
        {
            if(domainTask.EstimatedDate.Date.Equals(new DateTime().Date))
            {
                domainTask.EstimatedDate = DateTime.Now.Date.AddDays(30);
            }

            return DecideFlow(domainTask);
            
        }

        private int DecideFlow(DomainTask domainTask)
        {
            if(domainTask.TaskNumeber.Equals(0))
            {
                domainTask.CreateDate = DateTime.Now.Date;
                domainTask.Progress = Progress.ToDo;
                return _taskWriteDeleteOnlyRepository.Add(domainTask);
            }
            
            _taskWriteDeleteOnlyRepository.Update(domainTask);
            
            return default;
        }
    }
}
