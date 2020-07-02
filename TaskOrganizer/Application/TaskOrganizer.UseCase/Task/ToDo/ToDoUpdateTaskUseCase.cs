using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.Extension;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UseCase.Task.ToDo
{
    public class ToDoUpdateTaskUseCase : IToDoUpdateTaskUseCase
    {
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public ToDoUpdateTaskUseCase(ITaskReadOnlyRepository taskReadOnlyRepository, ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository)
        {
            _taskReadOnlyRepository = taskReadOnlyRepository;
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
        }   
    

        public void UpdateTask(DomainTask domainTask)
        {
            domainTask.IsValid();

            domainTask.ProgressValidation(Progress.ToDo);
            
            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);
            
            if(domainTaskDto is null)
                throw new RegisterNotFoundException(UseCaseMessage.registerNotFound);

            // verify if createDate to doesn't update
            if(domainTaskDto.CreateDate != domainTask.CreateDate)
                throw new UseCaseException.UseCaseException(string.Format(UseCaseMessage.fieldNotUpdate, nameof(domainTask.CreateDate)));

            _taskWriteDeleteOnlyRepository.Update(domainTask);
        }
    }
}