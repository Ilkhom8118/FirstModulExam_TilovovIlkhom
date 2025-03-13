namespace Dal.Entites
{
    public class User
    {
        public long BotUserId { get; set; }
        public long TelegramUserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumberr { get; set; }
        public UserInfo? UserInfo { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
