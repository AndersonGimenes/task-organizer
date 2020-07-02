using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UseCase.Task.ToDo
{
    public class ToDoDeleteTaskUseCase : IToDoDeleteTaskUseCase
    {
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;

        public ToDoDeleteTaskUseCase(ITaskReadOnlyRepository taskReadOnlyRepository, ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
            _taskReadOnlyRepository = taskReadOnlyRepository;
        }

        public void Delete(DomainTask domainTask)
        {
            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);
            
            if(domainTaskDto is null)
                throw new RegisterNotFoundException(UseCaseMessage.registerNotFound);

            _taskWriteDeleteOnlyRepository.Delete(domainTask);
        }
    }
}