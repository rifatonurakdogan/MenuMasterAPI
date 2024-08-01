using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Entities.Abstracts;
using System.Linq.Expressions;

namespace MenuMasterAPI.Domain.Repositories
{
    public interface IGenericRepository<TEntity,TContract> where TEntity : class, IBaseEntity
        where TContract: class, IBaseContract
    {
        Task<bool> AddAsync(TEntity model);
        Task<bool> AddRangeAsync(List<TEntity> datas);
        Task<bool> RemoveAsync(TEntity model);
        Task<bool> RemoveRangeAsync(List<TEntity> datas);
        Task<bool> RemoveByIdAsync(int id);
        Task<bool> UpdateAsync(TContract model);
        Task SaveAsync();
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<PagedResponse<TEntity>> GetPagedDataAsync(int pageNumber, int pageSize);
    }
}