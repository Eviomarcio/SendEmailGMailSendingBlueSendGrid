using app.Interface;
using app.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace app.Services
{
    public class SendGridService: IEmailService
    {
        private readonly SendGridSettings _emailSettings;

        public SendGridService(IOptions<SendGridSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string messageText, string htmlMessage)
        {
            var sgc = new SendGridClient(_emailSettings.ApiKey);
            var sender = new EmailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            var recipient = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(sender, recipient, subject, messageText, htmlMessage);
            await sgc.SendEmailAsync(msg);
        }
    }
}