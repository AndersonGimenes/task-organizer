using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers.Commom;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;

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
                return Ok(Helper.ReturnApiOutList(_getTasksUseCase.GetAll()));
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
                return Ok(Helper.MapperDomainTaskToTaskOut(_getTasksUseCase.Get(taskNumber)));
            }
            catch
            {
                return NotFound();
            }
        }   
    
        [HttpPost]
        public IActionResult Insert([FromBody] TaskModel taskModel)
        {
            try
            {
                taskModel.CreateDate = DateTime.Now.Date;
                taskModel.TaskNumeber = _registerTaskUseCase.Register(Helper.MapperTaskInToDomainTask(taskModel));
                var uri = Url.Action("Get", new {taskNumber = taskModel.TaskNumeber});

                return Created(uri, taskModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskModel taskModel)
        {
            try
            {
                _registerTaskUseCase.Register(Helper.MapperTaskInToDomainTask(taskModel));
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
                _deleteTaskUseCase.Delete(Helper.MapperTaskInToDomainTask(taskModel));
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
        
    }
}