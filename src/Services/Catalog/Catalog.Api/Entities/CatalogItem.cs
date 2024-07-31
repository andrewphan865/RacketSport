namespace Catalog.Api.Entities;

public class CatalogItem : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public int AvailableStock { get; set; } = 0;
    public Guid? CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; } = null!;
    public Guid? CatalogBrandId { get; set; }
    public CatalogBrand CatalogBrand { get; set; } = null!;
    public SportType SportType { get; set; }

    // For variant product properties
    public Dictionary<string, string> Attributes { get; set; } = new();

    public int RemoveStock(int quantityDesired)
    {
        if (AvailableStock == 0)
        {
            throw new CatalogDomainException($"Product item {Name} is sold out");
        }

        if (quantityDesired <= 0)
        {
            throw new CatalogDomainException($"Quantity should be greater than zero");
        }

        int removed = Math.Min(quantityDesired, this.AvailableStock);

        this.AvailableStock -= removed;

        return removed;
    }

    public int AddStock(int quantity)
    {
        int original = this.AvailableStock;    
        this.AvailableStock += quantity;       
        return this.AvailableStock - original;
    }
}
