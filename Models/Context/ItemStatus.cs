namespace Api.Models.Context;

public class ItemStatus
{
    public int Id { get; set; }
    public string Status { get; set; }

    public ICollection<Item> Items { get; set; }
}

// public enum ItemStatus
// {
//     New = 0,
//     Completed = 1,
//     Deleted = 2,
//     Overdue = 3
// }