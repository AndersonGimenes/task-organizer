using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models.Request;
using TaskOrganizer.Api.Models.Response;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/task/")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoCreateTaskUseCase _toDoCreateTaskUseCase;
        private readonly IToDoUpdateTaskUseCase _toDoUpdateTaskUseCase;
        private readonly IToDoDeleteTaskUseCase _toDoDeleteTaskUseCase;
        private readonly IMapper _mapper;

        public ToDoController(IToDoCreateTaskUseCase toDoCreateTaskUseCase, IToDoUpdateTaskUseCase toDoUpdateTaskUseCase, IToDoDeleteTaskUseCase toDoDeleteTaskUseCase, IMapper mapper)
        {           
            _toDoCreateTaskUseCase = toDoCreateTaskUseCase;
            _toDoUpdateTaskUseCase = toDoUpdateTaskUseCase;
            _toDoDeleteTaskUseCase = toDoDeleteTaskUseCase;
            _mapper = mapper;

        }
    
        [HttpPost]
        public IActionResult CreateTask([FromBody] ToDoTaskRequest request)
        {
            try
            {
                request.IsValid();

                var domainTask = _mapper.Map<DomainTask>(request);

                var response = _mapper.Map<ToDoTaskResponse>(_toDoCreateTaskUseCase.CreateNewTask(domainTask));

                return Created(string.Empty, response);
            }
            catch(Exception ex) when (ex is InvalidOperationException || ex is ArgumentException || ex is DomainException || ex is UseCaseException)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateTask([FromBody] ToDoTaskRequest request)
        {
            try
            {
                request.IsValid();

                var domainTask = _mapper.Map<DomainTask>(request);

                _toDoUpdateTaskUseCase.UpdateTask(domainTask);

                return Ok();
            }
            catch(Exception ex) when (ex is InvalidOperationException || ex is ArgumentException || ex is DomainException || ex is UseCaseException)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTask([FromBody] ToDoTaskRequest request)
        {
            try
            {
                var domainTask = _mapper.Map<DomainTask>(request);

                _toDoDeleteTaskUseCase.Delete(domainTask);
                
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }
     
    }
}