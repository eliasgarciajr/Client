using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;

namespace Client.Model.ViewModels
{
    public class GenericSingleRequestModel<T> : IRequest<IActionResult>
    {
        public int Id { get; set; }
    }  
    public class ClientSingleRequestModel : GenericSingleRequestModel<AClient>{}    
}