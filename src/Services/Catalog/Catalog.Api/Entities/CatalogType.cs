namespace Catalog.Api.Entities;

public class CatalogType: BaseEntity
{ 

    [Required]
    public string Name { get; set; }
}

