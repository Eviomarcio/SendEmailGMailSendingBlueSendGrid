using app.Interface;
using app.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace app.Services
{
    public class SendingBlueService: IEmailService
    {
        private readonly SendingBlueSettings _emailSettings;

        public SendingBlueService(IOptions<SendingBlueSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string emailRecipient, string subject, string messageText, string htmlMessage)
        {
            var messages = new MimeMessage();
            messages.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            messages.To.Add(MailboxAddress.Parse(emailRecipient));
            messages.Subject = subject;
            var builder = new BodyBuilder { TextBody = messageText, HtmlBody = htmlMessage };
            messages.Body = builder.ToMessageBody();
            
            try
            {                
                var smtpClient = new SmtpClient();
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(_emailSettings.ServerAddress, _emailSettings.ServerPort).ConfigureAwait(false);
                await smtpClient.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password).ConfigureAwait(false);
                await smtpClient.SendAsync(messages).ConfigureAwait(false);
                await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}