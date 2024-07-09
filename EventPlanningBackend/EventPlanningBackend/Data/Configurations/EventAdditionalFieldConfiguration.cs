using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data.Configurations
{
    public class EventAdditionalFieldConfiguration : IEntityTypeConfiguration<EventAdditionalField>
    {
        public void Configure(EntityTypeBuilder<EventAdditionalField> builder)
        {
            builder.HasKey(af => af.Id);

            builder.Property(af => af.FieldName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(af => af.FieldValue)
                .IsRequired();

        }
    }
}
