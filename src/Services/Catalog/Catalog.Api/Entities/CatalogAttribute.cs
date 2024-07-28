namespace Catalog.Api.Entities;
public class CatalogAttribute : BaseEntity
{  
    [Required]
    public string Name { get; set; }
    public List<string> Values { get; set; } = new();
}
