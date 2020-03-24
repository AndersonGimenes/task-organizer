using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest
{
    public class GetTaskGetAllsUseCaseIntegrationTest
    {
        private readonly TaskOrganizerContext _context;
        private readonly TaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly TaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly IGetTasksUseCase _getTasksUseCase;

        public GetTaskGetAllsUseCaseIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);
            _getTasksUseCase = new GetTasksUseCase(_taskReadOnlyRepository);
        }

        [Fact]
        public void MustReturnAllTasks()
        {
            for(var x = 0; x < 4; x++)
            {                       
                _taskWriteDeleteOnlyRepository.Add(MockDataTask.MockDataTest());
            }  

            var returnTask = _getTasksUseCase.GetAll();

            Assert.True(returnTask.Count > 0);
          
        }

    }
}