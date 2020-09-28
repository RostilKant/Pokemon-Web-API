using System.Threading.Tasks;
using Contracts;
using MailKit.Net.Smtp;
using MimeKit;

namespace Services
{
    public class EmailService: IEmailService
    {
        private MimeMessage _message;
        private SmtpClient _client;

        public EmailService()
        {
            _message = new MimeMessage();
            _client = new SmtpClient();
        }
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            _message.From.Add(new MailboxAddress("Pokemon Web API", "balgas92@gmail.com"));
            _message.To.Add(new MailboxAddress("", email));
            _message.Subject = subject;
            _message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            
            await _client.ConnectAsync("smtp.gmail.com", 587, false);
            await _client.AuthenticateAsync("pokemons.web.api.92@gmail.com", "monpoke123");
            await _client.SendAsync(_message);
 
            await _client.DisconnectAsync(true);
        }
    }
}