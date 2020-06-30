using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.ContractUseCase.Task;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskUseCase _taskUseCase;
        private readonly IMapper _mapper;

        public TaskController(ITaskUseCase taskUseCase, IMapper mapper)
        {           
            _taskUseCase = taskUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var domainTasks = _taskUseCase.GetAll();

                var taskModelList = _mapper.Map<List<TaskModel>>(domainTasks);

                return Ok(taskModelList);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{taskNumber}")]
        public IActionResult Get(int taskNumber)
        {
            try
            {
                var domainTask = _taskUseCase.Get(taskNumber);

                var taskModel = _mapper.Map<TaskModel>(domainTask);

                return Ok(taskModel);

            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}