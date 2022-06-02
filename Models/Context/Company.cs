namespace Api.Models.Context;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AccessLevel { get; set; }
    public int RelatedAccountId { get; set; }
}