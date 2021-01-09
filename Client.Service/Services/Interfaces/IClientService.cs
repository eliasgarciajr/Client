using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Model.Data;
using Client.Model.ViewModels;

namespace Client.Service.Services
{
    public interface IClientService
    {
        Task<IActionResult> GetAll(ClientListRequestModel request);
        Task<IActionResult> GetOne(GenericSingleRequestModel<AClient> request);
        Task<IActionResult> Delete(GenericDeleteRequestModel<AClient> request);
        Task<IActionResult> Post(ClientPostRequestModel request);
        Task<IActionResult> Put(ClientPutRequestModel request);
    }
}