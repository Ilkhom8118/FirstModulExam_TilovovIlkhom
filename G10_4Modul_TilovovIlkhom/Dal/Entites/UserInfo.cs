namespace Dal.Entites;

public class UserInfo
{
    public long UserInfoId { get; set; }
    public long UserId { get; set; }
    public string FirstNamee { get; set; }
    public string LastNamee { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Summary { get; set; }
    public User BotUsers { get; set; }
}
