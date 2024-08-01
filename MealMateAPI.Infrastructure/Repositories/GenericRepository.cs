using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Entities.Abstracts;
using MenuMasterAPI.Domain.Repositories;
using MenuMasterAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MenuMasterAPI.Infrastructure.Repositories;

public class GenericRepository<TEntity,TContract> : IGenericRepository<TEntity,TContract> where TEntity : class, IBaseEntity
    where TContract: class,IBaseContract
{
    private readonly MealMateAPIDbContext _context;
    private readonly DbSet<TEntity> Table;
    public GenericRepository(MealMateAPIDbContext context)
    {
        _context = context;
        Table = context.Set<TEntity>();
    }

    #region Read Methods

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        var getQuery = Table ?? throw new ArgumentNullException(typeof(IQueryable<TEntity>).ToString());
        return await Task.FromResult(getQuery);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id) ?? throw new ArgumentNullException(typeof(TEntity).ToString());
    }
    public async Task<PagedResponse<TEntity>> GetPagedDataAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await Table.AsNoTracking().CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        if (pageNumber > totalPages)
        {
            pageNumber = totalPages;
        }

        var entities = await Table.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedResponse<TEntity>(pageSize, pageNumber, totalRecords, totalPages, entities);

    }

    #endregion

    #region Write Methods 

    public async Task<bool> AddAsync(TEntity model)
    {
        EntityEntry<TEntity> entityEntry = Table.Add(model);
        
        try
        {
            await SaveAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<TEntity> datas)
    {
        await Table.AddRangeAsync(datas);
        await SaveAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(TContract model)
    {
        var existEntity = await Table.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (existEntity is IAuditable)
        {
            ((IAuditable)existEntity).UpdatedOn = DateTime.UtcNow;

        }
        _context.Entry(existEntity).CurrentValues.SetValues(model);
    
        await _context.SaveChangesAsync();
        

        return true;
    }

    public async Task<bool> RemoveAsync(TEntity model)
    {
        if (model is ISoftDelete)
        {
            ((ISoftDelete)model).IsDeleted = true;
            model.GetType().GetProperty("DeletedOn").SetValue(model, DateTime.UtcNow);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            EntityEntry<TEntity> entityEntry = Table.Remove(model);
            await SaveAsync();
            return entityEntry.State == EntityState.Deleted;
        }
    }

    public async Task<bool> RemoveRangeAsync(List<TEntity> datas)
    {
        foreach (var item in datas)
        {
            await RemoveAsync(item);
        }
        return true;
    }

    public async Task<bool> RemoveByIdAsync(int id)
    {
        var model = await Table.FirstOrDefaultAsync(data => data.Id == id);
        if (model == null)
        {
            throw new ArgumentNullException(typeof(TEntity).ToString());
        }
        return await RemoveAsync(model);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}

