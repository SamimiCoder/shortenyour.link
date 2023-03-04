namespace shortenyour.link.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationText { get; set; }
        public string CryptoWalletCode { get; set; } = null!;
        public string NotificationCategory { get; set; }
    }
}