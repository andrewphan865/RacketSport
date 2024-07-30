namespace Catalog.Api.Entities;
public class CatalogAttribute : BaseEntity
{  
    [Required]
    public string Name { get; set; } 
    public string Values { get; set; }
    public Guid CatalogItemId { get; set; }
    public CatalogItem CatalogItem { get; set; } = null!;
}
