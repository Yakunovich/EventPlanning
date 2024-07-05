// EventConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(e => e.AdditionalFields)
                .WithOne(af => af.Event)
                .HasForeignKey(af => af.EventId);

            builder.HasMany(e => e.Registrations)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId);
        }
    }
}

// RegistrationConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;
