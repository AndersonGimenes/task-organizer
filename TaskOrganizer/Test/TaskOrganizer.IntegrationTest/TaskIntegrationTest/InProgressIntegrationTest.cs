using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Newtonsoft.Json;
using TaskOrganizer.Api.Controllers;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class InProgressIntegrationTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly InProgressController _inProgressController;
        
        public InProgressIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);            
            _registerTaskUseCase = new RegisterTaskUseCase(_taskWriteDeleteOnlyRepository, _taskReadOnlyRepository);
            _getTasksUseCase = new GetTasksUseCase(_taskReadOnlyRepository);
            _inProgressController = new InProgressController(_getTasksUseCase,_registerTaskUseCase);
        }

        [Fact]
        public void MustBeUpdateAInProgressTask()
        {
            var taskNumber = InsertTaskToTest.InsertAndReturTask("ToDo").TaskNumber;

            var taskRequest = JsonDeserialize(ReturnJsonUpdateTask(taskNumber));
            OkResult returnTask = (OkResult)_inProgressController.Update(taskRequest);
            
            var taskReturn = _getTasksUseCase.Get(taskNumber);
            
            Assert.Equal(returnTask.StatusCode, 200);
            Assert.Equal(taskRequest.TaskNumber, taskReturn.TaskNumber);
            Assert.Equal(taskRequest.Title, taskReturn.Title);
            Assert.Equal(taskRequest.Description, taskReturn.Description);
            Assert.Equal(taskRequest.Progress, taskReturn.Progress.ToString());
            Assert.Equal(taskRequest.EstimatedDate, taskReturn.EstimatedDate);
            Assert.Equal(DateTime.Now.Date, taskReturn.StartDate);
        }

        [Fact]
        public void MustReturnJustOnlyTask()
        {
            var task = InsertTaskToTest.InsertAndReturTask("ToDo");
                        
            OkObjectResult returnTask = (OkObjectResult)_inProgressController.Get(task.TaskNumber);
            var obj = returnTask.Value;
            var taskModel = MapperReturnControllerToTaskModelTest(obj);

            Assert.Equal(taskModel.Title, task.Title);
            Assert.Equal(taskModel.Description, task.Description);
            Assert.Equal(taskModel.CreateDate, task.CreateDate);
            Assert.Equal(taskModel.EstimatedDate, task.EstimatedDate);
        }

        [Fact]
        public void IfEndDateWasFillThenAArgumentExceptionWillBeThrows()
        {
            var taskRequest = JsonDeserialize(ReturnInvalidJsonUpdate());
            var badRequest = (BadRequestObjectResult) _inProgressController.Update(taskRequest); 

            Assert.Equal(badRequest.Value, "When Progress is ToDo cannot record EndDate.");
        }

        #region AuxiliaryMethods
        private TaskModel JsonDeserialize(string json)
        {
            return JsonConvert.DeserializeObject<TaskModel>(json);
        }

        private TaskModel MapperReturnControllerToTaskModelTest(object obj)
        {
            var type = obj.GetType();
            TaskModel taskModel = new TaskModel
            {
                TaskNumber = Convert.ToInt32(type.GetProperty(nameof(taskModel.TaskNumber)).GetValue(obj)),
                Title = type.GetProperty(nameof(taskModel.Title)).GetValue(obj).ToString(),
                Description = type.GetProperty(nameof(taskModel.Description)).GetValue(obj).ToString(),
                Progress = type.GetProperty(nameof(taskModel.Progress)).GetValue(obj).ToString(),
                EstimatedDate = Convert.ToDateTime(type.GetProperty(nameof(taskModel.EstimatedDate)).GetValue(obj)),
                CreateDate = Convert.ToDateTime(type.GetProperty(nameof(taskModel.CreateDate)).GetValue(obj)),
                StartDate = Convert.ToDateTime(type.GetProperty(nameof(taskModel.StartDate)).GetValue(obj)),
                EndDate = Convert.ToDateTime(type.GetProperty(nameof(taskModel.EndDate)).GetValue(obj))
            };
            
            return taskModel;
        }
       
        private string ReturnJsonUpdateTask(int taskNumber)
        {
            return string.Format( @"{{
                          'taskNumber': {0},
                          'title': 'Title insert',
                          'description': 'Description Update',
                          'progress': 'InProgress',
                          'estimatedDate': '2020-05-15'
                          }}", taskNumber);
        }

        private string ReturnInvalidJsonUpdate()
        {
            return @"{
                    'taskNumber': 1,
                    'title': 'Title insert',
                    'description': 'Description Update',
                    'progress': 'InProgress',
                    'estimatedDate': '2020-05-15',
                    'startDate': '2020-05-15',
                    'endDate': '2020-05-15'
                    }";
        }

        #endregion        
    }
}