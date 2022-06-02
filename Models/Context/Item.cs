using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Context;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [ForeignKey("Status")]
    public int StatusId { get; set; }
    public ItemStatus Status { get; set; }
}