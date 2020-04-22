using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.UseCase
{
    public class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;

        public RegisterTaskUseCase(ITaskWriteDeleteOnlyRepository taskReadWriteOnlyRepository, ITaskReadOnlyRepository taskReadOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskReadWriteOnlyRepository; 
            _taskReadOnlyRepository = taskReadOnlyRepository;
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
                domainTask.Progress = Progress.ToDo;
                return _taskWriteDeleteOnlyRepository.Add(domainTask);
            }            

            if(domainTask.StartDate != null)
            {
                CheckFieldsInProgress(domainTask);
                domainTask.Progress = Progress.InProgress;
            }        
            
            _taskWriteDeleteOnlyRepository.Update(domainTask);
            
            return default;
        }

        private void CheckFieldsInProgress(DomainTask domainTask)
        {
            var task = _taskReadOnlyRepository.Get(domainTask.TaskNumeber);

            if(!task.Title.Equals(domainTask.Title))
                throw new UseCaseException.UseCaseException("Title can't be changed");

            if(!task.EstimatedDate.Equals(domainTask.EstimatedDate))
                throw new UseCaseException.UseCaseException("Estimated date can't be changed");
        }
    }
}
