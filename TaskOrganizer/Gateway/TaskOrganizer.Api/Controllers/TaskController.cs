using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        private readonly IMapper _mapper;

        public TaskController(IGetTasksUseCase getTasksUseCase, IMapper mapper)
        {           
            _getTasksUseCase = getTasksUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var domainTaskList = _getTasksUseCase.GetAll();

                var taskModelList = _mapper.Map<List<TaskModel>>(domainTaskList);

                return Ok(taskModelList);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}