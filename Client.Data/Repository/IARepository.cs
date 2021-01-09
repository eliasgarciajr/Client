using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Client.Data.Repository
{
    public interface IARepository<TEntity> where TEntity : class
    {
        Microsoft.EntityFrameworkCore.DbContext DbContext { get; }
        void Delete(params TEntity[] pObjects);
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> pFilter = null);
        TEntity Find(Expression<Func<TEntity, bool>> keys = null);
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryable<TEntity> Queryable();
        Task<List<TEntity>> ToListAnsyc();
        IQueryable<TEntity> QueryableDetach();
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> pFilter = null);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}