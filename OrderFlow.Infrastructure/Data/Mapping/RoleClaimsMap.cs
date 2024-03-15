using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderFlow.Infrastructure.Data.Mapping;

public class RoleClaimsMap : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("RoleClaims");
    }
}