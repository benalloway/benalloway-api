namespace Api.Models.Context;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public Email Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Email
{
    public string PrimaryEmail { get; set; }
}