using System;
using Moq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class RegisterTaskUseCaseUnitTest
    {
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _mockTaskWriteDeleteOnlyRepository;
        
        public RegisterTaskUseCaseUnitTest()
        {
            _mockTaskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object);
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
        public void IfThereIsEstimetedDateFilledThenKeepTheDateFilled()
        {
            var domainTask = new DomainTask{EstimetedDate = DateTime.Now.Date.AddDays(10)};
            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimetedDate.Equals(DateTime.Now.Date.AddDays(10)));
        }

        [Fact]
        public void WhenCreatingNewRegisterTheSystemWillPutAutomaticallyTheProgressWithToDo()
        {
            var domainTask = new DomainTask
            {
                IsNew = true
            };
            
            _registerTaskUseCase.Register(domainTask);  

            Assert.True(domainTask.Progress.Equals(Progress.ToDo));
        }
    }
}