using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Data.Repository;
using Client.Model.Data;
using Client.Model.ViewModels;
using ClientModel = Client.Model.Data.AClient;
using System;
using Client.Model.ValueObjects;
using System.Security.Cryptography;

namespace Client.Service.Services
{
    public class ClientService : IClientService
    {
        //private IARepository<ClientModel> _repository;

        private IClientRepository _repository;
        private IPhoneRepository _phoneRepository;
        public ClientService(IClientRepository repository, IPhoneRepository phoneRepository)
        {
            _repository = repository;
            _phoneRepository = phoneRepository;
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
            var resultPhone = _phoneRepository.DeletePhones(request.Id);

            _repository.Delete(request.Id);

            return new NoContentResult();
        }

        public async Task<IActionResult> Post(ClientPostRequestModel request)
        {
            if (!Utility.IsValidEmail(request.Data.Email)) return new BadRequestObjectResult("Email inválido");

            if (request.Data.DateBirth > DateTime.Now) return new BadRequestObjectResult("Data de aniversário não pode ser igual ou maior que hoje!");

            var hash = new Hash(SHA512.Create());

            request.Data.Password = hash.Encrypt(request.Data.Password);

            _repository.Insert(request.Data);
            return new OkObjectResult(request.Data);
        }

        public async Task<IActionResult> Put(ClientPutRequestModel request)
        {

            try
            {                
                if (!Utility.IsValidEmail(request.Data.Email)) return new BadRequestObjectResult("Email inválido");

                if (request.Data.DateBirth > DateTime.Now) return new BadRequestObjectResult("Data de aniversário não pode ser igual ou maior que hoje!");

                var hash = new Hash(SHA512.Create());

                request.Data.Password = hash.Encrypt(request.Data.Password);

                var result = _repository.UpdateClient(request.Data);

                var resultPhone = _phoneRepository.AddOrUpdatePhone(request.Data.Phones, request.Data.Id);

                return new OkObjectResult(result.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error! " + ex.Message);
            }

            
        }
       
    }
}