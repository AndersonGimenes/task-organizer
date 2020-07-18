using System;
using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.ContractUseCase.Task.InProgress;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.Extension;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UseCase.Task.InProgress
{
    public class InProgressUseCase : IInProgressUseCase
    {
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public InProgressUseCase(ITaskReadOnlyRepository taskReadOnlyRepository, ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository)
        {
            _taskReadOnlyRepository = taskReadOnlyRepository;
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
        }

        public void UpdateProgressTask(DomainTask domainTask)
        {    
            domainTask.IsValid();

            domainTask.ProgressValidation(Progress.ToDo);

            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);
            
            InProgressValidate(domainTask, domainTaskDto);

            // Change the task to InProgress and Update StartDate
            domainTask.StartDate = DateTime.Now.Date;
            domainTask.Progress = Progress.InProgress;    

            _taskWriteDeleteOnlyRepository.Update(domainTask);
        }

        public void UpdateTask(DomainTask domainTask)
        {
            domainTask.IsValid();

            domainTask.ProgressValidation(Progress.InProgress);

            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);
            
            InProgressValidate(domainTaskDto, domainTask);
	        
            _taskWriteDeleteOnlyRepository.Update(domainTask);
        }

        #region [ Auxiliary Methods ]
        private void InProgressValidate(DomainTask domainTask, DomainTask domainTaskDto)
        {
            string propertyName = string.Empty; 

            if(domainTaskDto is null)
                throw new RegisterNotFoundException(UseCaseMessage.registerNotFound);

            if(domainTask.StartDate != null && domainTaskDto.StartDate != domainTask.StartDate)
                propertyName = nameof(domainTask.StartDate);
                   
            if(!domainTaskDto.CreateDate.Equals(domainTask.CreateDate))
                propertyName = nameof(domainTask.CreateDate);
            
            if(!domainTaskDto.EstimatedDate.Equals(domainTask.EstimatedDate))
                propertyName = nameof(domainTask.EstimatedDate);

            if(!domainTaskDto.Title.Equals(domainTask.Title))
                propertyName = nameof(domainTask.Title);
            
            if(!string.IsNullOrEmpty(propertyName))
                throw new UseCaseException.UseCaseException(string.Format(UseCaseMessage.fieldNotUpdate, propertyName));
        }

        #endregion

    }
}