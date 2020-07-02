using System.Linq;
using System;
using System.Collections.Generic;
using Moq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class TaskUseCaseTest
    {
        private readonly Mock<ITaskReadOnlyRepository> _taskReadOnlyRepository;
        private readonly TaskUseCase _taskUseCase;

        public TaskUseCaseTest()
        {    
           _taskReadOnlyRepository = new Mock<ITaskReadOnlyRepository>();
           _taskUseCase = new TaskUseCase(_taskReadOnlyRepository.Object);
        }

        [Fact]
        public void MustReturnAllTasks()
        {
            _taskReadOnlyRepository
                .Setup(x => x.GetAll())
                .Returns(ReturnListDomainTask());

            var domainTaskList = _taskUseCase.GetAll();

            Assert.True(domainTaskList.Count > 0);
          
        }

        [Fact]
        public void MustReturnAEspecificTask()
        {
            var taskNumber = 3;
            var taskMock = ReturnListDomainTask().SingleOrDefault(x => x.TaskNumber.Equals(taskNumber));
            
            _taskReadOnlyRepository
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(ReturnListDomainTask().SingleOrDefault(x => x.TaskNumber.Equals(taskNumber)));

            var taskRetorned = _taskUseCase.Get(taskNumber);

            Assert.Equal(taskRetorned.TaskNumber, taskMock.TaskNumber);
            Assert.Equal(taskRetorned.Title, taskMock.Title);
            Assert.Equal(taskRetorned.Description, taskMock.Description);
            Assert.Equal(taskRetorned.CreateDate, taskMock.CreateDate);
            Assert.Equal(taskRetorned.Progress, taskMock.Progress);
            Assert.Equal(taskRetorned.EstimatedDate, taskMock.EstimatedDate);
            Assert.Equal(taskRetorned.EndDate, taskMock.EndDate);
            Assert.Equal(taskRetorned.StartDate, taskMock.StartDate);
        }

        #region [ Mock ]
        private List<DomainTask> ReturnListDomainTask()
        {
            return new List<DomainTask>
            {
                new DomainTask
                {
                    TaskNumber = 1,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title one", 
                    Description = "Test description one", 
                    Progress = Progress.ToDo,
                    EndDate = null,
                    StartDate = null,
                },
                new DomainTask
                {
                    TaskNumber = 2,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title two", 
                    Description = "Test description two", 
                    Progress = Progress.InProgress,
                    EndDate = null,
                    StartDate = DateTime.Now.Date.AddDays(5)
                },
                new DomainTask
                {
                    TaskNumber = 3,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title three", 
                    Description = "Test description three", 
                    Progress = Progress.Done,
                    EndDate = DateTime.Now.Date.AddDays(30),
                    StartDate = DateTime.Now.Date.AddDays(5)
                }
            };
        }
        #endregion        
    }
}