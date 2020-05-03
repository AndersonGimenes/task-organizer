using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class InsertTaskToTest
    {
        public static DomainTask InsertAndReturTask(string progress)
        {
            DomainTask task;

            using(var context = DataBaseInMemory.ReturnContext())
            {
                var taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(context);
                var mock = MockDataTask.MockDataTest(progress);            
                task = taskWriteDeleteOnlyRepository.Add(mock);
            }
            return task;
        }
    }
}