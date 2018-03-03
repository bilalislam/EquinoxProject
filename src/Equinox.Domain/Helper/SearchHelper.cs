using System;
using Equinox.Domain.Models;
using Nest;

public static class SearchHelper
{
    public const string PRODUCT_INDEX = "product";
    public static void Initialize(ElasticClient elasticClient)
    {
        var settings = new IndexSettings { NumberOfReplicas = 0, NumberOfShards = 1 };
        var indexConfig = new IndexState
        {
            Settings = settings
        };

        if (!elasticClient.IndexExists(PRODUCT_INDEX).Exists)
        {
            elasticClient.CreateIndex(PRODUCT_INDEX, i => i
                .InitializeUsing(indexConfig)
                .Mappings(m => m.Map<Product>(ms => ms
                .Properties(p => p.Text(t => t.Name(n => n.Name)
                .Analyzer("whitespace")))
                .AutoMap())).Aliases(x => x.Alias("alias-product")));
        }
    }
}