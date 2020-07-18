using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Models.Request;
using TaskOrganizer.Domain.ContractUseCase.Task.Done;
using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.UseCase.UseCaseException;

namespace TaskOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]/task")]
    public class DoneController : ControllerBase
    {
        private readonly IDoneUseCase _doneUseCase;
        private readonly IMapper _mapper;

        public DoneController(IDoneUseCase doneUseCase, IMapper mapper)
        {
            _doneUseCase = doneUseCase;
            _mapper = mapper;
        }

        [HttpPut]
        public IActionResult Update([FromBody] DoneTaskRequest request)
        {
            try
            {
                request.IsValid();

                var domainTask = _mapper.Map<DomainTask>(request);

                _doneUseCase.UpdateProgressTask(domainTask);

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