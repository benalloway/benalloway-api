namespace Api.Models.Context;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PlanLevel PlanLevel { get; set; }
}

public enum PlanLevel
{
    Free = 0,
    Paid = 1,
    Demo = 2,
    Admin = 3
}