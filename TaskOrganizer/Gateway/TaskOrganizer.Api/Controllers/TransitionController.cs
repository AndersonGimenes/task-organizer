using System;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers.Commom;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.Api.Controllers
{
    public class TransitionController : ControllerBase
    {
        private readonly IRegisterTaskUseCase _registerTaskUseCase;

        public TransitionController(IRegisterTaskUseCase registerTaskUseCase)
        {
            _registerTaskUseCase = registerTaskUseCase;
        }

        [HttpPut]
        public IActionResult ToDoForWipUpdate([FromBody] TaskRequest taskRequest)
        {
            try
            {
                taskRequest.StartDate = DateTime.Now.Date;
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