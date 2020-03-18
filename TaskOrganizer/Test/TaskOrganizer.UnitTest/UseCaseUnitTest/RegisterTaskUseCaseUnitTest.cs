using System;
using Moq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class RegisterTaskUseCaseUnitTest
    {
        private IRegisterTaskUseCase _registerTaskUseCase;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _mockTaskWriteDeleteOnlyRepository;
        
        public RegisterTaskUseCaseUnitTest()
        {
            _mockTaskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object);
        }

        [Fact]
        public void ThereMustBeACreateDateEqualToTheCurrentDate()
        {
            var domainTask = new DomainTask
            {
                IsNew = true
            };

            _registerTaskUseCase.Register(domainTask);  

            Assert.True(domainTask.CreateDate.Equals(DateTime.Now.Date));
        }
        
        [Fact]
        public void IfThereIsNotEstimetedDateFilledAssumeTheCurrentDatePlusThirty()
        {
            var domainTask = new DomainTask();

            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimetedDate.Equals(DateTime.Now.Date.AddDays(30)));
        }
        
        [Fact]
        public void IfThereIsEstimetedDateFilledThenKeepTheDateFilled()
        {
            var domainTask = new DomainTask{EstimetedDate = DateTime.Now.Date.AddDays(10)};
            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimetedDate.Equals(DateTime.Now.Date.AddDays(10)));
        }

        [Fact]
        public void WhenCreatingNewRegisterTheSystemWillPutAutomaticallyTheProgressWithToDo()
        {
            var domainTask = new DomainTask
            {
                IsNew = true
            };
            
            _registerTaskUseCase.Register(domainTask);  

            Assert.True(domainTask.Progress.Equals(Progress.ToDo));
        }
        
        [Fact]
        public void IfIsNewTaskThenCallInsertMethod()
        {
            var domainTask = new DomainTask
            {
                EstimetedDate = DateTime.Now.Date.AddDays(25),
                IsNew = true
            };
            domainTask.SetTitle("Title test");
            domainTask.SetDescription("Description test");
            
            _mockTaskWriteDeleteOnlyRepository.Setup(x => x.Add(It.IsAny<DomainTask>()));
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object);

            _registerTaskUseCase.Register(domainTask);

            var testOk = 
                domainTask.CreateDate.Equals(DateTime.Now.Date) &&
                domainTask.EstimetedDate.Equals(DateTime.Now.Date.AddDays(25)) &&
                domainTask.Progress.Equals(Progress.ToDo) &&
                domainTask.Title.Equals("Title test") &&
                domainTask.Description.Equals("Description test");

            Assert.True(testOk);
        }

        [Fact]
        public void IfIsNotNewTaskThenCallUpdateMethod()
        {
            var domainTask = new DomainTask
            {
                CreateDate = DateTime.Now.Date.AddDays(-10),
                EstimetedDate = DateTime.Now.Date.AddDays(10),
                Progress = Progress.ToDo,
                IsNew = false
            };
            domainTask.SetTitle("Title test update");
            domainTask.SetDescription("Description test update");
            
            _mockTaskWriteDeleteOnlyRepository.Setup(x => x.Update(It.IsAny<DomainTask>()));
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object);

            _registerTaskUseCase.Register(domainTask);

            var testOk = 
                domainTask.CreateDate.Equals(DateTime.Now.Date.AddDays(-10)) &&
                domainTask.EstimetedDate.Equals(DateTime.Now.Date.AddDays(10)) &&
                domainTask.Progress.Equals(Progress.ToDo) &&
                domainTask.Title.Equals("Title test update") &&
                domainTask.Description.Equals("Description test update");

            Assert.True(testOk);
        }
    }
}