using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MenuMasterAPI.Infrastructure.Data.Context
{
    public class MealMateAPIDbContext : DbContext
    {
        public MealMateAPIDbContext() { }
        public MealMateAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<FoodRecipe> FoodRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FoodRecipeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
