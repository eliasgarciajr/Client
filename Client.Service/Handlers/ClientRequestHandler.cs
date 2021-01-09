using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;
using Client.Model.ViewModels;

namespace Client.Services
{
    public class ClientRequestHandler :
        IRequestHandler<AddClientRequest, IActionResult>,
        IRequestHandler<DeleteClientRequest, IActionResult>,
        IRequestHandler<GetClientRequest, IActionResult>,
        IRequestHandler<GetAllClientRequest, IActionResult>,
        IRequestHandler<UpdateClientRequest, IActionResult>
    {
        private readonly IMapper _mapper;
        private readonly IClientService _service;

        public ClientRequestHandler(IClientService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public Task<IActionResult> Handle(DeleteClientRequest model, CancellationToken cancellation)
        {
            return Task.FromResult(_service.Delete(model.Id));
        }

        public Task<IActionResult> Handle(GetAllClientRequest model, CancellationToken cancellation)
        {
            return Task.FromResult(_service.Get());
        }

        public Task<IActionResult> Handle(GetClientRequest model, CancellationToken cancellation)
        {
            return Task.FromResult(_service.GetById(model.Id));
        }

        public Task<IActionResult> Handle(AddClientRequest model, CancellationToken cancellation)
        {           

            var obj = _mapper.Map<AClient>(model);

            return Task.FromResult(_service.Add(obj));
        }

        public Task<IActionResult> Handle(UpdateClientRequest model, CancellationToken cancellation)
        {         
            var obj = _mapper.Map<AClient>(model);
            return Task.FromResult(_service.Update(obj));
        }        
    }
}