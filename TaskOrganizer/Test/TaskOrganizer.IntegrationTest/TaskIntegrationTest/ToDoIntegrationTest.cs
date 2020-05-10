using System;
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
    public class ToDoIntegrationTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly ToDoController _toDoController;
        private readonly Mock<IUrlHelper> _urlHelperMock;

        public ToDoIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context);            
            _registerTaskUseCase = new RegisterTaskUseCase(_taskWriteDeleteOnlyRepository, _taskReadOnlyRepository);
            _getTasksUseCase = new GetTasksUseCase(_taskReadOnlyRepository);
            _deleteTaskUseCase = new DeleteTaskUseCase(_taskWriteDeleteOnlyRepository);
            _toDoController = new ToDoController(_getTasksUseCase,_registerTaskUseCase, _deleteTaskUseCase);
            _urlHelperMock = new Mock<IUrlHelper>();
        }

        [Fact]
        public void MustBeInsertANewTask()
        {
            _urlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns($"/api/ToDo/Task/");
            _toDoController.Url = _urlHelperMock.Object;

            var taskRequest = JsonDeserialize(ReturnJsonInsertNewTask());
            var returnTask = (CreatedResult)_toDoController.Insert(taskRequest);
            
            var obj = returnTask.Value;
            var taskModel = MapperReturnControllerToTaskModelTest(obj);

            char.TryParse(taskModel.TaskNumber.ToString(), out char charId);
            returnTask.Location += charId;

            Assert.Equal(returnTask.StatusCode, 201);
            Assert.Equal(returnTask.Location, $"/api/ToDo/Task/{charId}");
            Assert.Equal(taskRequest.Title, taskModel.Title);
            Assert.Equal(taskRequest.Description, taskModel.Description);
            Assert.Equal(taskRequest.Progress, taskModel.Progress);
            Assert.Equal(taskRequest.EstimatedDate, taskModel.EstimatedDate);
            Assert.Equal(DateTime.Now.Date, taskModel.CreateDate);

        }

        [Fact]
        public void MustBeUpdateATask()
        {
            var taskNumber = InsertTaskToTest.InsertAndReturTask("ToDo").TaskNumber;

            var taskRequest = JsonDeserialize(ReturnJsonUpdateTask(taskNumber));
            var returnTask = (OkResult)_toDoController.Update(taskRequest);
            
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
        public void MustReturnJustOnlyTask()
        {
            var task = InsertTaskToTest.InsertAndReturTask("ToDo");
                        
            var returnTask = (OkObjectResult)_toDoController.Get(task.TaskNumber);
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

            var taskRequest = JsonDeserialize(ReturnJsonUpdateTask(taskNumber));
            var returnTask = (NoContentResult)_toDoController.Delete(taskRequest);
            
            var ex = Assert.Throws<InvalidOperationException>(() => _getTasksUseCase.Get(taskNumber));
            Assert.Equal(ex.Message, "Sequence contains no elements");
            Assert.Equal(returnTask.StatusCode, 204);
        }
        [Fact]
        public void IfStardDateOrEndDateWasFillThenAArgumentExceptionWillBeThrows()
        {
            var taskRequest = JsonDeserialize(ReturnInvalidJsonInsert());
            var badRequest = (BadRequestObjectResult) _toDoController.Insert(taskRequest); 

            Assert.Equal(badRequest.Value, "When Progress is ToDo cannot record StartDate.\nWhen Progress is ToDo cannot record EndDate.");
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
       
        private string ReturnJsonInsertNewTask()
        {
            return @"{
                    'title': 'Title Insert',
                    'description': 'Description Insert',
                    'progress': 'ToDo',
                    'estimatedDate': '2020-05-15',
                    }";
        }

        private string ReturnInvalidJsonInsert()
        {
            return @"{
                    'title': 'Title Insert',
                    'description': 'Description Insert',
                    'progress': 'ToDo',
                    'estimatedDate': '2020-05-15',
                    'startDate': '2020-05-15',
                    'endDate': '2020-05-15',
                    }";
        }

        private string ReturnJsonUpdateTask(int taskNumber)
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

        #endregion        
    }
}