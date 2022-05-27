using Newtonsoft.Json;

namespace Api.CosmosDB.Models;

public class Item
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("status")]
    public ItemStatus Status { get; set; }

}

public enum ItemStatus
{
    Deleted = -1,
    New = 0,
    Complete = 1
}
