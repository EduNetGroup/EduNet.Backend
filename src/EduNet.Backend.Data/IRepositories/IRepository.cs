using System.Linq.Expressions;
using EduNet.Backend.Domain.Commons;

namespace EduNet.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    public Task<bool> DeleteAsync(long id);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<TEntity> InsertAsync(TEntity entity);
    public Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
}
