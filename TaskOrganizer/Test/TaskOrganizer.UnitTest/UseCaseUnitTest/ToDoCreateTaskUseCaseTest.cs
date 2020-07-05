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
    public class ToDoCreateTaskUseCaseTest
    {
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _taskWriteDeleteOnlyRepository;
        private readonly IToDoCreateTaskUseCase _toDoCreateTaskUseCase;
        
        public ToDoCreateTaskUseCaseTest()
        {
            _taskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _toDoCreateTaskUseCase = new ToDoCreateTaskUseCase(_taskWriteDeleteOnlyRepository.Object); 
        }

        [Fact]
        public void AUseCaseExceptionShouldBeThrownIfProgressIsNotToDo()
        {
            var errorMessage = "The Progress must be ToDo.";

            var domainTask = MockNewDomainTask();
            domainTask.Progress = Progress.InProgress;

            var ex = Assert.Throws<UseCaseException>(() => _toDoCreateTaskUseCase.CreateNewTask(domainTask));
            Assert.Equal(ex.Message, errorMessage);
        }

        [Fact]
        public void IfThereIsNotEstimatedDateFilledAssumeTheCurrentDatePlusThirty()
        {
            var domainTask = MockNewDomainTask();

            _toDoCreateTaskUseCase.CreateNewTask(domainTask);

            Assert.Equal(domainTask.EstimatedDate, DateTime.Now.Date.AddDays(30));
        }

        [Fact]
        public void IfThereIsEstimatedDateFilledThenKeepTheDateFilled()
        {
            var dateTest = DateTime.Now.Date.AddDays(10);

            var domainTask = MockNewDomainTask();
            domainTask.EstimatedDate = dateTest;
            
            _toDoCreateTaskUseCase.CreateNewTask(domainTask);

            Assert.Equal(domainTask.EstimatedDate, dateTest);
        }

        [Fact]
        public void WhenANewTaskWillBeAddedMustBeInsertACreateDateAndReturnedTaskNumberDifferentByZero()
        {
            int result = default;

            var domainTask = MockNewDomainTask();

            _taskWriteDeleteOnlyRepository
                .Setup(x => x.Add(It.IsAny<DomainTask>()))
                .Returns(MockTestReturn(domainTask));

                       
            _toDoCreateTaskUseCase.CreateNewTask(domainTask);  

            Assert.NotEqual(domainTask.TaskNumber, result);
            Assert.Equal(domainTask.CreateDate, DateTime.Now.Date);
        }

        #region [ Auxiliary Method ]

        private DomainTask MockTestReturn(DomainTask domainTask)
        {
            domainTask.TaskNumber = 1;
            return domainTask;
        }

        private DomainTask MockNewDomainTask()
        {
            return new DomainTask
            {
                Title = "Test",
                Description = "Test",
                Progress = Progress.ToDo, 
            };
        }

        #endregion
    }
}