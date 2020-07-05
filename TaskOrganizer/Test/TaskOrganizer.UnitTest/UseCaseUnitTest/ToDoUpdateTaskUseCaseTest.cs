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
            var errorMessage = "The Progress must be ToDo.";

            var domainTask = MockUpdateDomainTask();

            var ex = Assert.Throws<UseCaseException>(() => _toDoUpdateTaskUseCase.UpdateTask(domainTask));
            Assert.Equal(ex.Message, errorMessage);
        }
        
        [Fact]
        public void WhenNotExistsATaskInDataBaseShouldBeThrowARegisterNotFoundException()
        {
            var errorMessage = "Register not found.";

            var domainTask = MockUpdateDomainTask();

            _mockTaskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns((DomainTask)null);

            var ex = Assert.Throws<RegisterNotFoundException>(() => _toDoUpdateTaskUseCase.UpdateTask(domainTask));
            Assert.Equal(ex.Message, errorMessage);

        }

        [Fact]
        public void WhenTheCreateDateIsNotEqualTheCreateDatePersistedShouldBeThrowAUseCaseException()
        {
            var errorMessage = "The CreateDate can't be update!";

            var domainTask = MockUpdateDomainTask();
            domainTask.CreateDate = DateTime.Now.Date.AddDays(10);

            var domainTaskDto = MockDomainTaskDto();

            _mockTaskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(domainTaskDto);

            var ex = Assert.Throws<UseCaseException>(() => _toDoUpdateTaskUseCase.UpdateTask(domainTask));
            Assert.Equal(ex.Message, errorMessage);
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