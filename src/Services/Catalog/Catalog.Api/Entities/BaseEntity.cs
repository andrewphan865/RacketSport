namespace Catalog.Api.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDateUtc { get; set; } = DateTime.UtcNow;
}
