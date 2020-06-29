using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.Repository;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class InsertTaskToTest
    {
        public static DomainTask InsertAndReturTask(Progress progress)
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