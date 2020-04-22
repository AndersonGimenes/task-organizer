using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Api.Controllers.Commom;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
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
        public IActionResult Insert([FromBody] TaskRequest taskRequest)
        {
            try
            {
                taskRequest.CreateDate = DateTime.Now.Date;
                var id = _registerTaskUseCase.Register(Helper.MapperTaskInToDomainTask(taskRequest));
                var uri = Url.Action("Get", new {taskNumber = id});

                return Created(uri, taskRequest);
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
                _registerTaskUseCase.Register(Helper.MapperTaskInToDomainTask(taskRequest));
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
                _deleteTaskUseCase.Delete(Helper.MapperTaskInToDomainTask(taskRequest));
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
