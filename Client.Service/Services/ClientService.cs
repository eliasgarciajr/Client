using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Data.Repository;
using Client.Model.Data;
using Client.Model.ViewModels;
using ClientModel = Client.Model.Data.AClient;
namespace Client.Service.Services
{
    public class ClientService : IClientService
    {
        //private IARepository<ClientModel> _repository;

        private IClientRepository _repository;
        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> GetAll(ClientListRequestModel request)
        {
            var lst = await _repository.GetClient();         

            return new OkObjectResult(lst.ToList());
        }

        public async Task<IActionResult> GetOne(GenericSingleRequestModel<ClientModel> request)
        {          

            var result = await _repository.GetClientById(request.Id);

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> Delete(GenericDeleteRequestModel<ClientModel> request)
        {
            _repository.Delete(request.Id);
            return new NoContentResult();
        }

        public async Task<IActionResult> Post(ClientPostRequestModel request)
        {
            //var model = new GatheringModel();
            _repository.Insert(request.Data);
            return new OkObjectResult(request.Data);
        }

        public async Task<IActionResult> Put(ClientPutRequestModel request)
        {
            //var model = new GatheringModel();
            _repository.Update(request.Data);
            return new OkObjectResult(request.Data);
        }
       
    }
}