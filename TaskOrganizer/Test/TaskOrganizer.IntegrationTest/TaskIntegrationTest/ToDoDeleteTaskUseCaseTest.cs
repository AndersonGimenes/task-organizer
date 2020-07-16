using System;
using System.Linq;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.ToDo;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class ToDoDeleteTaskUseCaseTest
    {
        private readonly IToDoDeleteTaskUseCase _toDoDeleteTaskUseCase;
        private readonly TaskOrganizerContext _context;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;

        public ToDoDeleteTaskUseCaseTest()
        {
            InsertMockDataBaseInMemory.InsertMock();

            _context = DataBaseInMemory.ReturnContext();
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            
            _toDoDeleteTaskUseCase = new ToDoDeleteTaskUseCase(_taskReadOnlyRepository, _taskWriteDeleteOnlyRepository);
        }

        [Fact]
        public void WhenReceiveAValidTaskShouldBeDeleted()
        {
            var result = "Sequence contains no elements";

            var domainTask = new DomainTask
            {
                TaskNumber = 40,
                EstimatedDate = DateTime.Now.Date.AddDays(20),
                CreateDate = DateTime.Now.Date,
                Title = "Test title three", 
                Description = "Test description three", 
                Progress = Progress.ToDo,
            };

            _toDoDeleteTaskUseCase.Delete(domainTask);

            var ex = Assert.Throws<InvalidOperationException>( () =>  _taskReadOnlyRepository.Get(domainTask.TaskNumber));
            Assert.Equal(ex.Message, result);

        }
    }
}