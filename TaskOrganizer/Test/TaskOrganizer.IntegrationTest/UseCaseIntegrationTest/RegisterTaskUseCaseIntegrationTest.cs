using System.Linq;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using Xunit;

namespace TaskOrganizer.IntegrationTest.RepositoryTest
{
    public class RegisterTaskUseCaseIntegrationTest
    {
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        public readonly TaskOrganizerContext _context;

        public RegisterTaskUseCaseIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);            
            _registerTaskUseCase = new RegisterTaskUseCase(_taskWriteDeleteOnlyRepository);
        }


        [Fact(Skip = "Fix this")]
        public void MustInsertTheNewTask()
        {
            var mock = MockDataTask.MockDataTest();
            mock.SetDescription("Changed description test");

            _registerTaskUseCase.Register(mock);

            Helper.IncrementId();

            var returnTask = _taskReadOnlyRepository.Get(Helper.IdBase);
            
            Assert.Equal(returnTask.Title, mock.Title);
            Assert.Equal(returnTask.Description, mock.Description);
            Assert.Equal(returnTask.CreateDate, mock.CreateDate);
            Assert.Equal(returnTask.EstimatedDate, mock.EstimatedDate);
   
        }

        [Fact]
        public void MustUpdateOnlyTask()
        {        
            var repsitoryTaskUpdate = InsertTaskToTest.InsertAndReturTask();
            // Change the task
            repsitoryTaskUpdate.SetDescription("Update Description");
            repsitoryTaskUpdate.SetTitle("Update Title");
            
            // Update task
            _registerTaskUseCase.Register(repsitoryTaskUpdate);
            
            var returnTask = _taskReadOnlyRepository.Get(repsitoryTaskUpdate.TaskNumeber);

            Assert.Equal(returnTask.Title, repsitoryTaskUpdate.Title);
            Assert.Equal(returnTask.Description, repsitoryTaskUpdate.Description);
            Assert.Equal(returnTask.CreateDate, repsitoryTaskUpdate.CreateDate);
            Assert.Equal(returnTask.EstimatedDate, repsitoryTaskUpdate.EstimatedDate);
        }
        
    }
}