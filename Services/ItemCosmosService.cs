using Api.CosmosDB.Models;
using Microsoft.Azure.Cosmos;

namespace Api.Services;
public class ItemCosmosService : IItemCosmosService
{
    private readonly Container _container;
    public ItemCosmosService(CosmosClient cosmosClient,
    string databaseName,
    string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<List<Item>> Get(string sqlCosmosQuery)
    {
        var query = _container.GetItemQueryIterator<Item>(new QueryDefinition(sqlCosmosQuery));

        List<Item> result = new List<Item>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            result.AddRange(response);
        }

        return result;
    }
}