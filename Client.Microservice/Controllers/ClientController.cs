using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Client.Model.Data;
using Client.Model.ViewModels;
using System.Threading.Tasks;

namespace Client.Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ACrudController<ClientController,
        ClientListRequestModel,
        ClientSingleRequestModel,
        ClientDeleteRequestModel,
        ClientPostRequestModel,
        ClientPutRequestModel, AClient>
    {
        public ClientController(ILogger<ClientController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

    }
}