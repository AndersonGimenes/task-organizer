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


        [Fact]
        public void MustInsertTheNewTask()
        {
            var mock = MockDataTask.MockDataTest();
            _registerTaskUseCase.Register(mock);

            var returnTask = _taskReadOnlyRepository.GetAll().Last();
            
            var returnTest = returnTask.Title.Equals(mock.Title) &&
                             returnTask.Description.Equals(mock.Description) &&
                             returnTask.CreateDate.Equals(mock.CreateDate) &&
                             returnTask.EstimetedDate.Equals(mock.EstimetedDate);
                             
            Assert.True(returnTest);
        }

        [Fact]
        public void MustUpdateOnlyTask()
        {        
            var repsitoryTaskUpdate = InsertTaskToTest.InsertAndReturTask();
            // Change the task
            repsitoryTaskUpdate.SetDescription("Update Description");
            repsitoryTaskUpdate.SetTitle("Update Title");
            repsitoryTaskUpdate.IsNew = false;

            // Update task
            _registerTaskUseCase.Register(repsitoryTaskUpdate);

            var returnTask = _taskReadOnlyRepository.Get(repsitoryTaskUpdate.TaskNumeber);

            var returnTest = returnTask.Title.Equals(repsitoryTaskUpdate.Title) &&
                             returnTask.Description.Equals(repsitoryTaskUpdate.Description) &&
                             returnTask.CreateDate.Equals(repsitoryTaskUpdate.CreateDate) &&
                             returnTask.EstimetedDate.Equals(repsitoryTaskUpdate.EstimetedDate);
                             
            Assert.True(returnTest);
        }
        
    }
}