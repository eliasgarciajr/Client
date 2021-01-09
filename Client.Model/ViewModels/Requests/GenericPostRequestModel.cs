using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;

namespace Client.Model.ViewModels
{
    public class GenericPostRequestModel<T> : IRequest<IActionResult>
    {
        public T Data { get; set; }
    }
    
    public class ClientPostRequestModel : GenericPostRequestModel<AClient>{}    
}