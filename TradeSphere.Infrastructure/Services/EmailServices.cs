namespace TradeSphere.Infrastructure.Services
{
    public class EmailService(IConfiguration _configuration) : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(
                _configuration["EmailSettings:From"]
            ));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlBody
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration["EmailSettings:SmtpServer"],
                int.Parse(_configuration["EmailSettings:Port"]!),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]
            );

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }

}
