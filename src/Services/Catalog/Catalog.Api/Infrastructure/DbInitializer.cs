using System.Text.Json;

namespace Catalog.Api.Infrastructure;

public class DbInitializer
{
    private static string contentRootPath = string.Empty;   
    public static async Task InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        contentRootPath = env.ContentRootPath;        

        await SeedData(scope.ServiceProvider.GetService<CatalogContext>() ?? throw new InvalidOperationException("CatalogContext is not available"));
    }

    private static async Task SeedData(CatalogContext context)
    {
        await context.Database.MigrateAsync();

        if (context.CatalogItems.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }          

    
        var sourcePath = Path.Combine(contentRootPath, "Setup", "catalog.json");
        var sourceJson = await File.ReadAllTextAsync(sourcePath);
        var sourceItems = JsonSerializer.Deserialize<CatalogSourceEntry[]>(sourceJson)!;

        //Seed CatalogBrands
        context.CatalogBrands.RemoveRange(context.CatalogBrands);
        var catalogBrands = sourceItems.Select(x => x.Brand).Distinct().Select(brandName => new CatalogBrand {Id= Guid.NewGuid(), Name = brandName }).ToArray();
        await context.CatalogBrands.AddRangeAsync(catalogBrands);
       

        //Seed CatalogTypes
        context.CatalogTypes.RemoveRange(context.CatalogTypes);
        var catalogTypes = sourceItems.Select(x => x.Type).Distinct().Select(typeName => new CatalogType {Id=Guid.NewGuid(), Name = typeName }).ToArray();
        await context.CatalogTypes.AddRangeAsync(catalogTypes);      

        await context.SaveChangesAsync();

        var brandIdsByName = catalogBrands.ToDictionary(x => x.Name, x => x.Id);
        var typeIdsByName = catalogTypes.ToDictionary(x => x.Name, x => x.Id);

        //Seed CatalogItems
        var catalogItems = sourceItems.Select(source => new CatalogItem
        {
            Id = Guid.NewGuid(),
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
            CatalogBrandId = brandIdsByName[source.Brand],
            CatalogTypeId = typeIdsByName[source.Type],
            AvailableStock = 100,         
            PictureUrl = source.PictureUrl,
            SportType = Enum.Parse<SportType>(source.SportType),
            Attributes = source.Attributes ?? []
        }).ToArray();     
        

       await context.CatalogItems.AddRangeAsync(catalogItems);     
       await context.SaveChangesAsync();        
    }

    private class CatalogSourceEntry
    {       
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string SportType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }
}