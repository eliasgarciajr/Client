using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;

namespace Client.Model.ViewModels
{
    public class GenericDeleteRequestModel<T> : IRequest<IActionResult>
    {
        public int Id { get; set; }
    }
    public class ClientDeleteRequestModel : GenericDeleteRequestModel<AClient>{}    
}