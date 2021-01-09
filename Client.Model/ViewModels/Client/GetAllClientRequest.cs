using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.Model.ViewModels
{
    public class GetAllClientRequest : IRequest<IActionResult>
    {
    }
}