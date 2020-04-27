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
                var id = taskWriteDeleteOnlyRepository.Add(mock).TaskNumeber;
                
                task = taskReadOnlyRepositoy.Get(id);
            } 
            
            return task;
        }
    }
}