using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace api.Service
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;


        public EmailService(IOptions<ApiSettings> options)
        {
            _apiKey = options.Value.ApiKey;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string plainText, string htmlContent = null)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("ryanfox1@hotmail.co.uk", "My ASP.NET App");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent ?? plainText);
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine($"SendGrid Response: {response.StatusCode}");
        }
    }
}