using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderFlow.Infrastructure.Data.Mapping;

public class UserLoginMap : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("UsersLogin");
    }
}