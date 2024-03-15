using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderFlow.Infrastructure.Data.Mapping;

public class UserTokenMap : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable("UsersTokens");
    }
}