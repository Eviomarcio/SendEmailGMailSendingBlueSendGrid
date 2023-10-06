namespace app.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailRecipient, string subject, string messageText, string htmlMessage);
    }
}