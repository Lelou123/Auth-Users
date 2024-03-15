using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Common.Domain.Entities.User;

namespace OrderFlow.Infrastructure.Data.Mapping;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.HasMany(e => e.RoleClaims)
            .WithOne()
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();
    }
}