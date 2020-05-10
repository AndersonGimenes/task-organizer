using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers.Commom;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.DomainException;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]/task")]
    public class DoneController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;

        public DoneController(IGetTasksUseCase getTasksUseCase, IRegisterTaskUseCase registerTaskUseCase)
        {
            _getTasksUseCase = getTasksUseCase;
            _registerTaskUseCase = registerTaskUseCase;
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

        [HttpPut]
        public IActionResult Update([FromBody] TaskModel taskModel)
        {
            try
            {
                _registerTaskUseCase.Register(Helper.MapperTaskModelToDomainTask(taskModel));
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
    }
}