namespace Notification.application.DTO
{
    public class CreateNotificationDTO
    {
        public int CustomerId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}