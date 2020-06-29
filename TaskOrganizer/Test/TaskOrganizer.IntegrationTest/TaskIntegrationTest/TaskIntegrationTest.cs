using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class TaskIntegrationTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IMapper _mapper;
        private readonly TaskController _taskController;

        public TaskIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _getTasksUseCase = new GetTasksUseCase(_taskReadOnlyRepository);
            _mapper = CreateMapper.CreateMapperProfile();
            _taskController = new TaskController(_getTasksUseCase, _mapper);
        }

        [Fact]
        public void MustReturnAllTasks()
        {
            for(var x = 0; x < 4; x++)
            {                       
                InsertTaskToTest.InsertAndReturTask(Progress.ToDo);
            }  

            OkObjectResult returnTask = (OkObjectResult)_taskController.GetAll();
            var list = (List<TaskModel>)returnTask.Value;

            Assert.True(list.Count > 0);
          
        }
 
    }
}