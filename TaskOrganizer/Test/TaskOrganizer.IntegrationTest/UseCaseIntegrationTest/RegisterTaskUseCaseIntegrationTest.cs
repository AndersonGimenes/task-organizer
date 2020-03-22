using System;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.RepositoryTest
{
    public class RegisterTaskUseCaseIntegrationTest
    {
        private IRegisterTaskUseCase _registerTaskUseCase;
        
        [Fact]
        public void MustInsertTheTask()
        {
            var context = DataBaseInMemory.ReturnContext();
            var taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(context);
            var taskReadOnlyRepositoy = new TaskReadOnlyRepository(context);
            _registerTaskUseCase = new RegisterTaskUseCase(taskWriteDeleteOnlyRepository);

            var mock = MockDataTask.MockDataTest();
            _registerTaskUseCase.Register(mock);
            
            var returned = taskReadOnlyRepositoy.Get(1);

            var returnTest = returned.TaskNumeber.Equals(1) &&
                             returned.Title.Equals(mock.Title) &&
                             returned.Description.Equals(mock.Description) &&
                             returned.CreateDate.Equals(mock.CreateDate) &&
                             returned.EstimetedDate.Equals(mock.EstimetedDate);
                             
            Assert.True(returnTest);
        }
    }
}