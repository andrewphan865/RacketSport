namespace Catalog.Api.Entities;
public class CatalogBrand : BaseEntity
{
  
    [Required]
    public string Name { get; set; }
}
