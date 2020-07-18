using System;
using System.Linq;
using AutoMapper;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.ToDo;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class ToDoCreateTaskUseCaseTest
    {
        private readonly IToDoCreateTaskUseCase _toDoCreateTaskUseCase;
        private readonly IMapper _mapper;
        private readonly TaskOrganizerContext _context;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;

        public ToDoCreateTaskUseCaseTest()
        {
            _mapper = CreateMapper.CreateMapperProfile();

            _context = DataBaseInMemory.ReturnContext();
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context, _mapper);            

            _toDoCreateTaskUseCase = new ToDoCreateTaskUseCase(_taskWriteDeleteOnlyRepository);
        }

        [Fact]
        public void WhenReceiveAValidDomainTaskMustCreateANewTask()
        {
            var beforePersistTask = _context.RepositoryTasks.ToList().Count;  
            
            var domainTask = new DomainTask
            {
                Title = "Test title",
                Description = "Test description",
                Progress = Progress.ToDo, 
            };

            var taskReturned = _toDoCreateTaskUseCase.CreateNewTask(domainTask);

            var afterPersistTask = _context.RepositoryTasks.ToList().Count;

            Assert.True(afterPersistTask > beforePersistTask);     
            Assert.Equal(taskReturned.Title, domainTask.Title);
            Assert.Equal(taskReturned.Description, domainTask.Description);
            Assert.Equal(taskReturned.Progress, domainTask.Progress);
            Assert.Equal(taskReturned.CreateDate, DateTime.Now.Date);
            Assert.Equal(taskReturned.EstimatedDate, DateTime.Now.Date.AddDays(30));      

        }
    }
}