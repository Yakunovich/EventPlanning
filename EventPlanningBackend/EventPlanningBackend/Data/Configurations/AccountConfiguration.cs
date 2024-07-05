

// RegistrationConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PasswordHash)
                .IsRequired();
        }
    }
}

// AdditionalFieldConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;
