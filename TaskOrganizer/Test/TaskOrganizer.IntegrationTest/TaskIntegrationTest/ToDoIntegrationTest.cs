using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskOrganizer.Api.Controllers;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
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
        private readonly IToDoCreateTaskUseCase _toDoCreateTaskUseCase;
        private readonly IToDoUpdateTaskUseCase _toDoUpdateTaskUseCase;
        private readonly IToDoDeleteTaskUseCase _toDoDeleteTaskUseCase;
        private readonly ToDoController _toDoController;
        private readonly Mock<IUrlHelper> _urlHelperMock;

        public ToDoIntegrationTest()
        {
            _context = DataBaseInMemory.ReturnContext();
            _taskReadOnlyRepository = new TaskReadOnlyRepository(_context);
            _taskWriteDeleteOnlyRepository = new TaskWriteDeleteOnlyRepository(_context); 

            _mapper = CreateMapper.CreateMapperProfile();
            
            _toDoCreateTaskUseCase = new ToDoCreateTaskUseCase(_taskWriteDeleteOnlyRepository);
            _toDoUpdateTaskUseCase = new ToDoUpdateTaskUseCase(_taskReadOnlyRepository, _taskWriteDeleteOnlyRepository);
            _toDoDeleteTaskUseCase = new ToDoDeleteTaskUseCase(_taskReadOnlyRepository, _taskWriteDeleteOnlyRepository);

            _toDoController = new ToDoController(_toDoCreateTaskUseCase, _toDoUpdateTaskUseCase, _toDoDeleteTaskUseCase, _mapper);
            
            _urlHelperMock = new Mock<IUrlHelper>();
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