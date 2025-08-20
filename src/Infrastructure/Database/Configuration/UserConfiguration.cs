using Domain.Users.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired();

        builder.OwnsOne(u => u.Address, address 
            => address.Property(a => a.City).IsRequired()
            );

        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<User>(u => u.AccountId)
            .OnDelete(DeleteBehavior.Restrict); // отключаем cascade;
    }
}
