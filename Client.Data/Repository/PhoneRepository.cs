using Microsoft.EntityFrameworkCore;
using Client.Model.Data;
using Client.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data.Repository
{
    public interface IPhoneRepository : IARepository<Phone>    {

        Task<List<Phone>> GetClientById(int id);                
        Task<IList<Phone>> AddOrUpdatePhone(IList<Phone> model, int clientId);
        Task<IList<Phone>> DeletePhones(int clientId);
    }
    
    public class PhoneRepository : ARepository<Phone>, IPhoneRepository
    {
        private readonly ClientDbContext _context;

        public PhoneRepository(ClientDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Phone>> GetClientById(int id)
        {
            var list = await DbContext.Set<Phone>()                
                .AsNoTracking()
                .Where(c => c.ClientId == id).ToListAsync();                                
            
            return list;
        }

        public async Task<IList<Phone>> AddOrUpdatePhone(IList<Phone> model, int clientId)
        {
            var current = model.Select(c => c.Id).ToList();

            var list = DbContext.Set<Phone>()
                .AsNoTracking()
                .Where(c => c.ClientId == clientId).ToList();

            list.ForEach(i =>
            {
                _context.Entry(i).State = EntityState.Deleted;
            });

            _context.SaveChanges();

            foreach (var item in model)
            {
                if (item.Id == 0)
                    item.Id = 0;

                var action = EntityState.Added;

                if (DbContext.Set<Phone>().AsNoTracking().Any(c => c.Id == item.Id))
                    action = EntityState.Modified;

                _context.Entry(item).State = action;
                _context.SaveChanges();
            }

            return list;
        }


        public async Task<IList<Phone>> DeletePhones(int clientId)
        {            

            var list = DbContext.Set<Phone>()
                .AsNoTracking()
                .Where(c => c.ClientId == clientId).ToList();

            list.ForEach(i =>
            {
                _context.Entry(i).State = EntityState.Deleted;
            });

            _context.SaveChanges();            

            return list;
        }

    }

}