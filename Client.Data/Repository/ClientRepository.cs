using Microsoft.EntityFrameworkCore;
using Client.Model.Data;
using Client.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data.Repository
{
    public interface IClientRepository : IARepository<AClient>
    {       
        

    }
    
    public class ClientRepository : ARepository<AClient>, IClientRepository
    {
        private readonly ClientDbContext _context;

        public ClientRepository(ClientDbContext context) : base(context)
        {
            _context = context;
        }    
      


    }
}