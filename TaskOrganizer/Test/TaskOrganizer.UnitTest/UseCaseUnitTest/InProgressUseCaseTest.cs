using System;
using Moq;
using TaskOrganizer.Domain.ContractUseCase.Task.InProgress;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.InProgress;
using TaskOrganizer.UseCase.UseCaseException;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class InProgressUseCaseTest
    {
        private readonly Mock<ITaskReadOnlyRepository> _taskReadOnlyRepositoryMock;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _taskWriteDeleteOnlyRepositoryMock;
        private readonly IInProgressUseCase _inProgressUseCase;

        public InProgressUseCaseTest()
        {
            _taskReadOnlyRepositoryMock = new Mock<ITaskReadOnlyRepository>();
            _taskWriteDeleteOnlyRepositoryMock = new Mock<ITaskWriteDeleteOnlyRepository>(); 
            _inProgressUseCase = new InProgressUseCase(_taskReadOnlyRepositoryMock.Object, _taskWriteDeleteOnlyRepositoryMock.Object);
        }

        #region [ Validate UpdateChangeTask ]

        [Fact]
        public void IfStartDateIsDiffrentThanNullThenShouldBeThrowAUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(2, DateTime.Now.Date, Progress.ToDo);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(ReturnDomainTaskMock(2, null));

            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateChangeTask(domainTask));
            
            Assert.Equal("The StartDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfNotExistsTheTaskInDataBaseThenShouldBeThrowARegisterNotFoundException()
        {
            var domainTask = ReturnNewDomainTask(2, null, Progress.ToDo);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns((DomainTask)null);

            var ex = Assert.Throws<RegisterNotFoundException>(() => _inProgressUseCase.UpdateChangeTask(domainTask));
            
            Assert.Equal("Register not found.", ex.Message);
        }

        [Fact]
        public void WhenDomainTaskIsValidAndExistsInDataBaseShouldBeUpdateTheProgressToInProgressAndStartDateToDateTimeNow()
        {
            var domainTask = ReturnNewDomainTask(1, null, Progress.ToDo);
            
            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(ReturnDomainTaskMock(1, null));

            _inProgressUseCase.UpdateChangeTask(domainTask);

            Assert.Equal(DateTime.Now.Date, domainTask.StartDate);
            Assert.Equal(Progress.InProgress, domainTask.Progress);
        }

        [Fact]
        public void IfProgressIsDifferentThanToDoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, null, Progress.InProgress);
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateChangeTask(domainTask));
            Assert.Equal("The Progress must be ToDo.", ex.Message);
        }

        #endregion

        #region [ Validate UpdateTask ]

        [Fact]
        public void IfStartDateIsDifferentThanStartDateDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, DateTime.Now.Date, Progress.InProgress);
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date.AddDays(-10));

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateTask(domainTask));
            Assert.Equal("The StartDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfCreateDateIsDifferentThanCreateDateDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, DateTime.Now.Date, Progress.InProgress);
            domainTask.CreateDate = DateTime.Now.Date.AddDays(-5);
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateTask(domainTask));
            Assert.Equal("The CreateDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfEstimatedDateIsDifferentThanEstimatedDateDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, DateTime.Now.Date, Progress.InProgress);
            domainTask.EstimatedDate = DateTime.Now.Date.AddDays(40);
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateTask(domainTask));
            Assert.Equal("The EstimatedDate can't be update!", ex.Message);
        }

        [Fact]
        public void IfTitleIsDifferentThanTitleDtoThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, DateTime.Now.Date, Progress.InProgress);
            domainTask.Title = "Title update";
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateTask(domainTask));
            Assert.Equal("The Title can't be update!", ex.Message);
        }

        [Fact]
        public void IfProgressIsDifferentThanInProgressThenShouldBeThrowANewUseCaseException()
        {
            var domainTask = ReturnNewDomainTask(1, DateTime.Now.Date, Progress.ToDo);
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date);

            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);
            
            var ex = Assert.Throws<UseCaseException>(() => _inProgressUseCase.UpdateTask(domainTask));
            Assert.Equal("The Progress must be InProgress.", ex.Message);
        }

        [Fact]
        public void IfDomainTaskIsValidThenDescriptionShouldBeUpdated()
        {
            var domainTask = ReturnNewDomainTask(1, DateTime.Now.Date, Progress.InProgress);
            domainTask.Description = "Description update";
            var domainTaskDto = ReturnDomainTaskMock(1 , DateTime.Now.Date);
    
            _taskReadOnlyRepositoryMock
                .Setup(x  => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);

            _taskWriteDeleteOnlyRepositoryMock
                .Setup(x => x.Update(It.IsAny<DomainTask>()))
                .Callback(() => domainTaskDto.Description = domainTask.Description);
    
            _inProgressUseCase.UpdateTask(domainTask);

            Assert.Equal(domainTaskDto.Description, domainTask.Description);
        }

        #endregion

        #region [ Auxiliary Methods ]

        private DomainTask ReturnDomainTaskMock(int taskNumber, DateTime? date)
        {
           return new DomainTask
           {
               TaskNumber = taskNumber,
               Title = "Test title",
               Description = "Test description", 
               Progress = Progress.ToDo,
               CreateDate = DateTime.Now.Date.AddDays(-10),
               EstimatedDate = DateTime.Now.Date.AddDays(20),
               StartDate = date,
               EndDate = null
           };
        }

        private DomainTask ReturnNewDomainTask(int taskNumber, DateTime? date, Progress progress)
        {
            return new DomainTask
            {
               TaskNumber = taskNumber,
               Title = "Test title",
               Description = "Test description", 
               Progress = progress,
               CreateDate = DateTime.Now.Date.AddDays(-10),
               EstimatedDate = DateTime.Now.Date.AddDays(20),
               StartDate = date
            };
        }

        #endregion
    }
}