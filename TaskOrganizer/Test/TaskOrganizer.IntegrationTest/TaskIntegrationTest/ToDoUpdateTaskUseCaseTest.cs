using System;
using System.Linq;
using AutoMapper;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.Task.ToDo;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class ToDoUpdateTaskUseCaseTest
    {
        private readonly TaskOrganizerContext _context;
        private readonly TaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly TaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly IToDoUpdateTaskUseCase _toDoUpdateTaskUseCase;
        private readonly IMapper _mapper;

        public ToDoUpdateTaskUseCaseTest()
        {

            _mapper = CreateMapper.CreateMapperProfile();

            InsertMockDataBaseInMemory.InsertMock();
            
            _context = DataBaseInMemory.ReturnContext();

            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context, _mapper);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context, _mapper);

            _toDoUpdateTaskUseCase = new ToDoUpdateTaskUseCase(_taskReadOnlyRepository, _taskWriteDeleteOnlyRepository);

        }

        [Fact]
        public void WhenReceiveAValidDomainTaskMustUpdateTheTask()
        {
            var domainTask = new DomainTask
            {
                    TaskNumber = 10,
                    EstimatedDate = DateTime.Now.Date.AddDays(40),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title one update", 
                    Description = "Test description one update", 
                    Progress = Progress.ToDo,
                    EndDate = null,
                    StartDate = null,
            };

            _toDoUpdateTaskUseCase.UpdateTask(domainTask);

            var taskReturned = _context.RepositoryTasks
                                    .Single(x => x.TaskId.Equals(domainTask.TaskNumber));

            Assert.Equal(taskReturned.Title, domainTask.Title);
            Assert.Equal(taskReturned.Description, domainTask.Description);
            Assert.Equal(taskReturned.EstimatedDate, domainTask.EstimatedDate);
        }
    }
}