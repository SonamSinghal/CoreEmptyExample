using CoreEmptyExample.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoreEmptyExample.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";
        private readonly SMTPConfigModel _smtpOptions;

        public object NetworkCredentials { get; private set; }

        public EmailService(IOptions<SMTPConfigModel> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }


        public async Task SendTestEmail(SendEmailModel sendEmailModel)
        {
            sendEmailModel.Subject = "This is a test Email";
            sendEmailModel.Body = UpdatePlaceHolders( GetBodyData("TestEmail"), sendEmailModel.PlaceHolders);

            await SendEmail(sendEmailModel);
        }

        public async Task SendConfirmationEmailService(SendEmailModel sendEmailModel)
        {
            sendEmailModel.Subject = "Verify your Email";
            sendEmailModel.Body = UpdatePlaceHolders(GetBodyData("ConfirmationEmail"), sendEmailModel.PlaceHolders);

            await SendEmail(sendEmailModel);
        }


        private async Task SendEmail(SendEmailModel emailOptions)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Subject = emailOptions.Subject,
                Body = emailOptions.Body,
                From = new MailAddress(_smtpOptions.SenderAddress, _smtpOptions.SenderDisplayName),
                IsBodyHtml = _smtpOptions.isBodyHTML,
            };

            foreach (var toEmail in emailOptions.SendTo)
            {
                mailMessage.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpOptions.Host,
                Port = _smtpOptions.Port,
                EnableSsl = _smtpOptions.EnableSSL,
                UseDefaultCredentials = _smtpOptions.IDefaultCredentials,
                Credentials = networkCredential,
            };

            mailMessage.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mailMessage);
        }

        private string GetBodyData(string templateName)
        {
            var body = File.ReadAllText(String.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolders(string Text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(Text) && keyValuePairs != null)
            {
                foreach(var placeholder in keyValuePairs)
                {
                    if (Text.Contains(placeholder.Key))
                    {
                        Text = Text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return Text;
        }
    }
}
