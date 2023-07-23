using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SalesSummaryFunction.Services
{
    public class SendGridMailService: IMailSenderService
    {
        private readonly IConfiguration _config;
        public SendGridMailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> SendAsync(string to , string subject, string content)
        {
            try
            {
                var apiKey = _config.GetValue<string>("sendGridApiKey");
                var fromEmailAddress = _config.GetValue<string>("sendGridFromEmailAddress");
                var fromName = _config.GetValue<string>("sendGridFromName");
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(fromEmailAddress, fromName),
                    Subject = subject,
                    PlainTextContent = content,
                    HtmlContent = $"<strong>{content}</strong>"
                };
                msg.AddTo(new EmailAddress(to));
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
