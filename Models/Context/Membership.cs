namespace Api.Models.Context;

public class Membership
{
    public int Id { get; set; }
    public int RelatedUserId { get; set; }
    public int RelatedCompanyId { get; set; }
    public int RelatedRoleId { get; set; }
    public AccountEmail AccountEmailAddress { get; set; }
    public AccountPhone AccountPhoneNumber { get; set; }
}

public class AccountEmail
{
    public string BillingEmail { get; set; }
    public string NotificationsEmail { get; set; }
}

public class AccountPhone
{
    public string BillingPhone { get; set; }
    public string SMSPhone { get; set; }
}