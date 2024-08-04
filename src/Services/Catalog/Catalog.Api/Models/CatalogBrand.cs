namespace Catalog.Api.Models;
public class CatalogBrand : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
}
