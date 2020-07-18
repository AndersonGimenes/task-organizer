using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models.Request;
using TaskOrganizer.Domain.ContractUseCase.Task.InProgress;
using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/task/")]
    public class InProgressController : ControllerBase
    {
        private readonly IInProgressUseCase _inProgressUseCase;
        private readonly IMapper _mapper;

        public InProgressController(IInProgressUseCase inProgressUseCase, IMapper mapper)
        {
            _inProgressUseCase = inProgressUseCase;
            _mapper = mapper;
        }

        [HttpPut(nameof(UpdateTask))]
        public IActionResult UpdateTask([FromBody] InProgressTaskRequest request)
        {
            try
            {
                request.IsValid();

                var domainTask = _mapper.Map<DomainTask>(request);

                _inProgressUseCase.UpdateTask(domainTask);
                
                return Ok();
            }
            catch(Exception ex) when (ex is InvalidOperationException || ex is ArgumentException || ex is DomainException || ex is UseCaseException)
            {
                return BadRequest(ex.Message);
            }
            catch(RegisterNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut(nameof(UpdateProgressTask))]
        public IActionResult UpdateProgressTask([FromBody] InProgressTaskRequest request)
        {
            try
            {
                request.IsValid();

                var domainTask = _mapper.Map<DomainTask>(request);

                _inProgressUseCase.UpdateProgressTask(domainTask);
                
                return Ok();
            }
            catch(Exception ex) when (ex is InvalidOperationException || ex is ArgumentException || ex is DomainException || ex is UseCaseException)
            {
                return BadRequest(ex.Message);
            }
            catch(RegisterNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}