using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Repositories;
using MenuMasterAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MenuMasterAPI.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User,UserUpdateContractDomain>,IUserRepository
    {
        private readonly MealMateAPIDbContext _context;
        private readonly DbSet<User> _dbSet;


        public UserRepository(MealMateAPIDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<bool> CheckIfUserExistsAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email) != null;
        }

        public async Task<User> GetUserIfExistsAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Email == email) ?? throw new UserNotFoundException(email);
            return user;
        }

        public async Task UpdateUserPasswordAsync(User user)
        {
            var currentUser = await _dbSet.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (currentUser != null)
            {
                currentUser.Password = user.Password;
                currentUser.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
