using System;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UseCase.Task.ToDo
{
    public class ToDoUseCase : IToDoUseCase
    {
        private ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private ITaskReadOnlyRepository _taskReadOnlyRepository;
       
        public ToDoUseCase(ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository, ITaskReadOnlyRepository taskReadOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
            _taskReadOnlyRepository = taskReadOnlyRepository;
        }
        public DomainTask InsertNewTask(DomainTask domainTask)
        {
            domainTask.IsValid();
            
            ProgressToDoValidation(domainTask);
            
            // If EstimetedDate is null then system add date plus 30 days
            if(domainTask.EstimatedDate.Date.Equals(new DateTime().Date))
                    domainTask.EstimatedDate = DateTime.Now.Date.AddDays(30);
            
            // Fill createDate
            domainTask.CreateDate = DateTime.Now.Date;
                return _taskWriteDeleteOnlyRepository.Add(domainTask);
        }

        public void UpdateTask(DomainTask domainTask)
        {
            domainTask.IsValid();

            ProgressToDoValidation(domainTask);
            
            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);
            
            if(domainTaskDto is null)
                throw new RegisterNotFoundException("Register not found");

            // verify if createDate to doesn't update
            if(domainTaskDto.CreateDate != domainTask.CreateDate)
                throw new UseCaseException.UseCaseException($"The {nameof(domainTask.CreateDate)} can't be update!");

            _taskWriteDeleteOnlyRepository.Update(domainTask);
        }

        public void Delete(DomainTask domainTask)
        {
            _taskWriteDeleteOnlyRepository.Delete(domainTask);
        }

        #region [ Private Methods ]
        
        private void ProgressToDoValidation(DomainTask domainTask)
        {
            if(domainTask.Progress != Progress.ToDo)
                throw new UseCaseException.UseCaseException($"The {nameof(domainTask.Progress)} must be ToDo.");
        }

        #endregion
    }
}