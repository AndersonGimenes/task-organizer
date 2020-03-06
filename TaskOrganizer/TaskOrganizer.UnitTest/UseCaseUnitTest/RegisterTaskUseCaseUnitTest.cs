using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractUseCase;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class RegisterTaskUseCaseUnitTest
    {
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        public RegisterTaskUseCaseUnitTest()
        {
            _registerTaskUseCase = new RegisterTaskUseCase();
        }

        [Fact]
        public void ThereMustBeACreateDateEqualToTheCurrentDate()
        {
            var domainTask = new DomainTask
            {
                IsNew = true
            };

            _registerTaskUseCase.Register(domainTask);  

            Assert.True(domainTask.CreateDate.Equals(DateTime.Now.Date));
        }
        
        [Fact]
        public void IfThereIsNotEstimetedDateFilledAssumeTheCurrentDatePlusThirty()
        {
            var domainTask = new DomainTask();

            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimetedDate.Equals(DateTime.Now.Date.AddDays(30)));
        }
        
        [Fact]
        public void WhenCreatingNewRegisterTheSystemWillPutAutomaticallyTheProgressWithToDo()
        {
            var domainTask = new DomainTask
            {
                IsNew = true,
            };
            
            _registerTaskUseCase.Register(domainTask);  

            Assert.True(domainTask.Progress.Equals(Progress.ToDo));
        }
        // if IsNew(new task) is equal to true,call method to add a new register 
        // if IsNew(New task) is equal to false, call method to update a existing register

    }
}