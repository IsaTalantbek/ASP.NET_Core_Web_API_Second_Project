using Domain.Users.Aggregates.Account;
using Domain.Users.Aggregates.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{ 
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Balance).IsRequired();

        //builder.HasOne<User>()
        //    .WithOne()
        //    .HasForeignKey<Account>(a => a.UserId)
        //    .OnDelete(DeleteBehavior.Restrict); // отключаем cascade;
    }
}