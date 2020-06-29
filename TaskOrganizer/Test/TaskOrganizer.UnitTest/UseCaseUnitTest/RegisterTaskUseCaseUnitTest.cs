using System;
using Moq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;
using Xunit;
using TaskOrganizer.UseCase.UseCaseException;
using TaskOrganizer.Domain.Enum;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class RegisterTaskUseCaseUnitTest
    {
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly Mock<ITaskReadOnlyRepository> _mockTaskReadOnlyRepository;
        private readonly Mock<ITaskWriteDeleteOnlyRepository> _mockTaskWriteDeleteOnlyRepository;
        
        public RegisterTaskUseCaseUnitTest()
        {
            _mockTaskReadOnlyRepository = new Mock<ITaskReadOnlyRepository>();
            _mockTaskWriteDeleteOnlyRepository = new Mock<ITaskWriteDeleteOnlyRepository>();
            _registerTaskUseCase = new RegisterTaskUseCase(_mockTaskWriteDeleteOnlyRepository.Object, _mockTaskReadOnlyRepository.Object);
        }

        [Fact]
        public void IfThereIsNotEstimatedDateFilledAssumeTheCurrentDatePlusThirty()
        {
            var domainTask = new DomainTask();

            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimatedDate.Equals(DateTime.Now.Date.AddDays(30)));
        }
        
        [Fact]
        public void IfThereIsEstimatedDateFilledThenKeepTheDateFilled()
        {
            var domainTask = new DomainTask{EstimatedDate = DateTime.Now.Date.AddDays(10)};
            _registerTaskUseCase.Register(domainTask);

            Assert.True(domainTask.EstimatedDate.Equals(DateTime.Now.Date.AddDays(10)));
        }

        [Fact]
        public void WhenTaskNumberIsZeroNewTaskWillBeAddedAndTheReturnedIdWillNotBeZero()
        {
            var result = 0;
            var domainTask = new DomainTask();
            domainTask.TaskNumber = 0;

            _mockTaskWriteDeleteOnlyRepository.Setup(x => x.Add(It.IsAny<DomainTask>())).Returns(new DomainTask{TaskNumber = 1});
            
            domainTask.TaskNumber = _registerTaskUseCase.Register(domainTask).TaskNumber;  

            Assert.NotEqual(domainTask.TaskNumber, result);
        }

        [Fact]
        public void WhenTaskNumberIsZeroNewTaskWillBeAddedAndTheCreatedDateWillBeEqualDateTimeNow()
        {
            var domainTask = new DomainTask();
            domainTask.TaskNumber = 0;

            _registerTaskUseCase.Register(domainTask);  

            Assert.Equal(domainTask.CreateDate, DateTime.Now.Date);
        }

        [Fact]
        public void WhenTaskProgressIsEqualToInProgressAndStartDateNullThenStartDateWillBeFilledWithDateTimeNow()
        {
            var domainTaskOriginal = BaseOriginalTask();
            domainTaskOriginal.StartDate = null;
            
            var domainTask = BaseRequestTask(Progress.InProgress);
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);
                       
            _registerTaskUseCase.Register(domainTask);  

            Assert.Equal(domainTask.StartDate, DateTime.Now.Date);
        }

        [Fact]
        public void WhenTaskProgressIsEqualToDoneAndEndDateIsNullThenEndDateWillBeFilledWithDateTimeNow()
        {
            var domainTaskOriginal = BaseOriginalTask();
            domainTaskOriginal.StartDate = DateTime.Now.Date;

            var domainTask = BaseRequestTask(Progress.Done);
            domainTask.StartDate = DateTime.Now.Date;

            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);
                       
            _registerTaskUseCase.Register(domainTask);  

            Assert.Equal(domainTask.EndDate, DateTime.Now.Date);
        }

        [Fact]
        public void WhenTheTaskProgressIsEqualToInProgressAndTheRequestTitleIsDifferentFromTheOriginalTitleTaskAUseCaseExceptionShouldBeThrown()
        {
            var result = "Title can't be changed";
            var domainTaskOriginal = BaseOriginalTask();

            var domainTaskRequest = BaseRequestTask(Progress.InProgress);
            domainTaskRequest.Title = "Test request title";
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTaskRequest));

            Assert.Equal(ex.Message, result);
        }

        [Fact]
        public void WhenTheTaskProgressIsEqualToInProgressAndTheRequestEstimatedDateIsDifferentFromTheOriginalEstimatedDateATaskAUseCaseExceptionShouldBeThrown()
        {
            var result =  "Estimated date can't be changed";
            var domainTaskOriginal = BaseOriginalTask();
                        
            var domainTaskRequest = BaseRequestTask(Progress.InProgress);
            domainTaskRequest.EstimatedDate = DateTime.Now.Date.AddDays(10);
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTaskRequest));

            Assert.Equal(ex.Message, result);
        }

        [Fact]
        public void WhenTheTaskProgressIsEqualToInProgressAndTheRequestCreateDateIsDifferentFromTheOriginalCreateDateATaskAUseCaseExceptionShouldBeThrown()
        {
            var result =  "Create date can't be changed";
            var domainTaskOriginal = BaseOriginalTask();
            domainTaskOriginal.CreateDate = DateTime.Now.Date;            

            var domainTaskRequest = BaseRequestTask(Progress.InProgress);
            domainTaskRequest.CreateDate = DateTime.Now.Date.AddDays(10);
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTaskRequest));

            Assert.Equal(ex.Message, result);
        }

        [Fact]
        public void WhenTheTaskProgressIsEqualToInProgressAndTheRequestStartDateIsDifferentFromTheOriginalStartDateATaskAUseCaseExceptionShouldBeThrown()
        {
            var result =  "Start date can't be changed";
            var domainTaskOriginal = BaseOriginalTask();
            domainTaskOriginal.CreateDate = DateTime.Now.Date;    
            domainTaskOriginal.StartDate = DateTime.Now.Date;        

            var domainTaskRequest = BaseRequestTask(Progress.InProgress);
            domainTaskRequest.CreateDate = DateTime.Now.Date;
            domainTaskRequest.StartDate = DateTime.Now.Date.AddDays(10);
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTaskRequest));

            Assert.Equal(ex.Message, result);
        }

        [Fact]
        public void WhenTheTaskProgressIsEqualToDoneAndTheRequestDescriptionIsDifferentFromTheOriginalDescriptionATaskAUseCaseExceptionShouldBeThrown()
        {
            var result = "Description can't be changed";
            var domainTaskOriginal = BaseOriginalTask();
            
            var domainTaskRequest = BaseRequestTask(Progress.Done);
            domainTaskRequest.Description = "Request Description";
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTaskRequest));

            Assert.Equal(ex.Message, result);
        }

         [Fact]
        public void WhenTheTaskProgressIsEqualToDoneAndTheRequestEndDateIsDifferentFromTheOriginalEndDateATaskAUseCaseExceptionShouldBeThrown()
        {
            var result =  "End date can't be changed";
            var domainTaskOriginal = BaseOriginalTask();
            domainTaskOriginal.StartDate = DateTime.Now.Date; 
            domainTaskOriginal.EndDate = DateTime.Now.Date;       

            var domainTaskRequest = BaseRequestTask(Progress.Done);
            domainTaskRequest.StartDate = DateTime.Now.Date;
            domainTaskRequest.EndDate = DateTime.Now.Date.AddDays(10);
            
            _mockTaskReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(domainTaskOriginal);

            var ex = Assert.Throws<UseCaseException>(() => _registerTaskUseCase.Register(domainTaskRequest));

            Assert.Equal(ex.Message, result);
        }

        #region AuxiliaryMethods
        private DomainTask BaseOriginalTask()
        {
            return new DomainTask
            {
                TaskNumber = 1,
                EstimatedDate = DateTime.Now.Date,
                CreateDate = DateTime.Now.Date,
                Title = "Test original title", 
                Description = "Test original description"
            };
        }


        private DomainTask BaseRequestTask(Progress progress)
        {
            return new DomainTask
            {
                TaskNumber = 1,
                EstimatedDate = DateTime.Now.Date,
                CreateDate = DateTime.Now.Date,
                Title = "Test original title", 
                Description = "Test original description", 
                Progress = progress
            };
        }

        #endregion
    }
}