using System;
using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.ContractUseCase.Task.Done;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.Extension;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UseCase.Task.Done
{
    public class DoneUseCase : IDoneUseCase
    {
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public DoneUseCase(ITaskReadOnlyRepository taskReadOnlyRepository, ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository)
        {
            _taskReadOnlyRepository = taskReadOnlyRepository;
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
        }
        public void UpdateProgressTask(DomainTask domainTask)
        {
            domainTask.IsValid();
            
            domainTask.ProgressValidation(Progress.InProgress);
            
            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);
            
            DoneValidate(domainTask, domainTaskDto);

            // Update Progress and EndDate
            domainTask.Progress = Progress.Done;
            domainTask.EndDate = DateTime.Now.Date;
            
            _taskWriteDeleteOnlyRepository.Update(domainTask);
            
        }

        #region [ Auxiliary Methods ]
        
        private void DoneValidate(DomainTask domainTask, DomainTask domainTaskDto)
        {
            string propertyName = string.Empty; 

            if(domainTask.EndDate != null)
                propertyName = nameof(domainTask.EndDate);

            if(!domainTaskDto.StartDate.Equals(domainTask.StartDate))
                propertyName = nameof(domainTask.StartDate);
                   
            if(!domainTaskDto.CreateDate.Equals(domainTask.CreateDate))
                propertyName = nameof(domainTask.CreateDate);
            
            if(!domainTaskDto.EstimatedDate.Equals(domainTask.EstimatedDate))
                propertyName = nameof(domainTask.EstimatedDate);

            if(!domainTaskDto.Title.Equals(domainTask.Title))
                propertyName = nameof(domainTask.Title);

            if(!domainTaskDto.Description.Equals(domainTask.Description))
                propertyName = nameof(domainTask.Description);
            
            if(!string.IsNullOrEmpty(propertyName))
                throw new UseCaseException.UseCaseException(string.Format(UseCaseMessage.fieldNotUpdate, propertyName));
        }

        #endregion

    }
}