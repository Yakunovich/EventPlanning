

// RegistrationConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data.Configurations
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);

            builder.HasOne(r => r.Account)
                .WithMany(a => a.Registrations)
                .HasForeignKey(r => r.AccountId);
        }
    }
}

// AccountConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;
