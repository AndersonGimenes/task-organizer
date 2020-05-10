using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Controllers.Commom;
using TaskOrganizer.Domain.ContractUseCase;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IGetTasksUseCase _getTasksUseCase;
        public TaskController(IGetTasksUseCase getTasksUseCase)
        {           
            _getTasksUseCase = getTasksUseCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(Helper.ReturnTaskModelList(_getTasksUseCase.GetAll()));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}