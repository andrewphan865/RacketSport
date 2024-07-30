namespace Catalog.Api.Infrastructure.EntityConfigurations;
class CatalogTypeEntityTypeConfiguration: IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable("CatalogType");

        builder.HasKey(brand => brand.Id);

        builder.Property(cb => cb.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
 

}
