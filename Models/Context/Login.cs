namespace Api.Models.Context;

public class Login
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public int RelatedUserId { get; set; }
}