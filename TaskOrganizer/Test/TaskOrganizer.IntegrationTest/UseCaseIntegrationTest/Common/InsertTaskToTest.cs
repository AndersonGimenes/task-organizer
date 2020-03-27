using System.Linq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class InsertTaskToTest
    {
        public static DomainTask InsertAndReturTask()
        {
            DomainTask task = null;

            using(var context = DataBaseInMemory.ReturnContext())
            {
                var taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(context);
                var taskReadOnlyRepositoy = new TaskReadOnlyRepository(context);

                var mock = MockDataTask.MockDataTest();
                taskWriteDeleteOnlyRepository.Add(mock);
                Helper.IncrementId();

                task = taskReadOnlyRepositoy.Get(Helper.IdBase);
            } 
            
            return task;
        }
    }
}