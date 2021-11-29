using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Services
{
    public class EmailSender : IEmailSender
    {
        private string _smtpServer;
        private int _port;
        private string _username;
        private string _password;
        private string _displayName;

        public EmailSender(string smtpServer, int port, string password, string displayName, string host)
        {
            _smtpServer = smtpServer;
            _port = port;
            _username = host;
            _password = password;
            _displayName = displayName;
        }
        public Task<bool> SendEmail(string emailAddress, string subject, string text)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(emailAddress));
                message.CC.Add(new MailAddress("kafil.fast09@gmail.com"));
                message.Subject = subject;
                message.Body = text;
                message.IsBodyHtml = true;
                message.From = new MailAddress(_username, _displayName);
                using (var client = new SmtpClient(_smtpServer))
                {
                    client.Port = _port;
                    client.Credentials = new NetworkCredential(_username, _password);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            return Task.FromResult(true);
        }
    }
}
