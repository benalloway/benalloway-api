using Api.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions options) : base(options) { }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemStatus> ItemStatuses { get; set; }
}