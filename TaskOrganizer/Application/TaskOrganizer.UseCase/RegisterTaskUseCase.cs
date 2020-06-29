using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.Enum;

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

        public DomainTask Register(DomainTask domainTask)
        {
            return DecideFlow(domainTask);
            
        }

        private DomainTask DecideFlow(DomainTask domainTask)
        {
            if(domainTask.TaskNumber.Equals((int)default))
            {
                if(domainTask.EstimatedDate.Date.Equals(new DateTime().Date))
                    domainTask.EstimatedDate = DateTime.Now.Date.AddDays(30);
            
                domainTask.CreateDate = DateTime.Now.Date;
                return _taskWriteDeleteOnlyRepository.Add(domainTask);
            }            
           
            if(domainTask.Progress.Equals(Progress.InProgress))
            {
                if(domainTask.StartDate is null)
                    domainTask.StartDate = DateTime.Now.Date;
                    
                CheckFieldsInProgress(domainTask);
            }

            if(domainTask.Progress.Equals(Progress.Done))
            {
                if(domainTask.EndDate is null)
                    domainTask.EndDate = DateTime.Now.Date;
                
                CheckFieldsDone(domainTask);
            }

            _taskWriteDeleteOnlyRepository.Update(domainTask);
            
            return default;
        }

        private void CheckFieldsDone(DomainTask domainTask)
        {
            var task = _taskReadOnlyRepository.Get(domainTask.TaskNumber);

            if(!task.Description.Equals(domainTask.Description))
                throw new UseCaseException.UseCaseException("Description can't be changed");

            if(task.EndDate != null && !task.EndDate.Equals(domainTask.EndDate))
                throw new UseCaseException.UseCaseException("End date can't be changed");

            CheckFieldsTitleAndEstimatedDate(domainTask, task);
        }

        private void CheckFieldsInProgress(DomainTask domainTask)
        {
            var task = _taskReadOnlyRepository.Get(domainTask.TaskNumber);

            CheckFieldsTitleAndEstimatedDate(domainTask, task);
        }

        private void CheckFieldsTitleAndEstimatedDate(DomainTask taskRequest, DomainTask taskBase)
        {
            if(!taskBase.Title.Equals(taskRequest.Title))
                throw new UseCaseException.UseCaseException("Title can't be changed");

            if(!taskBase.EstimatedDate.Equals(taskRequest.EstimatedDate))
                throw new UseCaseException.UseCaseException("Estimated date can't be changed");

            if(!taskBase.CreateDate.Equals(taskRequest.CreateDate))
                throw new UseCaseException.UseCaseException("Create date can't be changed");

            if(taskBase.StartDate != null && !taskBase.StartDate.Equals(taskRequest.StartDate))
                throw new UseCaseException.UseCaseException("Start date can't be changed");
        }
    }
}
