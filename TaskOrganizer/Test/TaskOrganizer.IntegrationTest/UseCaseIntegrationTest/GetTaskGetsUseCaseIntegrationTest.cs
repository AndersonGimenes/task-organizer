using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest
{
    public class GetTaskGetsUseCaseIntegrationTest
    {
        private GetTasksUseCase _getTasksUseCase;

        [Fact]
        public void MustReturnJustOnlyTask()
        {

            var context = DataBaseInMemory.ReturnContext();
            var taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(context);
            var taskReadOnlyRepository = new TaskReadOnlyRepository(context);
            _getTasksUseCase = new GetTasksUseCase(taskReadOnlyRepository);

            var mock = MockDataTask.MockDataTest();
            taskWriteDeleteOnlyRepository.Add(mock);
                

            var returned = _getTasksUseCase.Get(1);

             var returnTest = returned.TaskNumeber.Equals(1) &&
                              returned.Title.Equals(mock.Title) &&
                              returned.Description.Equals(mock.Description) &&
                              returned.CreateDate.Equals(mock.CreateDate) &&
                              returned.EstimetedDate.Equals(mock.EstimetedDate);
                             
            Assert.True(returnTest);

        }

    }
}