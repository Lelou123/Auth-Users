using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderFlow.Infrastructure.Data.Mapping;

public class UserClaimsMap : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.ToTable("UsersClaims");
    }
}