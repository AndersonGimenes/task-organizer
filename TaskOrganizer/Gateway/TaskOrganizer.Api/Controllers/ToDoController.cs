using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;

        public ToDoController(IGetTasksUseCase getTasksUseCase, IRegisterTaskUseCase registerTaskUseCase, IDeleteTaskUseCase deleteTaskUseCase)
        {           
            _getTasksUseCase = getTasksUseCase;
            _registerTaskUseCase = registerTaskUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(ReturnApiOutList(_getTasksUseCase.GetAll()));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{taskNumber}")]
        public IActionResult Get(int taskNumber)
        {
            try
            {
                return Ok(MapperDomainTaskToTaskOut(_getTasksUseCase.Get(taskNumber)));
            }
            catch
            {
                return NotFound();
            }
        }   

        [HttpPost]
        public IActionResult Insert([FromBody] TaskRequest taskRequest)
        {
            try
            {
                var taskResponse = taskRequest;
                var id = _registerTaskUseCase.Register(MapperTaskInToDomainTask(taskRequest));
                var uri = Url.Action("Get", new {taskNumber = id});

                return Created(uri, taskResponse);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskRequest taskRequest)
        {
            try
            {
                _registerTaskUseCase.Register(MapperTaskInToDomainTask(taskRequest));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] TaskRequest taskRequest)
        {
            try
            {
                _deleteTaskUseCase.Delete(MapperTaskInToDomainTask(taskRequest));
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
        

        #region AuxiliaryMethods

        private IList<TaskResponse> ReturnApiOutList(IList<DomainTask> list)
        {
            var listReturn = new List<TaskResponse>();
            foreach(var item in list)
            {
                listReturn.Add(MapperDomainTaskToTaskOut(item));
            }
            
            return listReturn;
        }

        private TaskResponse MapperDomainTaskToTaskOut(DomainTask domainTask)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<DomainTask, TaskResponse>();}
            );  

            return config.CreateMapper().Map<DomainTask, TaskResponse>(domainTask);      
        }

        private DomainTask MapperTaskInToDomainTask(TaskRequest taskRequest)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<TaskRequest, DomainTask>();}
            );  

            return config.CreateMapper().Map<TaskRequest, DomainTask>(taskRequest);      
        }

        #endregion
    }
}
