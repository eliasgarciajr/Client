using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Client.Model.ViewModels
{
    public class DeleteClientRequest : IRequest<IActionResult>
    {
        public int Id { get; set; }
    }
}