using AutoMapper;
using MenuMasterAPI.Application.Interfaces;
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Entities.Abstracts;
using MenuMasterAPI.Domain.Repositories;

namespace MenuMasterAPI.Application.Services
{
    public class BaseService<TEntity, TContract> : IBaseService<TEntity, TContract>
    where TContract : class, IBaseContract
    where TEntity : class, IBaseEntity
    {
        private readonly IGenericRepository<TEntity,TContract> _repository;
        private readonly IMapper _mapper;

        public BaseService(IGenericRepository<TEntity,TContract> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(TContract model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<TContract> datas)
        {
            var list = _mapper.Map<List<TEntity>>(datas);
            await _repository.AddRangeAsync(list);
        }

        public async Task<List<TContract>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<List<TContract>>(list);
        }

        public async Task<TContract> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<TContract>(result);
        }

        public async Task RemoveAsync(TContract model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.RemoveAsync(entity);
        }

        public async Task RemoveByIdAsync(int id)
        {
            await _repository.RemoveByIdAsync(id);
        }

        public async Task RemoveRangeAsync(List<TContract> datas)
        {
            var list = _mapper.Map<List<TEntity>>(datas);
            await _repository.RemoveRangeAsync(list);
        }

        public async Task UpdateAsync(TContract model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task<PagedResponse<TContract>> GetPagedDataAsync(int pageNumber, int pageSize)
        {
            var response = await _repository.GetPagedDataAsync(pageNumber, pageSize);
            return _mapper.Map<PagedResponse<TContract>>(response);
        }
    }
}
