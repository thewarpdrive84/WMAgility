using Microsoft.Extensions.Configuration;
using WMAgility2.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string eventName, string eventDetails, string eventLocation, DateTime eventDate, DateTime eventTime);
    }

    public class SendGridMailService : IMailService
    {
        private IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string eventName, string eventDetails, string eventLocation, DateTime eventDate, DateTime eventTime)
        {
            var apiKey = _configuration["WMAgility"];
            var client = new SendGridClient(apiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("X00012206@mytudublin.ie", "WaggyMamas Agility");
            sendGridMessage.AddTo(toEmail);
            sendGridMessage.SetSubject(subject);
            sendGridMessage.SetTemplateId("d-a96c7059c7d54ba79d7a9b8bbefe5109");
            sendGridMessage.SetTemplateData(new Email
            {
                Subject = subject,
                EventName = eventName,
                EventDetails = eventDetails,
                EventLocation = eventLocation,
                EventDate = eventDate,
                EventTime = eventTime
            });
            var response = await client.SendEmailAsync(sendGridMessage);
        }
    }
}