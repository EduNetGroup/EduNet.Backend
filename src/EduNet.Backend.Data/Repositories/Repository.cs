using EduNet.Data.DbContexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Data.IRepositories;

namespace EduNet.Backend.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly AppDbContext _dbContext;
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entities = _dbSet.Where(expression);
        if(entities.Any())
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var model = (await _dbSet.AddAsync(entity)).Entity;
        await _dbContext.SaveChangesAsync();
        return model;
    }
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null)
    {
        var query = expression is null ? _dbSet : _dbSet.Where(expression);
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync();

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var model = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();

        return model.Entity;
    }
}
