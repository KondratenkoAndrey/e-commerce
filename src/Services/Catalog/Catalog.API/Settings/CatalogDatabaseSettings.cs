namespace Catalog.API.Settings;

public class CatalogDatabaseSettings
{
    public const string SectionName = "CatalogDatabaseSettings";
    
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string ProductsCollectionName { get; set; }
}