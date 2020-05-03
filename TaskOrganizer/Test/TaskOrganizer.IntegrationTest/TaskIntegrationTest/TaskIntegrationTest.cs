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

namespace TaskOrganizer.IntegrationTest.RepositoryTest
{
    public class TaskIntegrationTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly TaskController _taskController;
        private readonly Mock<IUrlHelper> _urlHelperMock;

        public TaskIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);            
            _registerTaskUseCase = new RegisterTaskUseCase(_taskWriteDeleteOnlyRepository, _taskReadOnlyRepository);
            _getTasksUseCase = new GetTasksUseCase(_taskReadOnlyRepository);
            _deleteTaskUseCase = new DeleteTaskUseCase(_taskWriteDeleteOnlyRepository);
            _taskController = new TaskController(_getTasksUseCase,_registerTaskUseCase, _deleteTaskUseCase);
            _urlHelperMock = new Mock<IUrlHelper>();
        }

        [Fact]
        public void MustBeInsertANewTask()
        {
            _urlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns($"/api/Task/");
            _taskController.Url = _urlHelperMock.Object;

            var taskRequest = JsonDeserialize(ReturnJsonInsertNewTaskToDo());
            CreatedResult returnTask = (CreatedResult)_taskController.Insert(taskRequest);
            
            var obj = returnTask.Value;
            var taskModel = MapperReturnControllerToTaskModelTest(obj);

            char.TryParse(taskModel.TaskNumber.ToString(), out char charId);
            returnTask.Location += charId;

            Assert.Equal(returnTask.StatusCode, 201);
            Assert.Equal(returnTask.Location, $"/api/Task/{charId}");
            Assert.Equal(taskRequest.Title, taskModel.Title);
            Assert.Equal(taskRequest.Description, taskModel.Description);
            Assert.Equal(taskRequest.Progress, taskModel.Progress);
            Assert.Equal(taskRequest.EstimatedDate, taskModel.EstimatedDate);
            Assert.Equal(DateTime.Now.Date, taskModel.CreateDate);

        }

        [Fact]
        public void MustBeUpdateAToDoTask()
        {
            var taskNumber = InsertTaskToTest.InsertAndReturTask("ToDo").TaskNumber;

            var taskRequest = JsonDeserialize(ReturnJsonUpdateTaskToDo(taskNumber));
            OkResult returnTask = (OkResult)_taskController.Update(taskRequest);
            
            var taskReturn = _getTasksUseCase.Get(taskNumber);
            
            Assert.Equal(returnTask.StatusCode, 200);
            Assert.Equal(taskRequest.TaskNumber, taskReturn.TaskNumber);
            Assert.Equal(taskRequest.Title, taskReturn.Title);
            Assert.Equal(taskRequest.Description, taskReturn.Description);
            Assert.Equal(taskRequest.Progress, taskReturn.Progress.ToString());
            Assert.Equal(taskRequest.EstimatedDate, taskReturn.EstimatedDate);
            Assert.Equal(taskRequest.CreateDate, taskReturn.CreateDate);
        }

        [Fact]
        public void MustBeUpdateAInProgressTask()
        {
            var taskNumber = InsertTaskToTest.InsertAndReturTask("ToDo").TaskNumber;

            var taskRequest = JsonDeserialize(ReturnJsonUpdateTaskInProgress(taskNumber));
            OkResult returnTask = (OkResult)_taskController.Update(taskRequest);
            
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
        public void MustReturnAllTasks()
        {
            for(var x = 0; x < 4; x++)
            {                       
                InsertTaskToTest.InsertAndReturTask("ToDo");
            }  

            OkObjectResult returnTask = (OkObjectResult)_taskController.GetAll();
            var list = (List<TaskModel>)returnTask.Value;

            Assert.True(list.Count > 0);
          
        }

        [Fact]
        public void MustReturnJustOnlyTask()
        {
            var task = InsertTaskToTest.InsertAndReturTask("ToDo");
                        
            OkObjectResult returnTask = (OkObjectResult)_taskController.Get(task.TaskNumber);
            var obj = returnTask.Value;
            var taskModel = MapperReturnControllerToTaskModelTest(obj);

            Assert.Equal(taskModel.Title, task.Title);
            Assert.Equal(taskModel.Description, task.Description);
            Assert.Equal(taskModel.CreateDate, task.CreateDate);
            Assert.Equal(taskModel.EstimatedDate, task.EstimatedDate);
        }

        [Fact]
        public void MustBeDeleteATask()
        {
            var taskNumber = InsertTaskToTest.InsertAndReturTask("ToDo").TaskNumber;

            var taskRequest = JsonDeserialize(ReturnJsonUpdateTaskInProgress(taskNumber));
            NoContentResult returnTask = (NoContentResult)_taskController.Delete(taskRequest);
            
            var ex = Assert.Throws<InvalidOperationException>(() => _getTasksUseCase.Get(taskNumber));
            Assert.Equal(ex.Message, "Sequence contains no elements");
            Assert.Equal(returnTask.StatusCode, 204);
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
       
        private string ReturnJsonInsertNewTaskToDo()
        {
            return @"{
                    'title': 'Title Insert',
                    'description': 'Description Insert',
                    'progress': 'ToDo',
                    'estimatedDate': '2020-05-15',
                    }";
        }

        private string ReturnJsonUpdateTaskToDo(int taskNumber)
        {
            var createDate = DateTime.Now.ToString("yyyy-MM-dd");
            return string.Format( @"{{
                          'taskNumber': {0},
                          'title': 'Title Update',
                          'description': 'Description Update',
                          'createDate': '{1}',
                          'progress': 'ToDo',
                          'estimatedDate': '2020-05-25'
                          }}", taskNumber, createDate);
        }

        private string ReturnJsonUpdateTaskInProgress(int taskNumber)
        {
            return string.Format( @"{{
                          'taskNumber': {0},
                          'title': 'Title insert',
                          'description': 'Description Update',
                          'progress': 'InProgress',
                          'estimatedDate': '2020-05-15'
                          }}", taskNumber);
        }

        #endregion        
    }
}