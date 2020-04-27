using System;
using Moq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;
using Xunit;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class RegisterTaskUseCaseUnitTest
    {
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly Mock<ITaskReadOnlyRepository> _mockTaskReadOnlyRepository;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _mockTaskWriteDeleteOnlyRepository;
        
        public RegisterTaskUseCaseUnitTest()
        {
            _mockTaskReadOnlyRepository = new Mock<ITaskReadOnlyRepository>();
            _mockTaskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object, _mockTaskReadOnlyRepository.Object);
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
        public void WhenTaskNumberIsZeroNewTaskWillBeAddedAndTheReturnedIdWillNotBeZero()
        {
            var domainTask = new DomainTask();
            domainTask.TaskNumeber = 0;

            _mockTaskWriteDeleteOnlyRepository.Setup(x => x.Add(It.IsAny<DomainTask>())).Returns(new DomainTask{TaskNumeber = 1});
            
            domainTask.TaskNumeber = _registerTaskUseCase.Register(domainTask).TaskNumeber;  

            Assert.NotEqual(domainTask.TaskNumeber, 0);
        }

        [Fact]
        public void WhenTaskNumberIsZeroNewTaskWillBeAddedAndTheCreatedDateWillBeEqualDateTimeNow()
        {
            var domainTask = new DomainTask();
            domainTask.TaskNumeber = 0;

            _registerTaskUseCase.Register(domainTask);  

            Assert.Equal(domainTask.CreateDate, DateTime.Now.Date);
        }

        [Fact(Skip="Rule not implemented yet")]
        public void WhenStartDateIsSetThenThePropertyProgressMustBeUpdatedInProgressTask()
        {
            var domainTaskBase = new DomainTask();
            domainTaskBase.TaskNumeber = 1;
            domainTaskBase.SetTitle("Test title");
            domainTaskBase.EstimatedDate = DateTime.Now.Date.AddDays(30);

            var domainTask = new DomainTask();
            domainTask.TaskNumeber = 1;
            domainTask.StartDate = DateTime.Now.Date;
            domainTask.SetTitle("Test title");
            domainTask.EstimatedDate = DateTime.Now.Date.AddDays(30);

            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskBase);

            _registerTaskUseCase.Register(domainTask);

            Assert.Equal(domainTask.Progress,Progress.InProgress);
        }

        [Theory(Skip="Rule not implemented yet")]
        [InlineData("Title can't be changed", "Requisition title")]
        [InlineData("Estimated date can't be changed", "Base Title")]
        public void UseCaseExceptionMustBeReturnedWhenTheTitleAndEstimetedDateTaskBaseNotEqualTheTitleAndEstimetedDateTaskRequisition(string result, string title)
        {
            var domainTaskBase = new DomainTask(){TaskNumeber = 1};
            domainTaskBase.SetTitle("Base Title");
            
            var domainTask = new DomainTask();
            domainTask.TaskNumeber = 1;
            domainTask.SetTitle(title);
            domainTask.StartDate = DateTime.Now.Date;
            domainTask.EstimatedDate = DateTime.Now.Date.AddDays(30);
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskBase);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTask));

            Assert.Equal(ex.Message, result);
        }
       
    }
}