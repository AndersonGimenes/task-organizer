using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private IRegisterTaskUseCase _registerTaskUseCase;
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
                return Ok(ReturnApiOutList(_getTasksUseCase.GetAll()));
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
                return Ok(MapperDomainTaskToTaskOut(_getTasksUseCase.Get(taskNumber)));
            }
            catch
            {
                return NotFound();
            }
        }   

        [HttpPost]
        public IActionResult Insert([FromBody] TaskIn taskIn)
        {
            try
            {
                _registerTaskUseCase.Register(MapperTaskInToDomainTask(taskIn));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskIn taskIn)
        {
            try
            {
                _registerTaskUseCase.Register(MapperTaskInToDomainTask(taskIn));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] TaskIn taskIn)
        {
            try
            {
                _deleteTaskUseCase.Delete(MapperTaskInToDomainTask(taskIn));
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
        

        #region AuxiliaryMethods

        private IList<TaskOut> ReturnApiOutList(IList<DomainTask> list)
        {
            var listReturn = new List<TaskOut>();
            foreach(var item in list)
            {
                listReturn.Add(MapperDomainTaskToTaskOut(item));
            }
            
            return listReturn;
        }

        private TaskOut MapperDomainTaskToTaskOut(DomainTask domainTask)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<DomainTask, TaskOut>();}
            );  

            return config.CreateMapper().Map<DomainTask, TaskOut>(domainTask);      
        }

        private DomainTask MapperTaskInToDomainTask(TaskIn taskIn)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<TaskIn, DomainTask>();}
            );  

            return config.CreateMapper().Map<TaskIn, DomainTask>(taskIn);      
        }

        #endregion
    }
}
