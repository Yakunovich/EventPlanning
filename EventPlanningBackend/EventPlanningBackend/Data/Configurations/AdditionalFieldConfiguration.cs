using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data.Configurations
{
    public class AdditionalFieldConfiguration : IEntityTypeConfiguration<AdditionalField>
    {
        public void Configure(EntityTypeBuilder<AdditionalField> builder)
        {
            builder.HasKey(af => af.Id);

            builder.Property(af => af.FieldName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(af => af.FieldValue)
                .IsRequired();

            builder.HasOne(af => af.Event)
                .WithMany(e => e.AdditionalFields)
                .HasForeignKey(af => af.EventId);
        }
    }
}
