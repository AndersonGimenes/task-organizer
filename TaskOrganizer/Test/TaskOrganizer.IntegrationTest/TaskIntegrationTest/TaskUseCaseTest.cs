using AutoMapper;
using TaskOrganizer.Domain.ContractUseCase.Task;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class TaskUseCaseTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly ITaskUseCase _taskUseCase;
        private readonly IMapper _mapper;

        public TaskUseCaseTest()
        {

            _mapper = CreateMapper.CreateMapperProfile();

            InsertMockDataBaseInMemory.InsertMock();               
                        
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context, _mapper);

            _taskUseCase = new TaskUseCase(_taskReadOnlyRepository);
        }

        [Fact]
        public void MustReturnAllTasks()
        {
            var list = _taskUseCase.GetAll();
            
            Assert.True(list.Count > 0);
        }

        [Fact]
        public void MustReturnAEspecificTask()
        {
            var taskNumber = 30;
            var taskDto = _taskReadOnlyRepository.Get(taskNumber);

            var taskRetorned = _taskUseCase.Get(taskNumber);

            Assert.Equal(taskRetorned.TaskNumber, taskDto.TaskNumber);
            Assert.Equal(taskRetorned.Title, taskDto.Title);
            Assert.Equal(taskRetorned.Description, taskDto.Description);
            Assert.Equal(taskRetorned.CreateDate, taskDto.CreateDate);
            Assert.Equal(taskRetorned.Progress, taskDto.Progress);
            Assert.Equal(taskRetorned.EstimatedDate, taskDto.EstimatedDate);
            Assert.Equal(taskRetorned.EndDate, taskDto.EndDate);
            Assert.Equal(taskRetorned.StartDate, taskDto.StartDate);
        }
      
    }
}