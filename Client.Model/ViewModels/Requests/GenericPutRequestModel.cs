using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;

namespace Client.Model.ViewModels
{
    public class GenericPutRequestModel<T> : IRequest<IActionResult>
    {
        public T Data { get; set; }
    }   

    public class ClientPutRequestModel : GenericPutRequestModel<AClient> {}    
}