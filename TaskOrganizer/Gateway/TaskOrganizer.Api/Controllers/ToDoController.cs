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
    [Route("api/[controller]/task/")]
    public class ToDoController : ControllerBase
    {
        
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IRegisterTaskUseCase _registerTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly IMapper _mapper;

        public ToDoController(IGetTasksUseCase getTasksUseCase, IRegisterTaskUseCase registerTaskUseCase, IDeleteTaskUseCase deleteTaskUseCase, IMapper mapper)
        {           
            _getTasksUseCase = getTasksUseCase;
            _registerTaskUseCase = registerTaskUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
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
    
        [HttpPost]
        public IActionResult Insert([FromBody] TaskModel taskModel)
        {
            try
            {
                taskModel.IsValid();

                var domainTask = _mapper.Map<DomainTask>(taskModel);

                taskModel = _mapper.Map<TaskModel>(_registerTaskUseCase.Register(domainTask));

                var uri = Url.Action("Get", new {taskNumber = taskModel.TaskNumber});

                return Created(uri, taskModel);
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

        [HttpPut]
        public IActionResult Update([FromBody] TaskModel taskModel)
        {
            try
            {
                taskModel.IsValid();

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

        [HttpDelete]
        public IActionResult Delete([FromBody] TaskModel taskModel)
        {
            try
            {
                var domainTask = _mapper.Map<DomainTask>(taskModel);

                _deleteTaskUseCase.Delete(domainTask);
                
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }
     
    }
}