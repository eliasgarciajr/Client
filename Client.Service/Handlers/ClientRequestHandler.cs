using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;
using Client.Model.ViewModels;
using Client.Service.Services;

namespace Client.Service.Handlers
{
    public class ClientRequestHandler : IRequestHandler<ClientListRequestModel, IActionResult>,
        IRequestHandler<ClientSingleRequestModel, IActionResult>,
        IRequestHandler<ClientDeleteRequestModel, IActionResult>,
        IRequestHandler<ClientPostRequestModel, IActionResult>,
        IRequestHandler<ClientPutRequestModel, IActionResult>        
    {
        private IClientService _service;

        public ClientRequestHandler(IClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Handle(ClientListRequestModel request, CancellationToken cancellationToken)
        {
            return await _service.GetAll(request);
        }

        public async Task<IActionResult> Handle(ClientSingleRequestModel request, CancellationToken cancellationToken)
        {
            return await _service.GetOne(request);
        }

        public async Task<IActionResult> Handle(ClientDeleteRequestModel request, CancellationToken cancellationToken)
        {
            return await _service.Delete(request);
        }

        public async Task<IActionResult> Handle(ClientPostRequestModel request, CancellationToken cancellationToken)
        {
            return await _service.Post(request);
        }

        public async Task<IActionResult> Handle(ClientPutRequestModel request, CancellationToken cancellationToken)
        {
            return await _service.Put(request);
        }         
        
       
    }
}