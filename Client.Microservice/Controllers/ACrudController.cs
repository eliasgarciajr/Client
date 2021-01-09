using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Client.Model.ViewModels;

namespace Client.Microservice.Controllers
{
    public abstract class ACrudController<T, TA, TO, TD, TP, TU, TR> : ControllerBase
        where T : ControllerBase
        where TA : GenericListRequestModel
        where TO : IRequest<IActionResult>
        where TD : IRequest<IActionResult>
        where TP : IRequest<IActionResult>
        where TU : IRequest<IActionResult>
        where TR : class
    {
        protected readonly ILogger<T> _logger;
        protected IMediator _mediator;

        public ACrudController(ILogger<T> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll([FromQuery] TA request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetOne([FromRoute] int id)
        {
            TO request = (TO) Activator.CreateInstance(typeof(TO));
            request.GetType().GetProperty("Id").SetValue(request, id);
            return await _mediator.Send(request);
        }
        
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] int id)
        {
            TD request = (TD) Activator.CreateInstance(typeof(TD));
            request.GetType().GetProperty("Id").SetValue(request, id);
            return await _mediator.Send(request);
        }       


        [HttpPost("")]
        public virtual async Task<IActionResult> Post([FromBody] TP request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] TU request)
        {
            var oid = request.GetType().GetProperty("Id")?.GetValue(request)?.ToString();
            if (id.ToString().Equals(oid))
                return await _mediator.Send(request);
            return BadRequest("Dados inconsistentes");
        }        
        
    }
}