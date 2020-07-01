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
        private readonly IToDoUseCase _toDoUseCase;
        private readonly IMapper _mapper;

        public ToDoController(IToDoUseCase toDoUseCase, IMapper mapper)
        {           
            _toDoUseCase = toDoUseCase;
            _mapper = mapper;

        }
    
        [HttpPost]
        public IActionResult Insert([FromBody] ToDoModel toDoModel)
        {
            try
            {
                toDoModel.IsValid();

                var domainTask = _mapper.Map<DomainTask>(toDoModel);

                toDoModel = _mapper.Map<ToDoModel>(_toDoUseCase.InsertNewTask(domainTask));

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
        public IActionResult Update([FromBody] ToDoModel toDoModel)
        {
            try
            {
                toDoModel.IsValid();

                var domainTask = _mapper.Map<DomainTask>(toDoModel);

                _toDoUseCase.UpdateTask(domainTask);

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
        public IActionResult Delete([FromBody] ToDoModel toDoModel)
        {
            try
            {
                var domainTask = _mapper.Map<DomainTask>(toDoModel);

                _toDoUseCase.Delete(domainTask);
                
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }
     
    }
}