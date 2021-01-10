#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Client.Model.Data;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Client.Data.Repository
{
	public abstract class ARepository<TEntity> : IARepository<TEntity> where TEntity : class
	{
		public ARepository(Microsoft.EntityFrameworkCore.DbContext context)
		{
			_context = context;
			if (_context != null)
			{
				_dbSet = _context.Set<TEntity>();
			}
        }

        public Microsoft.EntityFrameworkCore.DbContext DbContext
        {
            get
            {
	            return _context;
            }
        }

		//public void Delete(params TEntity[] pObjects)
		//{
		//	pObjects?.ForEach(Delete);
		//}

		public TEntity GetById(object id)
		{
			return _dbSet.Find(id);
		}

		public IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> pFilter = null)
		{
			return _dbSet.Where(pFilter).ToList();
		}

		public TEntity Find(Expression<Func<TEntity, bool>> keys = null)
		{
			return _dbSet.FirstOrDefault(keys);
		}
		public virtual TEntity Find(params object[] keyValues)
		{
			return _dbSet.Find(keyValues);
		}

		public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
		{
			return _context.Set<TEntity>().FromSqlRaw(query, parameters).AsQueryable();
		}

		public virtual void Insert(TEntity entity)
		{
			_context.Add(entity);
			_context.Entry(entity).State = EntityState.Added;
			_context.SaveChanges();
		}

		public virtual void InsertRange(IEnumerable<TEntity> entities)
		{
			InsertGraphRange(entities);
		}

		public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
		{
			_dbSet.AddRange(entities);
		}

		public virtual void Update(TEntity entity)
		{
			_context.Add(entity);
			_context.Entry(entity).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public virtual void Delete(object id)
		{
			var entity = _dbSet.Find(id);
			Delete(entity);
		}

		public virtual void Delete(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Deleted;
			_context.SaveChanges();
		}


		public IQueryable<TEntity> Queryable()
		{
			return _dbSet;
		}

		public async Task<List<TEntity>> ToListAnsyc()
		{
			return await _dbSet.ToListAsync();
		}

		public IQueryable<TEntity> QueryableDetach()
		{
			return _dbSet.AsNoTracking().AsQueryable();
		}

		public virtual async Task<TEntity> FindAsync(params object[] keyValues)
		{
			return await _dbSet.FindAsync(keyValues);
		}

		public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> pFilter = null)
		{
			return await _dbSet.Where(pFilter).ToListAsync();
		}

		public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			return await _dbSet.FindAsync(cancellationToken, keyValues);
		}

        public void Delete(params TEntity[] pObjects)
        {
            throw new NotImplementedException();
        }

        #region Private Fields
        private Microsoft.EntityFrameworkCore.DbContext _context;
		private DbSet<TEntity> _dbSet;
		#endregion Private Fields
		
	}
}