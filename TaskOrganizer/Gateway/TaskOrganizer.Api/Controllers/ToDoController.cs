using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
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
        public IActionResult CreateTask([FromBody] ToDoModel toDoModel)
        {
            try
            {
                toDoModel.IsValid();

                var domainTask = _mapper.Map<DomainTask>(toDoModel);

                toDoModel = _mapper.Map<ToDoModel>(_toDoCreateTaskUseCase.CreateNewTask(domainTask));

                return Created(string.Empty, toDoModel);
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
        public IActionResult UpdateTask([FromBody] ToDoModel toDoModel)
        {
            try
            {
                toDoModel.IsValid();

                var domainTask = _mapper.Map<DomainTask>(toDoModel);

                _toDoUpdateTaskUseCase.UpdateTask(domainTask);

                return Ok();
            }
            catch(Exception ex) when (ex is InvalidOperationException || ex is ArgumentException || ex is DomainException || ex is UseCaseException)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex) when (ex is RegisterNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTask([FromBody] ToDoModel toDoModel)
        {
            try
            {
                var domainTask = _mapper.Map<DomainTask>(toDoModel);

                _toDoDeleteTaskUseCase.Delete(domainTask);
                
                return NoContent();
            }
            catch(Exception ex) when (ex is RegisterNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }
     
    }
}