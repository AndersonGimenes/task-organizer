using System;
using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.Extension;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UseCase.Task.ToDo
{
    public class ToDoCreateTaskUseCase : IToDoCreateTaskUseCase
    {
        private ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        public ToDoCreateTaskUseCase(ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
        }

        public DomainTask CreateNewTask(DomainTask domainTask)
        {
            domainTask.IsValid();
            
            domainTask.ProgressValidation(Progress.ToDo);
            
            // If EstimetedDate is null then system add date plus 30 days
            if(domainTask.EstimatedDate.Date.Equals(new DateTime().Date))
                    domainTask.EstimatedDate = DateTime.Now.Date.AddDays(30);
            
            // Fill createDate
            domainTask.CreateDate = DateTime.Now.Date;

            return _taskWriteDeleteOnlyRepository.Add(domainTask);
        }

    }
}