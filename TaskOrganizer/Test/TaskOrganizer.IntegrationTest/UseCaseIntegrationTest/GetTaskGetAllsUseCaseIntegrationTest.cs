using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest
{
    public class GetTaskGetAllsUseCaseIntegrationTest
    {
        private IGetTasksUseCase _getTasksUseCase;

        [Fact]
        public void MustReturnAllTasks()
        {

            var context = DataBaseInMemory.ReturnContext();
            var taskReadOnlyRepository = new TaskReadOnlyRepository(context);
            var taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(context);
            _getTasksUseCase = new GetTasksUseCase(taskReadOnlyRepository);

            for(var x = 0; x < 4; x++)
            {                       
                taskWriteDeleteOnlyRepository.Add(MockDataTask.MockDataTest());
            }  

            var returned = _getTasksUseCase.GetAll();

            Assert.True(returned.Count > 0);
          
        }

    }
}