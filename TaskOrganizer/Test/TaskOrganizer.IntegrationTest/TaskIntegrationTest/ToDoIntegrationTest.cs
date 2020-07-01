using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Newtonsoft.Json;
using TaskOrganizer.Api.Controllers;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.UseCase.Task.ToDo;
using Xunit;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest
{
    public class ToDoIntegrationTest
    {
        private readonly TaskOrganizerContext _context;        
        private readonly ITaskWriteDeleteOnlyRepository _taskWriteDeleteOnlyRepository;
        private readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ToDoUseCase _toDoUseCase;
        private readonly ToDoController _toDoController;
        private readonly Mock<IUrlHelper> _urlHelperMock;

        public ToDoIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context); 

            _mapper = CreateMapper.CreateMapperProfile();
            
            _toDoUseCase = new ToDoUseCase(_taskWriteDeleteOnlyRepository, _taskReadOnlyRepository);
            
            _toDoController = new ToDoController(_toDoUseCase, _mapper);
            
            _urlHelperMock = new Mock<IUrlHelper>();
        }

        [Fact(Skip="fix this")]
        public void MustBeInsertANewTask()
        {
            var statusCodeResult = 201;

            _urlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns($"/api/ToDo/Task/");
            _toDoController.Url = _urlHelperMock.Object;

            var taskRequest = JsonConvert.DeserializeObject<ToDoModel>(ReturnJsonInsertNewTask());
            var returnTask = (CreatedResult)_toDoController.Insert(taskRequest);
            
            var taskModel = (TaskModel)returnTask.Value;

            returnTask.Location += taskModel.TaskNumber;

            Assert.Equal(returnTask.StatusCode, statusCodeResult);
            Assert.Equal(returnTask.Location, $"/api/ToDo/Task/{taskModel.TaskNumber}");
            Assert.Equal(taskRequest.TaskModel.Title, taskModel.Title);
            Assert.Equal(taskRequest.TaskModel.Description, taskModel.Description);
            Assert.Equal(taskRequest.TaskModel.Progress, taskModel.Progress);
            Assert.Equal(taskRequest.TaskModel.EstimatedDate, taskModel.EstimatedDate);
            Assert.Equal(DateTime.Now.Date, taskModel.CreateDate);

        }

        [Fact(Skip="fix this")]
        public void MustBeDeleteATask()
        {
            //var result = "Sequence contains no elements";
            var statusCodeResult = 204;

            var taskNumber = InsertTaskToTest.InsertAndReturTask(Progress.ToDo).TaskNumber;

            var taskRequest = JsonConvert.DeserializeObject<ToDoModel>(ReturnJsonUpdateTask(taskNumber));
            var returnTask = (NoContentResult)_toDoController.Delete(taskRequest);
            
            //var ex = Assert.Throws<InvalidOperationException>(() => _getTasksUseCase.Get(taskNumber));
            //Assert.Equal(ex.Message, result);
            Assert.Equal(returnTask.StatusCode, statusCodeResult);
        }
        [Fact(Skip="fix this")]
        public void IfStardDateOrEndDateWasFillThenAArgumentExceptionWillBeThrows()
        {
            var result = "When Progress is ToDo cannot record StartDate.\nWhen Progress is ToDo cannot record EndDate.";

            var taskRequest = JsonConvert.DeserializeObject<ToDoModel>(ReturnInvalidJsonInsert());
            var badRequest = (BadRequestObjectResult) _toDoController.Insert(taskRequest); 

            Assert.Equal(badRequest.Value, result);
        }

        #region AuxiliaryMethods
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