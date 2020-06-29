using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]/task")]
    public class DoneController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IMapper _mapper;

        public DoneController(IGetTasksUseCase getTasksUseCase, IRegisterTaskUseCase registerTaskUseCase, IMapper mapper)
        {
            _getTasksUseCase = getTasksUseCase;
            _registerTaskUseCase = registerTaskUseCase;
            _mapper = mapper;
        }

        [HttpGet("{taskNumber}")]
        public IActionResult Get(int taskNumber)
        {
            try
            {
                var domainTask = _getTasksUseCase.Get(taskNumber);

                var taskModel = _mapper.Map<TaskModel>(domainTask);
                
                return Ok(taskModel);
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
                var domainTask = _mapper.Map<DomainTask>(taskModel);

                _registerTaskUseCase.Register(domainTask);

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