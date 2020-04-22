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
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object, null);
        }

        [Fact]
        public void IfThereIsNotEstimatedDateFilledAssumeTheCurrentDatePlusThirty()
        {
            var domainTask = new DomainTask();

            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimatedDate.Equals(DateTime.Now.Date.AddDays(30)));
        }
        
        [Fact]
        public void IfThereIsEstimatedDateFilledThenKeepTheDateFilled()
        {
            var domainTask = new DomainTask{EstimatedDate = DateTime.Now.Date.AddDays(10)};
            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimatedDate.Equals(DateTime.Now.Date.AddDays(10)));
        }

        [Fact]
        public void WhenCreatingNewRegisterTheSystemWillPutAutomaticallyTheProgressWithToDo()
        {
            var domainTask = new DomainTask();
            
            _registerTaskUseCase.Register(domainTask);  

            Assert.True(domainTask.Progress.Equals(Progress.ToDo));
        }
    }
}