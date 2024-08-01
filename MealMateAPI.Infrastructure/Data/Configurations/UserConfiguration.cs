using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FullName).IsRequired(true).HasMaxLength(40);
        builder.Property(e => e.Password).IsRequired(true).HasMaxLength(250);
        builder.Property(e => e.Email).IsRequired(true).HasMaxLength(50);
        builder.HasIndex(e => e.Email).IsUnique(true);
        builder.Property(e => e.Age).IsRequired(true);
        builder.Property(e => e.Height).IsRequired(true);
        builder.Property(e => e.Weight).IsRequired(true);
        builder.Property(e => e.Role).IsRequired(true).HasMaxLength(50).HasDefaultValue("User");
        builder.Property(e => e.Gender).IsRequired(true);
        builder.Property(e => e.ActivityStatus).IsRequired(true).HasDefaultValue(Activity.Hareketsiz);
        builder.Property(e => e.DietTypes).IsRequired(true);
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.LastLogin).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.UpdatedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.DeletedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
