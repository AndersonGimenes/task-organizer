using System;
using Moq;
using TaskOrganizer.Domain.ContractUseCase.Task.Done;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.Done;
using TaskOrganizer.UseCase.UseCaseException;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class DoneUseCaseTest
    {
        private readonly IDoneUseCase _doneUseCase;
        private readonly Mock<ITaskReadOnlyRepository> _taskReadOnlyRepositoryMock;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _taskWriteDeleteOnlyRepositoryMock;

        public DoneUseCaseTest()
        {
            _taskReadOnlyRepositoryMock = new Mock<ITaskReadOnlyRepository>();
            _taskWriteDeleteOnlyRepositoryMock = new Mock<ITaskWriteDeleteOnlyRepository>(); 
            _doneUseCase = new DoneUseCase(_taskReadOnlyRepositoryMock.Object, _taskWriteDeleteOnlyRepositoryMock.Object);
        }

        [Fact]
        public void IfProgressIsDifferentThanInProgressThenShouldBeThrowAUseCaseException()
        {
            var domaintask = ReturnNewDomainTask(1, Progress.ToDo);

            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domaintask));

            Assert.Equal("The Progress must be InProgress.", ex.Message);
        }
        
        [Fact]
        public void IfEndDateIsDiffrentThanNullThenShouldBeThrowAUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            domainTask.EndDate = DateTime.Now.Date;

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(ReturnDomainTaskMock(1));

            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            
            Assert.Equal("The EndDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfNotExistsTheTaskInDataBaseThenShouldBeThrowARegisterNotFoundException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns((DomainTask)null);

            var ex = Assert.Throws<RegisterNotFoundException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            
            Assert.Equal("Register not found.", ex.Message);
        }

        [Fact]
        public void IfStartDateIsDifferentThanStartDateDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            domainTask.StartDate = DateTime.Now.Date.AddDays(-30);
            var domainTaskDto = ReturnDomainTaskMock(1);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            Assert.Equal("The StartDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfCreateDateIsDifferentThanCreateDateDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            domainTask.CreateDate = DateTime.Now.Date.AddDays(-40);
            var domainTaskDto = ReturnDomainTaskMock(1);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            Assert.Equal("The CreateDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfEstimatedDateIsDifferentThanEstimatedDateDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            domainTask.EstimatedDate = DateTime.Now.Date.AddDays(40);
            var domainTaskDto = ReturnDomainTaskMock(1);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            Assert.Equal("The EstimatedDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfTitleIsDifferentThanTitleDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            domainTask.Title = "Title update";
            var domainTaskDto = ReturnDomainTaskMock(1);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            Assert.Equal("The Title can't be update!", ex.Message);
        }

        [Fact]
        public void IfDescriptionIsDifferentThanDescriptionDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            domainTask.Description = "Description update";
            var domainTaskDto = ReturnDomainTaskMock(1);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _doneUseCase.UpdateChangeTask(domainTask));
            Assert.Equal("The Description can't be update!", ex.Message);
        }

        [Fact]
        public void WhenDomainTaskIsValidAndExistsInDataBaseShouldBeUpdateTheProgressToInProgressAndStartDateToDateTimeNow()
        {
            var domainTask = ReturnNewDomainTask(1, Progress.InProgress);
            
            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(ReturnDomainTaskMock(1));

            _doneUseCase.UpdateChangeTask(domainTask);

            Assert.Equal(DateTime.Now.Date, domainTask.EndDate);
            Assert.Equal(Progress.Done, domainTask.Progress);
        }

        #region [ Auxiliary Methods ]

        private DomainTask ReturnDomainTaskMock(int taskNumber)
        {
           return new DomainTask
           {
               TaskNumber = taskNumber,
               Title = "Test title",
               Description = "Test description", 
               Progress = Progress.InProgress,
               CreateDate = DateTime.Now.Date.AddDays(-10),
               EstimatedDate = DateTime.Now.Date.AddDays(20),
               StartDate = DateTime.Now.Date.AddDays(-5),
               EndDate = null
           };
        }

        private DomainTask ReturnNewDomainTask(int taskNumber, Progress progress)
        {
            return new DomainTask
            {
               TaskNumber = taskNumber,
               Title = "Test title",
               Description = "Test description", 
               Progress = progress,
               CreateDate = DateTime.Now.Date.AddDays(-10),
               EstimatedDate = DateTime.Now.Date.AddDays(20),
               StartDate = DateTime.Now.Date.AddDays(-5)
            };
        }

        #endregion
    }
}