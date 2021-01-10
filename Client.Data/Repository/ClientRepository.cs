using Microsoft.EntityFrameworkCore;
using Client.Model.Data;
using Client.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data.Repository
{
    public interface IClientRepository : IARepository<AClient>    {

        Task<List<AClient>> GetClient();
        Task<AClient> GetClientById(int id);
    }
    
    public class ClientRepository : ARepository<AClient>, IClientRepository
    {
        private readonly ClientDbContext _context;

        public ClientRepository(ClientDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<AClient>> GetClient()
        {
            var list = await DbContext.Set<AClient>()
                .Include(a => a.Phones)
                .AsNoTracking()                
                .ToListAsync();

            return list;
        }

        public async Task<AClient> GetClientById(int id)
        {
            var list = DbContext.Set<AClient>()
                .Include(a => a.Phones)
                .AsNoTracking()
                .Where(c => c.Id == id).FirstOrDefault();                                

            return list;
        }
    }
}