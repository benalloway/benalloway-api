using Api.CosmosDB.Models;

namespace Api.Services;

public interface IItemCosmosService
{
    Task<List<Item>> Get(string sqlCosmosQuery);
}