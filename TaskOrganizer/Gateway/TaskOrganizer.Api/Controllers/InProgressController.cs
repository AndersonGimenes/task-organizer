using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers.Commom;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.Api.Controllers
{
    public class InProgressController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;

        public InProgressController(IGetTasksUseCase getTasksUseCase, IRegisterTaskUseCase registerTaskUseCase)
        {
            _getTasksUseCase = getTasksUseCase;
            _registerTaskUseCase = registerTaskUseCase;
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
    }
}