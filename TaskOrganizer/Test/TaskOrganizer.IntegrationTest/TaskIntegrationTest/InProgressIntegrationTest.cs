using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
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
            var statusCodeResult = 200;

            var taskNumber = InsertTaskToTest.InsertAndReturTask("ToDo").TaskNumber;

            var taskRequest = Json.JsonDeserialize(ReturnJsonUpdateTask(taskNumber));
            OkResult returnTask = (OkResult)_inProgressController.Update(taskRequest);
            
            var taskReturn = _getTasksUseCase.Get(taskNumber);
            
            Assert.Equal(returnTask.StatusCode, statusCodeResult);
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
            var taskModel = (TaskModel)returnTask.Value;

            Assert.Equal(taskModel.Title, task.Title);
            Assert.Equal(taskModel.Description, task.Description);
            Assert.Equal(taskModel.CreateDate, task.CreateDate);
            Assert.Equal(taskModel.EstimatedDate, task.EstimatedDate);
        }

        [Fact]
        public void IfEndDateWasFillThenAArgumentExceptionWillBeThrows()
        {
            var result = "When Progress is ToDo cannot record EndDate.";

            var taskRequest = Json.JsonDeserialize(ReturnInvalidJsonUpdate());
            var badRequest = (BadRequestObjectResult) _inProgressController.Update(taskRequest); 

            Assert.Equal(badRequest.Value,result);
        }

        #region AuxiliaryMethods
              
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