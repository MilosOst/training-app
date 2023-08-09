using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TrainingApp.Features.Trainings.FixedDrills;

public class FixedDrillEntityTypeConfiguration: IEntityTypeConfiguration<FixedDrill>
{
    public void Configure(EntityTypeBuilder<FixedDrill> builder)
    {
        var converter2 = new EnumToStringConverter<Category>();

        builder
            .Property(d => d.Category)
            .HasConversion(converter2);

        builder
            .HasIndex(d => d.Name)
            .IsUnique();
    }
}