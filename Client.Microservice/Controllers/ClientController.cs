using System;
using System.Threading.Tasks;
using Client.Model.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            var request = new GetAllClientRequest();
            var response = _mediator.Send(request);
            return response;
        }

        [HttpGet("{id}")]
        public Task<IActionResult> Get(int id)
        {
            var request = new GetClientRequest() { Id = id };
            var response = _mediator.Send(request);
            return response;
        }      

        [HttpPost]
        public Task<IActionResult> Post([FromBody] AddClientRequest model)
        {
            var response = _mediator.Send(model);

            return response;
        }

        [HttpPut("{id}")]
        public Task<IActionResult> Put(int id, [FromBody] UpdateClientRequest model)
        {
            model.Id = id;
            var response = _mediator.Send(model);
            return response;
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(int id)
        {
            var request = new DeleteClientRequest() { Id = id };
            var response = _mediator.Send(request);
            return response;
        }        
    }
}
