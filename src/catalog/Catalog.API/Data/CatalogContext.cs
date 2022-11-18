using Catalog.API.Data.interfaces;
using Catalog.API.Entities;
using Catalog.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }

    public CatalogContext(IOptions<CatalogDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        Products = database.GetCollection<Product>(settings.Value.ProductsCollectionName);
        CatalogContextSeed.SeedData(Products);
    }
}