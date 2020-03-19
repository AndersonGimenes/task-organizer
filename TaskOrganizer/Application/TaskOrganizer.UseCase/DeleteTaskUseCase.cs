using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.UseCase
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public DeleteTaskUseCase(ITaskWriteDeleteOnlyRepository taskWriteDeleteOnlyRepository)
        {
            _taskWriteDeleteOnlyRepository = taskWriteDeleteOnlyRepository;
        }

        public void Delete(DomainTask domainTask)
        {
            _taskWriteDeleteOnlyRepository.Delete(domainTask);
        }
    }
}