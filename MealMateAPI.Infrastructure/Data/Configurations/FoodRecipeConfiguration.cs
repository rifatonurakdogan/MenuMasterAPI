using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Infrastructure.Data.Configurations;

public class FoodRecipeConfiguration : IEntityTypeConfiguration<FoodRecipe>
{
    public void Configure(EntityTypeBuilder<FoodRecipe> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Ingredients).IsRequired(true);
        builder.Property(e => e.Recipe).IsRequired(true).HasMaxLength(1000);
        builder.Property(e => e.IsLiked).HasDefaultValue(false);
        builder.Property(e => e.MealType).IsRequired(true);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.UpdatedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
    }
}
