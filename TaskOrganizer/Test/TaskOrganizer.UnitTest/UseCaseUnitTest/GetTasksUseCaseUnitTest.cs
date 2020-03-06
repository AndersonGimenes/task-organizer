using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.ContractUseCase;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class GetTasksUseCaseUnitTest
    {
        private IGetTasksUseCase _getTasksUseCase;
        private readonly Mock<ITaskReadOnlyRepositoy> _mockTaskReadOnlyRepository;

        public GetTasksUseCaseUnitTest()
        {
            _mockTaskReadOnlyRepository = new Mock<ITaskReadOnlyRepositoy>();
        }

       [Fact]
        public void MustReturnListOfDomainTask()
        {
            _mockTaskReadOnlyRepository.Setup(x => x.GetAll()).Returns(MockListTask());
            _getTasksUseCase = new GetTasksUseCase(_mockTaskReadOnlyRepository.Object);

            var resultReturn = _getTasksUseCase.Get();

            Assert.Collection(resultReturn, task => Assert.Contains("Title one", task.Title),
                                            task => Assert.Contains("Title two", task.Title),
                                            task => Assert.Contains("Title three", task.Title),
                                            task => Assert.Contains("Title four", task.Title));
        }

        #region AuxiliaryMethods
        private IList<DomainTask> MockListTask()
        {
            return new List<DomainTask>
            {
                DomainTaskGenerator("Title one" ),
                DomainTaskGenerator("Title two" ),
                DomainTaskGenerator("Title three" ),
                DomainTaskGenerator("Title four" )
            };
        }

        private DomainTask DomainTaskGenerator(string title)
        {
            var data = new DomainTask { CreateDate = DateTime.Now, EstimetedDate = DateTime.Now.AddDays(30), Progress = Progress.ToDo, IsNew = true };
            data.SetTitle(title);
            data.SetDescription("Description test");

            return data;
        }
        #endregion
    }
}
