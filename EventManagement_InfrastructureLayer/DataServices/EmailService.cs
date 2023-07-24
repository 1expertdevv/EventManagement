using EventManagement_ApplicationLayer.IDataServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EventManagement_InfrastructureLayer.DataServices
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;
		private readonly IEventService _eventService;


		public EmailService(IConfiguration configuration, IEventService eventService)
		{
			_configuration = configuration;
			_eventService = eventService;
		}
		public async Task<bool> SendInvitationAsync(string[] recipientEmails, string eventId)
		{
			var eventData = await _eventService.GetEventAsync(eventId);

			string joiningLink = "https://github.com/innoloft/Backend-Application";
			string emailBody = $@"
                <html>
                <head>
				 <style>
                        body {{
                            font-family: Arial, sans-serif;
							margin:0;
                        }}
						div{{
							background - color: rgb(202 245 234);
							padding: 0.5rem 1rem;
						}}
                        h1 {{
                            color: #2e384d;
                        }}
                        p {{
                            color: #333333;
                        }}
                        a {{
                            color: #007bff;
                            text-decoration: none;
                        }}
			     </style>
                </head>
                <body>
					<div style="" background-color: rgb(93, 212, 182); padding: 0.5rem 1rem;"">
                    <h1>Event Invitation</h1>
                    <p>Dear Participant,</p>
                    <p>You are invited to attend the following event:</p>
					<p><strong>Event Title:</strong> {eventData.Title}</p>
					<p><strong>Event Description:</strong> {eventData.Description}</p>
					<p><strong>Event Date:</strong> {eventData.StartDate} to {eventData.EndDate}</p>
					<p><strong>Event Location:</strong> {eventData.TimeZone}</p>
                    <p>Please click the link below to redirect to the event page:</p>
                    <a href=""{joiningLink}"">Click here to join the event</a>
                    <br />
                    <p>Thank you for your participation!</p>
                    <p>Sincerely,</p>
                    <p>Your Event Organizer</p>
					</div>
                </body>
                </html>
            ";

			try
			{
				SendEmail(recipientEmails, "Event Invitation", emailBody);
				return true;
			}
			catch (Exception ex)
			{
				return false;
				throw;
			}
		}

		private void SendEmail(string[] recipientEmails, string subject, string body)
		{
			string fromEmail = _configuration["EmailSettings:SenderEmail"];
			string fromPassword = _configuration["EmailSettings:SenderPassword"];
			string smtpServer = _configuration["EmailSettings:SmtpServer"];
			int smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);

			using (var client = new SmtpClient(smtpServer, smtpPort))
			{
				client.UseDefaultCredentials = false;
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential(fromEmail, fromPassword);

				foreach (var recipientEmail in recipientEmails)
					using (var message = new MailMessage(fromEmail, recipientEmail))
					{
						message.Subject = subject;
						message.Body = body;
						message.IsBodyHtml = true;

						client.Send(message);
					}
			}
		}
	}
}
