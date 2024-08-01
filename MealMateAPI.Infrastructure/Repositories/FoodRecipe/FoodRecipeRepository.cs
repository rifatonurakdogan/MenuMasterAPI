using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Entities.Abstracts;
using MenuMasterAPI.Domain.Repositories;
using MenuMasterAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MenuMasterAPI.Infrastructure.Repositories
{
    public class FoodRecipeRepository : GenericRepository<FoodRecipe, FoodRecipeUpdateContractDomain>, IFoodRecipeRepository
    {
        private readonly MealMateAPIDbContext _context;
        private readonly DbSet<FoodRecipe> _dbSet;
        public FoodRecipeRepository(MealMateAPIDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<FoodRecipe>();
        }

        public async Task<FoodRecipe> GetByMealTypeAsync(MealType mealType)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.MealType == mealType) ?? throw new BaseException("Not found");
        }

        public async Task<List<FoodRecipe>> GetNamesByMealTypeAsync(MealType mealTypeEnum)
        {
            var entities = await _dbSet.AsNoTracking().Include(x => x.Users)
                            .OrderBy(x => x.Id)
                            .Where(x => x.MealType == mealTypeEnum && (x.Users == null || x.Users.Count == 0))
                            .Take(4)
                            .ToListAsync();
            return entities;
        }
        public async Task<FoodRecipe> GetFoodRecipeByName(string name)
        {
            var model = await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
            return model;

        }

        public async Task<List<FoodRecipe>> GetUsersFoodRecipes(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new UserNotFoundException(userId.ToString());
            var entities = await _dbSet.AsNoTracking().Include(x => x.Users)
                                 .OrderBy(x => x.Id)
                                 .Where(x => x.Users.Contains(user) && DateTime.UtcNow.Date == x.CreatedOn.Value.Date)
                                 .ToListAsync();
                                 
            return entities;
        }
    }
}
