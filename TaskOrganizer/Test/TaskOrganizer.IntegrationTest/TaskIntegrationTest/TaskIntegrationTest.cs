using System;
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
using TaskOrganizer.UseCase.Task;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class TaskIntegrationTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly TaskUseCase _taskUseCase;
        private readonly IMapper _mapper;
        private readonly TaskController _taskController;

        public TaskIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskUseCase = new TaskUseCase(_taskReadOnlyRepository);
            _mapper = CreateMapper.CreateMapperProfile();
            _taskController = new TaskController(_taskUseCase, _mapper);
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

        [Fact(Skip="Fix this")]
        public void MustReturnAEspecificTask()
        {
            var task = InsertTaskToTest.InsertAndReturTask(Progress.ToDo);
            
            OkObjectResult returnTask = (OkObjectResult)_taskController.Get(task.TaskNumber);
            var taskRetorned = (TaskModel)returnTask.Value;

            Assert.Equal(taskRetorned.TaskNumber, task.TaskNumber);
            Assert.Equal(taskRetorned.Title, task.Title);
            Assert.Equal(taskRetorned.Description, task.Description);
            Assert.Equal(taskRetorned.CreateDate, DateTime.Now.Date);
            Assert.Equal(taskRetorned.Progress, task.Progress.ToString());
            Assert.Equal(taskRetorned.EstimatedDate, task.EstimatedDate);
        }
 
    }
}