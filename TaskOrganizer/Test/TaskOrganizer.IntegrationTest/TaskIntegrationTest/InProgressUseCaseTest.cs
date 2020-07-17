using System;
using TaskOrganizer.Domain.ContractUseCase.Task.InProgress;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.InProgress;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class InProgressUseCaseTest
    {
        private readonly TaskOrganizerContext _context;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly TaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IInProgressUseCase _inProgressUseCase;

        public InProgressUseCaseTest()
        {

            InsertMockDataBaseInMemory.InsertMock();

            _context = DataBaseInMemory.ReturnContext();   
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);

            _inProgressUseCase = new InProgressUseCase(_taskReadOnlyRepository, _taskWriteDeleteOnlyRepository); 
        }

        [Fact]
        public void IfDomainTaskIsValidAndProgressEqualToDoShouldBeUpdateStartDateAndProgress()
        {
            var domainTask = ReturnNewDomainTask(100, null, Progress.ToDo, "Test title one hundred");

            _inProgressUseCase.UpdateChangeTask(domainTask);

            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);

            // Fields updated
            Assert.Equal(DateTime.Now.Date , domainTaskDto.StartDate);
            Assert.Equal(Progress.InProgress, domainTaskDto.Progress);
            // Other fields compared
            Assert.Equal(domainTask.TaskNumber, domainTaskDto.TaskNumber);
            Assert.Equal(domainTask.Title, domainTaskDto.Title);
            Assert.Equal(domainTask.Description, domainTaskDto.Description);
            Assert.Equal(domainTask.CreateDate, domainTaskDto.CreateDate);
            Assert.Equal(domainTask.EstimatedDate, domainTaskDto.EstimatedDate);

        }

        [Fact]
        public void IfDomainTaskIsValidAndProgressEqualInProgressShouldBeUpdateJustDescription()
        {
            var domainTask = ReturnNewDomainTask(200, DateTime.Now.Date.AddDays(-5), Progress.InProgress, "Test title two hundred");
            domainTask.Description = "description test";

            _inProgressUseCase.UpdateTask(domainTask);

            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);

            // Field Updated
            Assert.Equal("description test" ,domainTaskDto.Description);
            // Other fields compared
            Assert.Equal(domainTask.TaskNumber, domainTaskDto.TaskNumber);
            Assert.Equal(domainTask.StartDate , domainTaskDto.StartDate);
            Assert.Equal(domainTask.Progress, domainTaskDto.Progress);
            Assert.Equal(domainTask.Title, domainTaskDto.Title);
            Assert.Equal(domainTask.CreateDate, domainTaskDto.CreateDate);
            Assert.Equal(domainTask.EstimatedDate, domainTaskDto.EstimatedDate);
        }

        #region [ Auxiliary methods ]
        
        private DomainTask ReturnNewDomainTask(int taskNumber, DateTime? date, Progress progress, string title)
        {
            return new DomainTask
            {
               TaskNumber = taskNumber,
               Title = title,
               Description = "Property can be update.", 
               Progress = progress,
               CreateDate = DateTime.Now.Date.AddDays(-8),
               EstimatedDate = DateTime.Now.Date.AddDays(20),
               StartDate = date
            };
        }

        #endregion
    }
}