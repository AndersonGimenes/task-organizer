using System;
using TaskOrganizer.Domain.ContractUseCase.Task.Done;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.Done;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class DoneUseCaseTest
    {
        private readonly TaskOrganizerContext _context;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly TaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IDoneUseCase _doneUseCase;

        public DoneUseCaseTest()
        {

            InsertMockDataBaseInMemory.InsertMock();

            _context = DataBaseInMemory.ReturnContext();   
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);

            _doneUseCase = new DoneUseCase(_taskReadOnlyRepository, _taskWriteDeleteOnlyRepository); 
        }

        [Fact]
        public void IfDomainTaskIsValidAndProgressEqualInProgressShouldBeUpdateEndDateAndProgress()
        {
            var domainTask = ReturnNewDomainTask(300);

            _doneUseCase.UpdateChangeTask(domainTask);

            var domainTaskDto = _taskReadOnlyRepository.Get(domainTask.TaskNumber);

            // Fields updated
            Assert.Equal(DateTime.Now.Date , domainTaskDto.EndDate);
            Assert.Equal(Progress.Done, domainTaskDto.Progress);
            // Other fields compared
            Assert.Equal(domainTask.TaskNumber, domainTaskDto.TaskNumber);
            Assert.Equal(domainTask.Title, domainTaskDto.Title);
            Assert.Equal(domainTask.Description, domainTaskDto.Description);
            Assert.Equal(domainTask.CreateDate, domainTaskDto.CreateDate);
            Assert.Equal(domainTask.EstimatedDate, domainTaskDto.EstimatedDate);

        }

        #region [ Auxiliary methods ]
        
        private DomainTask ReturnNewDomainTask(int taskNumber)
        {
            return new DomainTask
            {
               TaskNumber = taskNumber,
               Title = "Test title three hundred",
               Description = "Test description three hundred", 
               Progress = Progress.InProgress,
               CreateDate = DateTime.Now.Date.AddDays(-8),
               EstimatedDate = DateTime.Now.Date.AddDays(20),
               StartDate = DateTime.Now.Date.AddDays(-5)
            };
        }

        #endregion
    }
}