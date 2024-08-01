using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User,UserUpdateContractDomain>
    {
        Task<bool> CheckIfUserExistsAsync(string email);
        Task<User> GetUserIfExistsAsync(string email);
        Task UpdateUserPasswordAsync(User user);
    }
}
