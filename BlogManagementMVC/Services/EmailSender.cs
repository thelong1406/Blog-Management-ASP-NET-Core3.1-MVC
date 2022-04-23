﻿using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BlogManagementMVC.Services
{
    public class EmailSender : IEmailSender
    {
        private string _smtpServer;
        private int _smtpPort;
        private string _fromEmailAddress;

        public EmailSender(
            string smtpServer,
            int smtpPort,
            string fromEmailAddress
            )
        {
            this._smtpServer = smtpServer;
            this._smtpPort = smtpPort;
            this._fromEmailAddress = fromEmailAddress;
        }
        public Task SendEmailAsync(
            string email,
            string subject,
            string htmlMessage
            )
        { 
            var message = new MailMessage
            {
                From = new MailAddress(_fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.Send(message);

            return Task.CompletedTask;
        }
    }
}
