using System;
using Moq;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.ToDo;
using TaskOrganizer.UseCase.UseCaseException;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class ToDoDeleteTaskUseCaseTest
    {
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _mockTaskWriteDeleteOnlyRepository;
        private readonly Mock<ITaskReadOnlyRepository> _mockTaskReadOnlyRepository;
        private readonly IToDoDeleteTaskUseCase _toDoDeleteTaskUseCase;

        public ToDoDeleteTaskUseCaseTest()
        {
            _mockTaskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _mockTaskReadOnlyRepository = new Mock<ITaskReadOnlyRepository>();
            _toDoDeleteTaskUseCase = new ToDoDeleteTaskUseCase(_mockTaskReadOnlyRepository.Object, _mockTaskWriteDeleteOnlyRepository.Object);
        }

        [Fact]
        public void WhenReceiveATaskShouldBeDelete()
        {
            var domainTask = new DomainTask
            {
                TaskNumber = 1,
                Title = "test",
                Description = "test",
                Progress = Progress.ToDo,
                CreateDate = DateTime.Now.Date,
                EstimatedDate = DateTime.Now.Date.AddDays(20)
            };

            _mockTaskReadOnlyRepository
                .Setup( x => x.Get(It.IsAny<int>()))
                .Returns(domainTask);

            _mockTaskWriteDeleteOnlyRepository
                .Setup(x => x.Delete(It.IsAny<DomainTask>()))
                .Callback(() => domainTask = null);

            _toDoDeleteTaskUseCase.Delete(domainTask);

            Assert.True(domainTask is null);        
        }

        [Fact]
        public void WhenNotExistsATaskInDataBaseShouldBeThrowARegisterNotFoundException()
        {
            var errorMessage = "Register not found.";

            _mockTaskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns((DomainTask)null);

            var ex = Assert.Throws<RegisterNotFoundException>(() => _toDoDeleteTaskUseCase.Delete(new DomainTask()));
            Assert.Equal(ex.Message, errorMessage);

        }

        [Fact]
        public void WhenTheProgressIsNotToDoTheTaskCannotBeDeleted()
        {
            var errorMessage = "Register can't delete when your progress is differente Progress.";

            var domainTaskDto = new DomainTask
            {
                TaskNumber = 1,
                Title = "test",
                Description = "test",
                Progress = Progress.InProgress,
                CreateDate = DateTime.Now.Date,
                EstimatedDate = DateTime.Now.Date.AddDays(20)
            };

           _mockTaskReadOnlyRepository
                .Setup( x => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);

            var ex = Assert.Throws<UseCaseException>(() => _toDoDeleteTaskUseCase.Delete(new DomainTask()));
            Assert.Equal(ex.Message, errorMessage);

        }

    }
}