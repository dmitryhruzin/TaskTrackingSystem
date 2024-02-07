using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BuisnessLogicLayer.Services
{
    /// <summary>
    ///   Implements a emailService
    /// </summary>
    public class EmailService : IEmailService
    {
        readonly IOptions<EmailOptions> options;

        /// <summary>Initializes a new instance of the <see cref="EmailService" /> class.</summary>
        /// <param name="options">The options.</param>
        public EmailService(IOptions<EmailOptions> options)
        {
            this.options = options;
        }

        /// <summary>Sends the message asynchronous.</summary>
        /// <param name="model">The message model.</param>
        public async Task SendEmailAsync(MessageModel model)
        {
            var mailMessage = CreateEmailMessage(model);

            await SendAsync(mailMessage);
        }

        MimeMessage CreateEmailMessage(MessageModel message)
        {
            var emailParams = options.Value;

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Task Tracking", emailParams.From));

            emailMessage.To.AddRange(message.To);

            emailMessage.Subject = message.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format("<h2>{0}</h2>", message.Content)
            };

            return emailMessage;
        }

        async Task SendAsync(MimeMessage mailMessage)
        {
            var emailParams = options.Value;

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(emailParams.SmtpServer, emailParams.Port, true);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    await client.AuthenticateAsync(emailParams.UserName, emailParams.Password);

                    await client.SendAsync(mailMessage);
                }
                finally
                {
                    await client.DisconnectAsync(true);

                    client.Dispose();
                }
            }
        }
    }
}
