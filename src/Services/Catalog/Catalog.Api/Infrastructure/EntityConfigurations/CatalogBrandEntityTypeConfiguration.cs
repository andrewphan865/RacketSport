namespace Catalog.Api.Infrastructure.EntityConfigurations;

class CatalogBrandEntityTypeConfiguration: IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable("CatalogBrand");

        builder.Property(cb => cb.Name)
            .HasMaxLength(100);
    }
}
