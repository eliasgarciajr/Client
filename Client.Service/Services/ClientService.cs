using AutoMapper;
using Client.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Client.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Model.Data;

namespace Client.Services
{
    public interface IClientService
    {
        IActionResult Add(AClient claim);
        IActionResult Delete(int id);
        IActionResult GetById(int id);
        IActionResult Get();
        IActionResult Update(AClient claim);            
    }

    public class ClientService : IClientService
    {
        private readonly ClientDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(ClientDbContext service, IMapper mapper)
        {
            _context = service;
            _mapper = mapper;
        }

        public IActionResult Get()
        {
            var query = _context.Set<AClient>()
                .Include(x => x.Phones)                
                .AsQueryable()
                .AsNoTracking();            

            
            return new OkObjectResult(query.Select(x => _mapper.Map<AddClientRequest>(x)).ToList());
            
        }

        public IActionResult GetById(int id)
        {
            try
            {
                if (id == 0) return new BadRequestObjectResult("Id not informed");
                var obj = _context.Set<AClient>()
                    .Include(x => x.Phones).FirstOrDefault(x => x.Id == id);
                if (obj == null) return new BadRequestObjectResult("Client not found");

                var result = _mapper.Map<AClient>(obj);                

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
       
        public IActionResult Delete(int id)
        {
            try
            {

                if (id == 0) return new BadRequestObjectResult("Id not informed");
                var obj = _context.Set<AClient>()
                .Include(x => x.Phones)                
                .FirstOrDefault(x => x.Id == id);
                if (obj == null) return new BadRequestObjectResult("Client not found");                               

                foreach (var item in obj.Phones)
                {
                    _context.Phones.Remove(item);
                }              
                _context.Clients.Remove(obj);
                _context.SaveChanges();

                return new OkObjectResult("Object removed");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error! " + ex.Message);
            }
        }

        public IActionResult Add(AClient obj)
        {
            try
            {
                //if (!obj.FederalLicense.IsNullOrEmpty()
                //    && _context.Clients.Any(x => x.FederalLicense.Equals(obj.FederalLicense)))
                //    return new BadRequestObjectResult("CNPJ já existente");               

                _context.Attach(obj);
                _context.Entry(obj).State = EntityState.Added;
                _context.SaveChanges();                

                return new OkObjectResult(new { obj });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error! " + ex.Message);
            }
        }

        public IActionResult Update(AClient model)
        {
            try
            {
                if (model.Id == 0) return new BadRequestObjectResult("Id not informed");
                var exists = _context.Clients.Any(x => x.Id == model.Id);
                if (!exists) return new BadRequestObjectResult("Client not found");
                

                _context.Attach(model);
                _context.Entry(model).State = EntityState.Modified;                              
                _context.SaveChanges();

                return GetById(model.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error! " + ex.Message);
            }
        }     
        
    }
}
