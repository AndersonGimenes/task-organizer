using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers.Commom;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.DomainException;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;

        public TaskController(IGetTasksUseCase getTasksUseCase, IRegisterTaskUseCase registerTaskUseCase, IDeleteTaskUseCase deleteTaskUseCase)
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
                return Ok(Helper.ReturnTaskModelList(_getTasksUseCase.GetAll()));
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
                return Ok(Helper.MapperDomainTaskToTaskModel(_getTasksUseCase.Get(taskNumber)));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }   
    
        [HttpPost]
        public IActionResult Insert([FromBody] TaskModel taskModel)
        {
            try
            {
                var domainTask = _registerTaskUseCase.Register(Helper.MapperTaskModelToDomainTask(taskModel));
                taskModel = Helper.MapperDomainTaskToTaskModel(domainTask);

                var uri = Url.Action("Get", new {taskNumber = taskModel.TaskNumeber});

                return Created(uri, taskModel);
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskModel taskModel)
        {
            try
            {
                _registerTaskUseCase.Register(Helper.MapperTaskModelToDomainTask(taskModel));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
           
        }
    
        [HttpDelete]
        public IActionResult Delete([FromBody] TaskModel taskModel)
        {
            try
            {
                _deleteTaskUseCase.Delete(Helper.MapperTaskModelToDomainTask(taskModel));
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
        
    }
}