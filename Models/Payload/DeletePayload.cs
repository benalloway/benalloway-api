using Newtonsoft.Json;

namespace Api.Models.Payload;

[JsonObject]
public class DeletePayload
{
    public List<int> Ids { get; set; }
}