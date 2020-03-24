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

            var id = _getTasksUseCase.GetAll().Last().TaskNumeber;

            var returnTask = _getTasksUseCase.Get(id);

             var returnTest = returnTask.TaskNumeber.Equals(id) &&
                              returnTask.Title.Equals(mock.Title) &&
                              returnTask.Description.Equals(mock.Description) &&
                              returnTask.CreateDate.Equals(mock.CreateDate) &&
                              returnTask.EstimetedDate.Equals(mock.EstimetedDate);
                             
            Assert.True(returnTest);

        }

    }
}