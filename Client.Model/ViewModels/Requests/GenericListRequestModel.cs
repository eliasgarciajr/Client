using MediatR;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;

namespace Client.Model.ViewModels
{
    public class GenericListRequestModel : IRequest<IActionResult>
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
        public string Search { get; set; } = string.Empty;
    }    
    public class ClientListRequestModel : GenericListRequestModel{}
}