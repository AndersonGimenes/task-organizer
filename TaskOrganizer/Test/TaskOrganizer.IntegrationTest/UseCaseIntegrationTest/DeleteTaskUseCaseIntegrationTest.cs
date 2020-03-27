using System.Linq;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest
{
    public class DeleteTaskUseCaseIntegrationTest
    {
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly TaskOrganizerContext _context;
        private readonly TaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly TaskReadOnlyRepository _taskReadOnlyRepositoy;

        public DeleteTaskUseCaseIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);
            _taskReadOnlyRepositoy = new TaskReadOnlyRepository(_context);
            _deleteTaskUseCase = new DeleteTaskUseCase(_taskWriteDeleteOnlyRepository);             
        }
         
        [Fact(Skip = "Fix this")]
        public void MustBeDeleteOnlyTask()
        {
            var task = InsertTaskToTest.InsertAndReturTask();
            var countBeforeDelete = ReturnCountTask();
            
            _deleteTaskUseCase.Delete(task);

            var countAfterDelete = ReturnCountTask();

            Assert.True(countBeforeDelete > countAfterDelete);

        }

        #region  AuxiliaryMethods
        private int ReturnCountTask()
        {
            int count = 0;
            using( var context = DataBaseInMemory.ReturnContext())
            {
                var taskReadOnlyRepositoy = new TaskReadOnlyRepository(context);
                count = taskReadOnlyRepositoy.GetAll().Count();
            }
           
            return count;
        }

        #endregion

    }
}