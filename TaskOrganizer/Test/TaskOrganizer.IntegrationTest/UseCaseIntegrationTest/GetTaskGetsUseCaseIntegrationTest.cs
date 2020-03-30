using System.Linq;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest
{
    public class GetTaskGetsUseCaseIntegrationTest
    {
        private readonly GetTasksUseCase _getTasksUseCase;
        private readonly TaskOrganizerContext _context;
        private readonly TaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly TaskReadOnlyRepository _taskReadOnlyRepository;

        public GetTaskGetsUseCaseIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _getTasksUseCase = new GetTasksUseCase(_taskReadOnlyRepository);
        }

        [Fact]
        public void MustReturnJustOnlyTask()
        {
            var mock = MockDataTask.MockDataTest();
            _taskWriteDeleteOnlyRepository.Add(mock);
            Helper.IncrementId();

            var returnTask = _getTasksUseCase.Get(Helper.IdBase);

            Assert.Equal(returnTask.Title, mock.Title);
            Assert.Equal(returnTask.Description, mock.Description);
            Assert.Equal(returnTask.CreateDate, mock.CreateDate);
            Assert.Equal(returnTask.EstimatedDate, mock.EstimatedDate);

        }

    }
}