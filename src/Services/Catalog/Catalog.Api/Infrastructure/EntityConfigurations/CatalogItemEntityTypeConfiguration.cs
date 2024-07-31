using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Catalog.Api.Infrastructure.EntityConfigurations;

class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItem");

        builder.Property(item => item.Name)
            .IsRequired(true)
            .HasMaxLength(200);

        builder.Property(item => item.Price)
            .HasPrecision(10, 2)
            .IsRequired(true);     

        builder.HasOne(item => item.CatalogBrand)
            .WithMany()
            .HasForeignKey(item => item.CatalogBrandId);

        builder.HasOne(item => item.CatalogType)
            .WithMany()
            .HasForeignKey(item => item.CatalogTypeId);

        // Configure Attributes to be stored as JSON
        var dictionaryComparer = new ValueComparer<Dictionary<string, string>>(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
        );

        builder.Property(item => item.Attributes)
            .HasConversion(
                attributes => JsonSerializer.Serialize(attributes, (JsonSerializerOptions)null),
                attributes => JsonSerializer.Deserialize<Dictionary<string, string>>(attributes, (JsonSerializerOptions)null)
            )           
            .HasColumnType("jsonb");

        builder.Property(item => item.Attributes)
            .Metadata.SetValueComparer(dictionaryComparer);

        builder.Property(item => item.SportType)
               .HasConversion(
                   sportType => sportType.ToString(), 
                   sportTypeString => (SportType)Enum.Parse(typeof(SportType), sportTypeString)
               )
               .IsRequired(true)
               .HasMaxLength(50);
    }
}
