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
    public class ToDoUpdateTaskUseCaseTest
    {
        private readonly Mock<ITaskReadOnlyRepository> _mockTaskReadOnlyRepository;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _mockTaskWriteDeleteOnlyRepository;
        private readonly IToDoUpdateTaskUseCase _toDoUpdateTaskUseCase;

        public ToDoUpdateTaskUseCaseTest()
        {
            _mockTaskReadOnlyRepository = new Mock<ITaskReadOnlyRepository>();
            _mockTaskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _toDoUpdateTaskUseCase = new ToDoUpdateTaskUseCase(_mockTaskReadOnlyRepository.Object, _mockTaskWriteDeleteOnlyRepository.Object);
        }

        [Fact]
        public void WhenReceiveAValidTaskShoulBeUpdateWithSucces()
        {
            var domainTask = MockUpdateDomainTask();

            var domainTaskDto = MockDomainTaskDto();

            _mockTaskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);

            _mockTaskWriteDeleteOnlyRepository
                .Setup(x => x.Update(It.IsAny<DomainTask>()))
                .Callback(() => UpdateDomainTask(domainTask, domainTaskDto));

            _toDoUpdateTaskUseCase.UpdateTask(domainTask);

            Assert.Equal(domainTask.Title, domainTaskDto.Title);
            Assert.Equal(domainTask.Description, domainTaskDto.Description);

        }

        [Fact]
        public void AUseCaseExceptionShouldBeThrownIfProgressIsNotToDo()
        {
            var domainTask = MockUpdateDomainTask();
            domainTask.Progress = Progress.InProgress;

            var ex = Assert.Throws<UseCaseException>(() => _toDoUpdateTaskUseCase.UpdateTask(domainTask));
            Assert.Equal("The Progress must be ToDo.", ex.Message);
        }
        
        [Fact]
        public void WhenNotExistsATaskInDataBaseShouldBeThrowARegisterNotFoundException()
        {
            var domainTask = MockUpdateDomainTask();

            _mockTaskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Throws(new InvalidOperationException("Sequence contains no elements."));

            var ex = Assert.Throws<InvalidOperationException>(() => _toDoUpdateTaskUseCase.UpdateTask(domainTask));
            Assert.Equal("Sequence contains no elements.", ex.Message);

        }

        [Fact]
        public void WhenTheCreateDateIsNotEqualTheCreateDatePersistedShouldBeThrowAUseCaseException()
        {
            var domainTask = MockUpdateDomainTask();
            domainTask.CreateDate = DateTime.Now.Date.AddDays(10);

            var domainTaskDto = MockDomainTaskDto();

            _mockTaskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);

            var ex = Assert.Throws<UseCaseException>(() => _toDoUpdateTaskUseCase.UpdateTask(domainTask));
            Assert.Equal("The CreateDate can't be update!", ex.Message);
        }

        #region [ Auxiliary Methods ]

        private void UpdateDomainTask(DomainTask domainTask, DomainTask domainTaskDto)
        {
            domainTaskDto.Title = domainTask.Title;
            domainTaskDto.Description = domainTask.Description;
        }  

        private DomainTask MockUpdateDomainTask()
        {
            return new DomainTask
            {
                TaskNumber = 1,
                Title = "Test  update",
                Description = "Test update",
                Progress = Progress.ToDo,
                EstimatedDate = DateTime.Now.Date.AddDays(30),
                CreateDate = DateTime.Now.Date
            };
        }

        private DomainTask MockDomainTaskDto()
        {
            return new DomainTask
            {
                TaskNumber = 1,
                Title = "Test",
                Description = "Test",
                Progress = Progress.ToDo,
                EstimatedDate = DateTime.Now.Date.AddDays(30),
                CreateDate = DateTime.Now.Date 
            };
        }      

        #endregion
    }
}