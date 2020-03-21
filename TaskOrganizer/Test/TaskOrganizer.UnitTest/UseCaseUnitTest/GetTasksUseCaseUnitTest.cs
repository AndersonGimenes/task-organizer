using Moq;
using System;
using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;
using Xunit;

namespace TaskOrganizer.UnitTest.UseCaseUnitTest
{
    public class GetTasksUseCaseUnitTest
    {
        private IGetTasksUseCase _getTasksUseCase;
        private readonly Mock<ITaskReadOnlyRepository> _mockTaskReadOnlyRepository;

        public GetTasksUseCaseUnitTest()
        {
            _mockTaskReadOnlyRepository = new Mock<ITaskReadOnlyRepository>();
        }

       [Fact]
        public void MustReturnListOfDomainTask()
        {
            _mockTaskReadOnlyRepository.Setup(x => x.GetAll()).Returns(MockListTask());
            _getTasksUseCase = new GetTasksUseCase(_mockTaskReadOnlyRepository.Object);

            var resultReturn = _getTasksUseCase.GetAll();

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
