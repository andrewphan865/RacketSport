namespace Catalog.Api.Models;

public class CatalogType: BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
}

