using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data.Configurations
{
    public class AccountAdditionalFieldConfiguration : IEntityTypeConfiguration<AccountAdditionalField>
    {
        public void Configure(EntityTypeBuilder<AccountAdditionalField> builder)
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
