namespace Catalog.Api.Entities;
public class CatalogAttribute : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } 
    public string Values { get; set; }
    public Guid CatalogItemId { get; set; }
    public CatalogItem CatalogItem { get; set; } = null!;
}
