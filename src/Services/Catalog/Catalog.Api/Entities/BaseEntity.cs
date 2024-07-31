﻿namespace Catalog.Api.Entities;

public class BaseEntity
{   
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
