using System;
using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.RepositoryTest
{
    public class TaskWriteDeleteOnlyRepositoryIntegrationTest
    {
        private IRegisterTaskUseCase _registerTaskUseCase;
        
        [Fact]
        public void MustInsertTheTask()
        {
            var taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(ReturnContext());
            var taskReadOnlyRepositoy = new TaskReadOnlyRepositoy(ReturnContext());
            _registerTaskUseCase = new RegisterTaskUseCase(taskWriteDeleteOnlyRepository);

            var mock = MockDataTest();
            _registerTaskUseCase.Register(mock);

            var returned = taskReadOnlyRepositoy.Get(1);

            var returnTest = returned.TaskNumeber.Equals(1) &&
                             returned.Title.Equals(mock.Title) &&
                             returned.Description.Equals(mock.Description) &&
                             returned.CreateDate.Equals(mock.CreateDate) &&
                             returned.EstimetedDate.Equals(mock.EstimetedDate);
                             
            Assert.True(returnTest);
        }

        #region AuxiliaryMethods
        private DomainTask MockDataTest()
        {
            var domainTask = new DomainTask
            {
                EstimetedDate = DateTime.Now.Date.AddDays(25),
                IsNew = true
            };
            domainTask.SetTitle("Title test");
            domainTask.SetDescription("Description test");

            return domainTask;
        }
        
        private TaskOrganizerContext ReturnContext()
        {
            var option = new DbContextOptionsBuilder<TaskOrganizerContext>()
                .UseInMemoryDatabase("DbTaskOrganizer")
                .Options;
            return new TaskOrganizerContext(option); 
        }

        #endregion
    }
}